///////////////////////////////////////////////////////////////////////////////////////
//  程式名稱： EocProjectDao
//  程式描述：EocProjectDao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員      日期            方法名稱              版本      功能說明
//  KEN           2019-10-15      SearchAvailable       1.0.1     新增 SearchAvailable2  FOR  NDS20301 選專案用
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
///////////////////////////////////////////////////////////////////////////////////////

namespace EMIC2.Models.Dao.NDS2
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;
    using System.Threading.Tasks;

    using Dapper;
    using EMIC2.Models.Dao.Dto.NDS2;
    using EMIC2.Models.Helper;
    using EMIC2.Models.Interface.NDS2;

    public class EocProjectDao : IEocProjectDao
    {
        public IEnumerable<EocProjectDto> SearchAvailable(string prjType, DateTime prjSDate, DateTime prjEDate, string prjName)
        {
            return SearchAvailableAsync(prjType, prjSDate, prjEDate, prjName).Result;
        }

        public async Task<IEnumerable<EocProjectDto>> SearchAvailableAsync(string prjType, DateTime prjSDate, DateTime prjEDate, string prjName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("WITH PRJ_GROUP_DEF AS (" +
                         "SELECT PRJ_GROUP_UID " +
                         "FROM EEM2_EOC_PRJ " +
                         "GROUP BY PRJ_GROUP_UID" +
                       "), PRJ_NO_DEF AS(" +
                         "SELECT PRJ_GROUP_UID," +
                         "(" +
                           "SELECT TOP 1 EOC_ID FROM COM2_EOC_ID " +
                           "WHERE EOC_ID IN(" +
                             "SELECT EOC_ID FROM EEM2_EOC_PRJ " +
                             "WHERE PRJ_GROUP_UID = t1.PRJ_GROUP_UID" +
                           ")" +
                           "ORDER BY CAST([LEVEL] AS INT)" +
                         ") AS EOC_ID " +
                         "FROM PRJ_GROUP_DEF AS t1" +
                       "), EEM2_EOC_PRJ_DEF AS(" +
                         "SELECT PRJ_NO, a.EOC_ID, a.PRJ_GROUP_UID, CASE_NAME, DIS_DATA_UID, PRJ_STIME, PRJ_ETIME, OPEN_LV " +
                         "FROM EEM2_EOC_PRJ AS a " +
                         "JOIN PRJ_NO_DEF AS b ON b.PRJ_GROUP_UID= a.PRJ_GROUP_UID AND b.EOC_ID= a.EOC_ID " +
                         "WHERE PRJ_ETIME IS NOT NULL" +
                       ") ");

            sql.Append("SELECT P.PRJ_NO,P.EOC_ID,PRJ_GROUP_UID," +
                "CASE DIS_DATA_UID WHEN 11 THEN '風災' WHEN 12 THEN '水災' WHEN 13 THEN '震災' ELSE '其他' END AS PRJ_TYPE_NAME," +
                "CASE_NAME,PRJ_STIME,PRJ_ETIME," +
                "CASE OPEN_LV WHEN 1 THEN '一級' WHEN 2 THEN '二級' WHEN 3 THEN '三級' ELSE '撤除' END AS OPEN_LV," +
                "CASE WHEN PRJ_ETIME IS NULL THEN '開設中' ELSE '已撤除' END AS OPEN_STATUS " +
                "FROM(" +
                  "SELECT PRJ_NO,EOC_ID,PRJ_GROUP_UID,DIS_DATA_UID,CASE_NAME,PRJ_STIME,PRJ_ETIME,OPEN_LV " +
                  //"FROM EEM2_EOC_PRJ " +
                  "FROM EEM2_EOC_PRJ_DEF " +
                  "WHERE 1 = 1 ");

            // DIS_DATA_UID 的資料請參考 [EEM2_DIS_DATA]
            if (!string.IsNullOrEmpty(prjType))
            {
                switch (prjType)
                {
                    case "0": // 風災
                        sql.Append("    AND DIS_DATA_UID IN (11) ");
                        break;

                    case "1": // 水災
                        sql.Append("    AND DIS_DATA_UID IN (12) ");
                        break;

                    case "2": // 震災
                        sql.Append("    AND DIS_DATA_UID IN (13) ");
                        break;

                    case "3": // 其他
                        sql.Append("    AND DIS_DATA_UID NOT IN (11,12,13) ");
                        break;
                }
            }

            sql.Append("    AND convert(char,PRJ_STIME,112) >= @PRJ_STIME ");
            sql.Append("    AND (PRJ_ETIME IS NULL OR convert(char,PRJ_ETIME,112) <= @PRJ_ETIME) ");
            if (!string.IsNullOrEmpty(prjName))
            {
                sql.Append("    AND CASE_NAME LIKE '@CASE_NAME%' ");
            }

            sql.Append(") AS P " +
                "LEFT JOIN NDS2_BASIS B ON B.PRJ_NO = P.PRJ_NO " +
                "WHERE B.MAIN_ID IS NULL " +
                "ORDER BY PRJ_STIME ");

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string SDate = prjSDate.ToString("yyyyMMdd");
                string EDate = prjEDate.ToString("yyyyMMdd");
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PRJ_STIME", SDate);
                parameters.Add("@PRJ_ETIME", EDate);
                if (!string.IsNullOrEmpty(prjName))
                {
                    parameters.Add("@CASE_NAME", prjName);
                }

                //var result = await conn.QueryAsync<EocProjectDto>(sql.ToString(), parameters);
                var result = conn.Query<EocProjectDto>(sql.ToString(), parameters);
                return result;
            }
        }

        /// <summary>
        /// for 20301  search  project
        /// </summary>
        /// <param name="prjType"></param>
        /// <param name="prjSDate"></param>
        /// <param name="prjEDate"></param>
        /// <param name="prjName"></param>
        /// <returns> IEnumerable<EocProjectDto></returns>
        /// 開發人員            日期           異動內容                    解決的問題
        /// KEN                 2019-10-15     新增一個FUCNTION            移除B.MAIN_ID IS NULL  條件
        public IEnumerable<EocProjectDto> SearchAvailable20301(string prjType, DateTime prjSDate, DateTime prjEDate, string prjName)
        {
            return SearchAvailableAsync20301(prjType, prjSDate, prjEDate, prjName).Result;
        }

        /// <summary>
        /// for 20301  search  project
        /// </summary>
        /// <param name="prjType"></param>
        /// <param name="prjSDate"></param>
        /// <param name="prjEDate"></param>
        /// <param name="prjName"></param>
        /// <returns> IEnumerable<EocProjectDto></returns>
        /// 開發人員            日期           異動內容                    解決的問題
        /// KEN                 2019-10-15     新增一個FUCNTION            移除B.MAIN_ID IS NULL  條件
        public async Task<IEnumerable<EocProjectDto>> SearchAvailableAsync20301(string prjType, DateTime prjSDate, DateTime prjEDate, string prjName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("WITH PRJ_GROUP_DEF AS (" +
                         "SELECT PRJ_GROUP_UID " +
                         "FROM EEM2_EOC_PRJ " +
                         "GROUP BY PRJ_GROUP_UID" +
                       "), PRJ_NO_DEF AS(" +
                         "SELECT PRJ_GROUP_UID," +
                         "(" +
                           "SELECT TOP 1 EOC_ID FROM COM2_EOC_ID " +
                           "WHERE EOC_ID IN(" +
                             "SELECT EOC_ID FROM EEM2_EOC_PRJ " +
                             "WHERE PRJ_GROUP_UID = t1.PRJ_GROUP_UID" +
                           ")" +
                           "ORDER BY CAST([LEVEL] AS INT)" +
                         ") AS EOC_ID " +
                         "FROM PRJ_GROUP_DEF AS t1" +
                       "), EEM2_EOC_PRJ_DEF AS(" +
                         "SELECT PRJ_NO, a.EOC_ID, a.PRJ_GROUP_UID, CASE_NAME, DIS_DATA_UID, PRJ_STIME, PRJ_ETIME, OPEN_LV " +
                         "FROM EEM2_EOC_PRJ AS a " +
                         "JOIN PRJ_NO_DEF AS b ON b.PRJ_GROUP_UID= a.PRJ_GROUP_UID AND b.EOC_ID= a.EOC_ID " +
                         "WHERE PRJ_ETIME IS NOT NULL" +
                       ") ");

            sql.Append("SELECT P.PRJ_NO,P.EOC_ID,PRJ_GROUP_UID," +
                "CASE DIS_DATA_UID WHEN 11 THEN '風災' WHEN 12 THEN '水災' WHEN 13 THEN '震災' ELSE '其他' END AS PRJ_TYPE_NAME," +
                "CASE_NAME,PRJ_STIME,PRJ_ETIME," +
                "CASE OPEN_LV WHEN 1 THEN '一級' WHEN 2 THEN '二級' WHEN 3 THEN '三級' ELSE '撤除' END AS OPEN_LV," +
                "CASE WHEN PRJ_ETIME IS NULL THEN '開設中' ELSE '已撤除' END AS OPEN_STATUS " +
                "FROM(" +
                  "SELECT PRJ_NO,EOC_ID,PRJ_GROUP_UID,DIS_DATA_UID,CASE_NAME,PRJ_STIME,PRJ_ETIME,OPEN_LV " +
                  //"FROM EEM2_EOC_PRJ " +
                  "FROM EEM2_EOC_PRJ_DEF " +
                  "WHERE 1 = 1 ");

            // DIS_DATA_UID 的資料請參考 [EEM2_DIS_DATA]
            if (!string.IsNullOrEmpty(prjType))
            {
                switch (prjType)
                {
                    case "0": // 風災
                        sql.Append("    AND DIS_DATA_UID IN (11) ");
                        break;

                    case "1": // 水災
                        sql.Append("    AND DIS_DATA_UID IN (12) ");
                        break;

                    case "2": // 震災
                        sql.Append("    AND DIS_DATA_UID IN (13) ");
                        break;

                    case "3": // 其他
                        sql.Append("    AND DIS_DATA_UID NOT IN (11,12,13) ");
                        break;
                }
            }

            sql.Append("    AND convert(char,PRJ_STIME,112) >= @PRJ_STIME ");
            sql.Append("    AND (PRJ_ETIME IS NULL OR convert(char,PRJ_ETIME,112) <= @PRJ_ETIME) ");
            if (!string.IsNullOrEmpty(prjName))
            {
                sql.Append("    AND CASE_NAME LIKE '@CASE_NAME%' ");
            }

            sql.Append(") AS P " +
                "LEFT JOIN NDS2_BASIS B ON B.PRJ_NO = P.PRJ_NO " +
                "WHERE 1=1 " +    //B.MAIN_ID IS NULL
                "ORDER BY PRJ_STIME ");

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string SDate = prjSDate.ToString("yyyyMMdd");
                string EDate = prjEDate.ToString("yyyyMMdd");
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PRJ_STIME", SDate);
                parameters.Add("@PRJ_ETIME", EDate);
                if (!string.IsNullOrEmpty(prjName))
                {
                    parameters.Add("@CASE_NAME", prjName);
                }

                //var result = await conn.QueryAsync<EocProjectDto>(sql.ToString(), parameters);
                var result = conn.Query<EocProjectDto>(sql.ToString(), parameters);
                return result;
            }
        }
    }
}