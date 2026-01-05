using Dapper;
using EMIC2.Models.Dao.Dto.EEM2.EEM2010303;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.EEM2.EEM2010303;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.EEM2
{
    public class EEM2010303Dao : IEEM2010303Dao
    {
        /// <summary>
        ///  查到 目前 未結的 專案 的 Group
        /// </summary>
        /// <returns></returns>
        public List<string> GET_PRJ_GROUP_UID()
        {
            List<string> result = new List<string>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();

                DynamicParameters parameters = new DynamicParameters();

                sql.Append("select CAST(A2.PRJ_GROUP_UID as nvarchar) + ';' + (select TOP 1 CASE_NAME from EEM2_EOC_PRJ B2 where B2.PRJ_GROUP_UID = A2.PRJ_GROUP_UID  order by B2.EOC_ID) as data from EEM2_EOC_PRJ A2 where A2.PRJ_ETIME is null AND A2.EOC_ID <> '00000' group by A2.PRJ_GROUP_UID; ");

                result = conn.Query<string>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        ///  EEM2010303 查詢用
        /// </summary>
        /// <param name="eocID">EocID</param>
        /// <param name="EOC_GROUP_UID">EOC_GROUP_UID</param>
        /// <returns>List<EEM2010303Dao></returns>
        public List<EEM2010303Dto> EEM2010303_Result(List<string> eocID, decimal EOC_GROUP_UID = 0)
        {
            List<EEM2010303Dto> result = new List<EEM2010303Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();

                DynamicParameters parameters = new DynamicParameters();

                sql.Append(" select A1.PRJ_GROUP_UID, A1.CASE_NAME, A1.EOC_ID,  ");
                sql.Append(" (select D1.EOC_NAME  from EEM2_EOC_DATA D1 WHERE D1.EOC_ID=A1.EOC_ID) as EOC_NAME , A1.PRJ_STIME, DATEDIFF(DAY, A1.PRJ_STIME, getDate()) as EstablishedDays, ");
                sql.Append(" D2.EOC_PARENT,  (SELECT top 1 max(A3.PRJ_ETIME) FROM EEM2_EOC_PRJ A3 WHERE A3.EOC_ID = D2.EOC_PARENT AND A3.PRJ_GROUP_UID = A1.PRJ_GROUP_UID) AS ParentRemoveTime, ");
                sql.Append(" N10.CONTACT_NAME, N10.CONTACT_EMAIL, PRJ_NO ");
                sql.Append(" from EEM2_EOC_PRJ A1 left join EEM2_EOC_DATA D2 on D2.EOC_ID = A1.EOC_ID left join NTT2_COMMLIST_10 N10 on N10.EOC_ID = A1.EOC_ID ");
                sql.Append(" where A1.PRJ_GROUP_UID in (select A2.PRJ_GROUP_UID from EEM2_EOC_PRJ A2 where A2.PRJ_ETIME is null ");
                sql.Append(" group by A2.PRJ_GROUP_UID) AND A1.PRJ_ETIME is null  ");

                if (eocID.Count() > 0)
                {
                    sql.Append("  and A1.EOC_ID in ( ");
                    for (var i = 0; i < eocID.Count(); i++)
                    {
                        string e = eocID[i];
                        if (!string.IsNullOrEmpty(e))
                        {
                            string pString = i == 0 ? $" @EOC_ID_{i} " : $" , @EOC_ID_{i} ";
                            sql.Append(pString);
                            parameters.Add($"EOC_ID_{i}", eocID[i]);
                        }
                    }
                    sql.Append(" ) ");
                }

                if (EOC_GROUP_UID > 0)
                {
                    sql.Append(" And A1.PRJ_GROUP_UID = @PRJ_GROUP_UID ");
                    parameters.Add("PRJ_GROUP_UID", EOC_GROUP_UID);
                }

                // 排除中央
                sql.Append(" AND A1.EOC_ID <> '00000' ");
                sql.Append(" order by 1,3; ");

                result = conn.Query<EEM2010303Dto>(sql.ToString(), parameters).ToList();

                foreach (var item in result)
                {
                    item.IS_Checked = false;
                    item.ParentRemoveTimeText = item.ParentRemoveTime == null ? string.Empty : ((DateTime)item.ParentRemoveTime).ToString("yyyy-MM-dd HH:mm:ss");
                    item.Suggest = (item.EstablishedDays >= 10) || (item.ParentRemoveTime != null);
                    item.CONTACT_NAME = string.IsNullOrEmpty(item.CONTACT_NAME) ? string.Empty : item.CONTACT_NAME;
                    item.CONTACT_EMAIL = string.IsNullOrEmpty(item.CONTACT_EMAIL) ? string.Empty : item.CONTACT_EMAIL;
                    item.PRJ_STIME_Txt = item.PRJ_STIME.ToString("yyyy/MM/dd");
                }

                return result;
            }
        }
    }
}
