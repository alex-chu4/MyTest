///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  SSO2020701Dao.cs
//  程式名稱：
//  帳號相關資料查詢
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期             版本        備註
//  Vivian Chu      2019-06-05          1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供[帳號相關資料]資料庫SQL查詢指令。 
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Dao.SSO2
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Dapper;
    using EMIC2.Models.Dao.Dto.SSO2;
    using EMIC2.Models.Helper;
    using EMIC2.Models.Interface;
    using EMIC2.Models.Interface.SSO2;
    using EMIC2.Models.Repository;

    public class SSODao : ISSO2Dao
    {
        public string GetUserRoles(string userId)
            => ConnectionHelper.Connect(DBHelper.GetEMIC2DBConnection(), c =>
              c.Query<string>(
                  "SELECT ROLE_LIST from [dbo].[COM2_GET_USER_ROLE](@P_USER_ID)",
                  new { P_USER_ID = userId })).FirstOrDefault() ?? null;

        public string GetGroupCodes(string userId)
          => ConnectionHelper.Connect(DBHelper.GetEMIC2DBConnection(), c =>
              c.Query<string>(
                  "SELECT GROUP_LIST from [dbo].[COM2_GET_USER_GROUP](@P_USER_ID)",
                  new { P_USER_ID = userId })).FirstOrDefault() ?? null;

        public string GetOrgIDFromOID(string oid)
        {
            if (string.IsNullOrEmpty(oid) == false)
            {
                IRepository<COM2_OID> _repos = new GenericRepository<COM2_OID>();
                COM2_OID data = _repos.Get(x => x.OID == oid);
                if (data != null)
                    return data.ORG_ID.ToString();

                //string sql = @"SELECT TOP(1) ORG_ID FROM COM2_OID WHERE OID = @oid";
                //DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@oid", oid);

                //using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
                //{
                //    IEnumerable<COM2_OID> result = conn.Query<COM2_OID>(sql, parameters);
                //    if (result.AsList<COM2_OID>().Count > 0)
                //    {
                //        string rtn = result.AsList<COM2_OID>()[0].ORG_ID.ToString();
                //        return rtn;
                //    }
                //}
 
            }

            return string.Empty;

        }

        public IEnumerable<SSO2020701Dto> GetLoginDataList(DateTime startTime, DateTime endTime, string accountTypeName, string access)
        {
            string sqlStatement = @"
                            SELECT DISTINCT log.ACCESS_TIME, log.USER_ID
                                            , log.USER_NAME
                                            , log.COMPANY
                                            , (SELECT TOP 1 ACCESS FROM SSO2_OPERATING WHERE ACCESS = log.ACCESS) as ACCESS
                                            , (SELECT TOP 1 ACCOUNT_TYPE_NAME FROM COM2_ACCOUNT_TYPE WHERE ACCOUNT_TYPE_NAME = log.ACCOUNT_TYPE_NAME) as ACCOUNT_TYPE_NAME
                            FROM SSO2_ACCESS_LOG log
                            WHERE 1 = 1
                        ";

            if (startTime > new DateTime(1911, 1, 1) && endTime > new DateTime(1911, 1, 1))
            {
                sqlStatement += @" AND ACCESS_TIME >= @startTime AND ACCESS_TIME <= @endTime ";
            }

            if (!string.IsNullOrEmpty(accountTypeName))
            {
                sqlStatement += " AND log.ACCOUNT_TYPE_NAME = @accountTypeName";
            }

            if (!string.IsNullOrEmpty(access))
            {
                sqlStatement += " AND log.ACCESS = @access";
            }
            sqlStatement += " ORDER BY log.ACCESS_TIME DESC";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@startTime", startTime);
                parameters.Add("@endTime", endTime);
                parameters.Add("@accountTypeName", accountTypeName);
                parameters.Add("@access", access);

                var result = conn.Query<SSO2020701Dto>(sqlStatement, parameters);
                return result;
            }
        }

        public IEnumerable<SSO2020703Dto> GetStatusDataList(DateTime startTime, DateTime endTime, string accountType, string status)
        {
            string sqlStatement = @"
                SELECT USER_ID
                        , USER_NAME
                        , COMPANY
                        , (SELECT TOP 1 ACCOUNT_TYPE_NAME FROM COM2_ACCOUNT_TYPE WHERE ACCOUNT_TYPE_CODE = info.ACCOUNT_TYPE) as ACCOUNT_TYPE
                        , (CASE STATUS WHEN 'A' THEN '啟用'
	                                WHEN 'D' THEN '刪除'
	                                WHEN 'T' THEN '停用'
	                                WHEN 'L' THEN '鎖定' END) AS STATUS
                        , MODIFIED_TIME
                FROM SSO2_USER_INFO info
                WHERE 1 = 1
            ";

            if (startTime > new DateTime(1911, 1, 1) && endTime > new DateTime(1911, 1, 1))
            {
                sqlStatement += @"
                  AND info.MODIFIED_TIME >= @startTime
                  AND info.MODIFIED_TIME <= @endTime
                ";
            }

            if (!string.IsNullOrEmpty(accountType))
            {
                sqlStatement += " AND info.ACCOUNT_TYPE = @accountType";
            }

            if (!string.IsNullOrEmpty(status))
            {
                sqlStatement += " AND info.STATUS = @status";
            }

            sqlStatement += " ORDER BY USER_ID";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@startTime", startTime);
                parameters.Add("@endTime", endTime);
                parameters.Add("@accountType", accountType);
                parameters.Add("@status", status);

                var result = conn.Query<SSO2020703Dto>(sqlStatement, parameters);
                return result;
            }
        }

        public IEnumerable<SSO2020705Dto> GetAuthDataList(DateTime startDate, DateTime endDate, string sysCode)
        {
            string sqlStatement = @"
			    SELECT CREATED_TIME, USER_ID, USER_NAME, COMPANY, SYS_NAME, ISNULL(GROUP_NAME, ROLE_NAME) as AUTH_NAME
			    FROM(
				    SELECT userAuth.CREATED_TIME
                         , userAuth.USER_ID
                         , userInfor.USER_NAME
                         , userInfor.COMPANY
                         --, userAuth.SYS_CODE
                         , (SELECT TOP 1 SYS_NAME FROM COM2_SYS_CODE WHERE SYS_CODE = userAuth.SYS_CODE) as SYS_NAME
                         , (SELECT TOP 1 GROUP_NAME FROM SSO2_GROUP WHERE GROUP_CODE = userAuth.GROUP_CODE) as GROUP_NAME
                         , (SELECT TOP 1 ROLE_NAME FROM SSO2_ROLE WHERE ROLE_CODE = userAuth.ROLE_CODE) as ROLE_NAME
                    FROM SSO2_USER_AUTH userAuth
                    LEFT JOIN (SELECT USER_ID, USER_NAME, COMPANY FROM SSO2_USER_INFO) userInfor ON userAuth.USER_ID = userInfor.USER_ID
                    WHERE 1 = 1			    
                ";

            if (startDate > new DateTime(1911, 1, 1) && endDate > new DateTime(1911, 1, 1))
            {
                sqlStatement += @" AND userAuth.CREATED_TIME >= @startDate AND userAuth.CREATED_TIME <= @endDate";
            }

            if (!string.IsNullOrEmpty(sysCode))
            {
                sqlStatement += @" AND userAuth.SYS_CODE = @sysCode ";
            }

            sqlStatement += ") as Result ORDER BY CREATED_TIME DESC";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@startDate", startDate);
                parameters.Add("@endDate", endDate);
                parameters.Add("@sysCode", sysCode);

                var result = conn.Query<SSO2020705Dto>(sqlStatement, parameters);
                return result;
            }
        }

        public IEnumerable<SSO2020707Dto> GetLongUnLoginDataList(int dateAgo, string accountType)
        {
            string sqlStatement = @"
				SELECT LAST_LOGIN as LAST_LOGIN_TIME
                     , USER_ID, USER_NAME
                     , COMPANY
                     , at.ACCOUNT_TYPE_NAME
                FROM SSO2_USER_INFO info
                LEFT JOIN COM2_ACCOUNT_TYPE at ON at.ACCOUNT_TYPE_CODE = info.ACCOUNT_TYPE
                WHERE LAST_LOGIN IS NOT NULL		    
                ";

            // 上次登入時間
            if (dateAgo > 0)
            {
                sqlStatement += @" AND info.LAST_LOGIN <= DateAdd(m,-@dateAgo, GetDate())";
            }

            //帳號類別
            if (!string.IsNullOrEmpty(accountType))
            {
                sqlStatement += @" AND at.ACCOUNT_TYPE_CODE = @accountType ";
            }

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@dateAgo", dateAgo);
                parameters.Add("@accountType", accountType);

                var result = conn.Query<SSO2020707Dto>(sqlStatement, parameters);
                return result;
            }
        }

        public IEnumerable<SSO2020702Dto> GetLoginStaDataList(DateTime startTime, DateTime endTime)
        {
            string sqlStatement = @"SELECT ACCOUNT_TYPE_NAME
                                            , ACCOUNT_LOGIN_COUNT
                                            , (UserCount - ACCOUNT_LOGIN_COUNT) AS ACCOUNT_UNLOGIN_COUNT
	                                        , LOGIN_TIMES
                                    FROM( SELECT ACCOUNT_TYPE_NAME
		                                    --登入帳號數量
			                                    , (SELECT COUNT(DISTINCT USER_ID)
                                                    FROM SSO2_ACCESS_LOG 
				                                    WHERE ACCESS = '登入'　
				                                    AND ACCOUNT_TYPE_NAME = sat.ACCOUNT_TYPE_NAME
                                    ";

            if (startTime > new DateTime(1911, 1, 1) && endTime > new DateTime(1911, 1, 1))
            {
                sqlStatement += @" AND ACCESS_TIME >= @startTime AND ACCESS_TIME <= @endTime";
            }

            sqlStatement += @"      
                                                 ) AS ACCOUNT_LOGIN_COUNT
		                                    --所有帳號
			                                    , (SELECT COUNT(1) FROM SSO2_USER_INFO WHERE ACCOUNT_TYPE = sat.ACCOUNT_TYPE_CODE) AS UserCount
		                                    --登入次數
			                                    , (SELECT COUNT(1)
                                                    FROM SSO2_ACCESS_LOG
                                                    WHERE ACCESS = '登入'
                                                    AND ACCOUNT_TYPE_NAME = sat.ACCOUNT_TYPE_NAME
                                    ";

            if (startTime > new DateTime(1911, 1, 1) && endTime > new DateTime(1911, 1, 1))
            {
                sqlStatement += @" AND ACCESS_TIME >= @startTime AND ACCESS_TIME <= @endTime";
            }

            sqlStatement += @"
				                                    ) AS LOGIN_TIMES
		                                    FROM COM2_ACCOUNT_TYPE sat
                                    ) ssat";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@startTime", startTime);
                parameters.Add("@endTime", endTime);

                var result = conn.Query<SSO2020702Dto>(sqlStatement, parameters);
                return result;
            }
        }

        public IEnumerable<SSO2020704Dto> GetStatusStaDataList(DateTime startTime, DateTime endTime)
        {
            string sqlStatement = @"SELECT ty.ACCOUNT_TYPE_NAME
                                            , R.A
                                            , R.T
                                            , R.L
                                    FROM (SELECT result.ACCOUNT_TYPE
                                                , result.A
                                                , result.T
                                                , result.L
                                            FROM (SELECT ACCOUNT_TYPE
                                                        , STATUS
                                                FROM SSO2_USER_INFO 
                                    ";

            if (startTime > new DateTime(1911, 1, 1) && startTime > new DateTime(1911, 1, 1))
            {
                sqlStatement += @"
                      WHERE MODIFIED_TIME >= @startTime
                        AND MODIFIED_TIME <= @endTime
                    ";
            }

            sqlStatement += @"
                            ) as info
		                PIVOT (COUNT(STATUS) FOR STATUS IN ([A],[T],[L]) ) AS result) R
                    RIGHT JOIN COM2_ACCOUNT_TYPE ty ON ty.ACCOUNT_TYPE_CODE = R.ACCOUNT_TYPE
                    ORDER BY ty.ACCOUNT_TYPE_CODE";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@startTime", startTime);
                parameters.Add("@endTime", endTime);

                var result = conn.Query<SSO2020704Dto>(sqlStatement, parameters);
                return result;
            }
        }

        public IEnumerable<SSO2020706Dto> GetAuthStaDataList(DateTime startTime, DateTime endTime)
        {
            string sqlStatement
                = @"Select R.SYS_CODE, R.SYS_NAME, ISNULL(R.GROUP_NAME, R.ROLE_NAME) GR_NAME, GEN, ORG, EDD, EGOV
                    FROM(
	                    SELECT *
	                    FROM (SELECT userAuth.ACCOUNT_TYPE
				                    , userAuth.SYS_CODE
				                    , (SELECT TOP 1 SYS_NAME FROM COM2_SYS_CODE WHERE SYS_CODE = userAuth.SYS_CODE) as SYS_NAME
				                    , userAuth.GROUP_CODE
				                    , (SELECT TOP 1 GROUP_NAME FROM SSO2_GROUP WHERE GROUP_CODE = userAuth.GROUP_CODE) as GROUP_NAME
				                    , userAuth.ROLE_CODE
				                    , (SELECT TOP 1 ROLE_NAME FROM SSO2_ROLE WHERE ROLE_CODE = userAuth.ROLE_CODE) as ROLE_NAME
			                    FROM SSO2_USER_AUTH userAuth 
                            ";

            if (startTime > new DateTime(1911, 1, 1) && endTime > new DateTime(1911, 1, 1))
            {
                sqlStatement += @"
                      WHERE userAuth.CREATED_TIME >= @startTime
                        AND userAuth.CREATED_TIME <= @endTime
                    ";
            }

            sqlStatement += @"
		                    ) AS info 
	                    PIVOT (COUNT(ACCOUNT_TYPE) FOR ACCOUNT_TYPE IN ([GEN],[ORG],[EDD],[EGOV])) AS result
                    ) as R
                    ORDER BY R.GROUP_CODE, R.ROLE_CODE";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@startTime", startTime);
                parameters.Add("@endTime", endTime);

                var result = conn.Query<SSO2020706Dto>(sqlStatement, parameters);
                return result;
            }
        }

        public IEnumerable<SSO2020708Dto> GeOnLineDataList(int timeAgo, string accountTypeName)
        {
            string sqlStatement = @"
                            SELECT DISTINCT log.ACCESS_TIME, log.USER_ID
                                            , log.USER_NAME
                                            , log.COMPANY
				                            , (SELECT TOP 1 ACCOUNT_TYPE_NAME FROM COM2_ACCOUNT_TYPE 
					                            WHERE ACCOUNT_TYPE_NAME = log.ACCOUNT_TYPE_NAME) as ACCOUNT_TYPE_NAME
                            FROM SSO2_ACCESS_LOG log
                            WHERE 1 = 1 AND ACCESS = '登入'
                        ";

            // 上次登入時間
            if (timeAgo > 0)
            {
                sqlStatement += @" AND log.ACCESS_TIME >= DateAdd(n,-@timeAgo, GetDate())";
            }

            if (!string.IsNullOrEmpty(accountTypeName))
            {
                sqlStatement += " AND log.ACCOUNT_TYPE_NAME = @accountTypeName";
            }

            sqlStatement += " ORDER BY log.ACCESS_TIME DESC";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@timeAgo", timeAgo);
                parameters.Add("@accountTypeName", accountTypeName);

                var result = conn.Query<SSO2020708Dto>(sqlStatement, parameters);
                return result;
            }
        }
    }
}
