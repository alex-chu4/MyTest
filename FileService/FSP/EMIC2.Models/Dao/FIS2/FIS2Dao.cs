///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  IFIS2Dao.cs
//  程式名稱：
//  目前撤離區域-查詢
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員           日期             版本           備註
//  Vivian Chu      2019-09-06         1.0.0.0       初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供[目前撤離區域]資料庫SQL查詢指令。 
///////////////////////////////////////////////////////////////////////////////////////

namespace EMIC2.Models.Dao.FIS2
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using Dapper;
    using EMIC2.Models.Dao.Dto.FIS2;
    using EMIC2.Models.Helper;
    using EMIC2.Models.Interface.FIS2;
    using Util.WebClass;

    public class FIS2Dao : IFIS2Dao
    {
        /// <summary>
        /// 取得撤離區域列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IEnumerable<FIS2010106Dto> GetDataList(string cityId, string townId)
        {
            DynamicParameters parameters = new DynamicParameters();

            string sqlStatement = @" SELECT
                                        R.CITY_NAME, R.TOWN_NAME, R.VILLAGE_NAME
                                     FROM  
                                        (SELECT 
	                                          EA.CITY_ID, CT.CITY_NAME, 
	                                          EA.TOWN_ID, CT.TOWN_NAME, 
	                                          EA.VILLAGE_ID, CT.VILLAGE_NAME 
	                                    FROM EEA2_EVACUATE_AREA EA , COM2_CITYTOWN_ID CT
	                                    WHERE EA.EVACUATEDATE_E IS NULL
		                                    AND CT.IS_VALID=1
		                                    AND EA.CITY_ID = CT.CITY_ID
		                                    AND EA.TOWN_ID = CT.TOWN_ID
		                                    AND EA.VILLAGE_ID = CT.VILLAGE_ID
	                                    GROUP BY 
	                                        EA.CITY_ID, CT.CITY_NAME, 
	                                        EA.TOWN_ID, CT.TOWN_NAME, 
	                                        EA.VILLAGE_ID, CT.VILLAGE_NAME) R
                                    WHERE 1 = 1";

            if (!string.IsNullOrEmpty(cityId))
            {
                sqlStatement += " AND R.CITY_ID = @cityId";
            }

            if (townId != null && townId.Length > 0)
            {
                string[] list = townId.Split(',');
                sqlStatement += " AND R.TOWN_ID in (";
                for(int i = 1; i <= list.Length; i++)
                {
                    sqlStatement += "@townId" + i + ",";

                    parameters.Add("@townId" + i, list[i-1]);
                }
                sqlStatement = sqlStatement.TrimEnd(',');
                sqlStatement += ")";
            }

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                parameters.Add("@cityId", cityId);
                
                var result = conn.Query<FIS2010106Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得[我正在找的人]-[設定自動尋人]列表
        /// </summary>
        /// <param name="personUid">登入者序號(尋人者)</param>
        /// <param name="startupSeqno">系統啟用序號</param>
        /// <returns></returns>
        public IEnumerable<FIS2010105Dto> GetLookForDataList(string personUid, string startupSeqno)
        {
            DynamicParameters parameters = new DynamicParameters();

            string sqlStatement = @" 
                                    SELECT LOOKFOR_SEQNO, LOOKFOR_PERSON_NAME
	                                        , SUCCESS_REC_NO
	                                        , CREATED_TIME 
		                                    , MODIFIED_TIME LASTSEARCH_TIME
		                                    , LOOKFOR_STATUS
                                    FROM FIS2_LOOKFOR
                                    WHERE PERSON_UID = @personUid AND STARTUP_SEQNO = @startupSeqno
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                parameters.Add("@personUid", personUid);
                parameters.Add("@startupSeqno", startupSeqno);

                var result = conn.Query<FIS2010105Dto>(sqlStatement, parameters);
                return result;
            }
        }

        public bool Create(FIS2_LOOKFOR dto)
        {
            DynamicParameters parameters = new DynamicParameters();

            string sqlStatement = @" 
                                    INSERT INTO FIS2_LOOKFOR 
                                    (PERSON_UID, LOOKFOR_PERSON_NAME, PERSON_SEX, BIRTHDAY_YEAR, 
                                     CITIZENSHIP, LOGIN_PERSON_EMAIL, LOOKFOR_STATUS, SUCCESS_REC_NO, DISP_FLAG,
                                     CREATED_TIME, CREATED_BY, MODIFIED_TIME, MODIFIED_BY)
                                    VALUES 
                                    (@personUid, @LookforPersonNmae, @PersonSex, @BirthdayYear,
                                     @Citizenship, @loginPersonEmail, @lookforStatus, @SuccessRecNo, @DispFlag,
                                     GETDATE(), @CreatedBy, GETDATE(), @ModifiedBy)
                                    ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                parameters.Add("@personUid", dto.PERSON_UID);
                parameters.Add("@LookforPersonNmae", dto.LOOKFOR_PERSON_NAME);
                parameters.Add("@PersonSex", dto.PERSON_SEX);
                parameters.Add("@BirthdayYear", dto.BIRTHDAY_YEAR);
                parameters.Add("@Citizenship", dto.CITIZENSHIP);
                parameters.Add("@loginPersonEmail", dto.LOGIN_PERSON_EMAIL);
                parameters.Add("@lookforStatus", dto.LOOKFOR_STATUS);
                parameters.Add("@SuccessRecNo", dto.SUCCESS_REC_NO);
                parameters.Add("@DispFlag", dto.DISP_FLAG);
                parameters.Add("@CreatedBy", dto.CREATED_BY);
                parameters.Add("@ModifiedBy", dto.MODIFIED_BY);

                // 1 if success, returns 0 if fail.
                var result = conn.Execute(sqlStatement, parameters);
                return result == 1;
            }
        }

        /// <summary>
        /// 取得目前中央EOC開設中的專案代號prj_no
        /// </summary>
        private List<EocID_PrjNO> GetEocPrjNos()
        {
            DBDataUtility util = new DBDataUtility();
            List<EocID_PrjNO> result = new List<EocID_PrjNO>();

            string sql = @"select PRJ_NO, EOC_ID from EEM2_EOC_PRJ where PRJ_ETIME is null and EOC_ID in (select EOC_ID from EEM2_EOC_DATA WHERE EOC_LEVEL=2)";

            using (SqlConnection conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                using (SqlCommand cmdSelect = new SqlCommand(sql, conn))
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();

                    // FORTIFY: Unreleased Resource: Database 
                    using (SqlDataReader dr = cmdSelect.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                EocID_PrjNO data = new EocID_PrjNO();
                                data.eoc_id = util.CheckString(dr["EOC_ID"]);
                                data.prj_no = Convert.ToInt64(util.CheckDecimal(dr["PRJ_NO"]));
                                result.Add(data);
                            }
                        }
                    }
                    conn.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 取得傷亡人數
        /// </summary>
        /// <param name="eocId"></param>
        /// <returns></returns>
        public Int64 GetRealHurtPeople()
        {
            Int64 result = 0;
            List<EocID_PrjNO> datas = GetEocPrjNos();
            string sql = @"select count(*) FROM ERA2_QRY_MAX_PEOPLE_MISSING(@eocId,@prjNo,null)";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                foreach (EocID_PrjNO dat in datas)
                {
                    //設定參數
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@eocId", dat.eoc_id);
                    parameters.Add("@prjNo", dat.prj_no);

                    result += Convert.ToInt64(conn.ExecuteScalar(sql, parameters));
                }
            }
            return result;
        }

        /// <summary>
        /// 取中央EOC開設中專案最早成立時間
        /// </summary>
        /// <returns></returns>
        private DateTime GetStartDate()
        {
            DateTime result = new DateTime();
            string sql = @"select min(PRJ_STIME) from EEM2_EOC_PRJ where EOC_ID='00000' and PRJ_ETIME is null";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                result = Convert.ToDateTime(conn.ExecuteScalar(sql, null));
            }
            return result;
        }

        /// <summary>
        /// 取得收容名冊人數
        /// </summary>
        /// <param name="eocId"></param>
        /// <returns></returns>
        public Int64 GetRealHelterPeople()
        {
            Int64 result = 0;
            DateTime startDate = GetStartDate();

            string sql = @"select count(*) FROM EEA2_QRY_SH_REPORT(@startDate,@nowDate)";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@startDate", startDate.ToString("yyyy/MM/dd"));
                parameters.Add("@nowDate", DateTime.Now.ToString("yyyy/MM/dd"));

                result = Convert.ToInt64(conn.ExecuteScalar(sql, parameters));
            }
            return result;
        }

        /// <summary>
        /// 取得收容名冊人數
        /// </summary>
        /// <param name="eocId"></param>
        /// <returns></returns>
        public Int64 GetRealLeavePeople()
        {
            Int64 result = 0;
            DateTime startDate = GetStartDate();

            string sql = @"select count(*) FROM EEA2_QRY_EVA_REPORT(@startDate,@nowDate)";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@startDate", startDate.ToString("yyyy/MM/dd"));
                parameters.Add("@nowDate", DateTime.Now.ToString("yyyy/MM/dd"));

                result = Convert.ToInt64(conn.ExecuteScalar(sql, parameters));
            }
            return result;
        }
    }

    public class EocID_PrjNO
    {
        public string eoc_id { set; get; }
        public Int64 prj_no { set; get; }
    }
}
