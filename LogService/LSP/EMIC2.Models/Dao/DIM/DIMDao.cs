namespace EMIC2.Models.Dao.DIM
{
    using Dapper;
    using EMIC2.Models.Dao.Dto.DIM;
    using EMIC2.Models.Dao.Dto.DIM.DIM2010301;
    using EMIC2.Models.Dao.Dto.DIM.DIM2050101;
    using EMIC2.Models.Helper;
    using EMIC2.Models.Interface.DIM;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DIMDao : IDIMDao
    {
        /// <summary>
        /// 取得鄰近案件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<DIMCaseDto> GetNearByCase(DIMCaseDto model)
        {
            string sql = @"
                WITH tbC (CASE_NO, Longitude, Latitude)
                AS
                (SELECT CASE_NO
                       ,CAST(LEFT(CASE_WGS84_PTS
                       ,CHARINDEX(',',CASE_WGS84_PTS)-1) AS DECIMAL(18,10)) AS Longitude
                       ,CASE CHARINDEX(',',CASE_WGS84_PTS,CHARINDEX(',',CASE_WGS84_PTS)+1)
                        WHEN 0 THEN CAST(RIGHT(CASE_WGS84_PTS,
                        LEN(CASE_WGS84_PTS)-CHARINDEX(',',CASE_WGS84_PTS)) AS DECIMAL(18,10))
                        ELSE CAST(SUBSTRING(CASE_WGS84_PTS
                       ,CHARINDEX(',',CASE_WGS84_PTS)+1
                       ,CHARINDEX(',',CASE_WGS84_PTS,CHARINDEX(',',CASE_WGS84_PTS)+1)-CHARINDEX(',',CASE_WGS84_PTS)-1) AS DECIMAL(18,10))
                        END AS Latitude
                FROM DIM2_CASE_INFO
                WHERE ISNULL(CASE_WGS84_PTS,'') <> ''),
                tbD (CASE_NO, Distance)
                AS
                (SELECT tbB.CASE_NO
                       ,geography::Point(tbC.Latitude, tbC.Longitude, 4326).STDistance(geography::Point(@Place_X, @Place_Y, 4326)) AS Distance
                FROM DIM2_CASE_INFO tbB
                INNER JOIN tbC ON tbC.CASE_NO=tbB.CASE_NO)
                SELECT tbA.CASE_NO
                       ,tbA.CASE_LOCATION
                       ,tbA.CASE_TIME
                       ,tbD.Distance
                       ,tbA.CASE_WGS84_PTS
                FROM DIM2_CASE_INFO tbA
                INNER JOIN tbD ON tbD.CASE_NO=tbA.CASE_NO
                WHERE 1=1
                AND tbA .CITY_ID = @CITY_ID
                AND tbA .TOWN_ID = @TOWN_ID
                AND tbA .CLOSE_STATUS = 'N'
                AND tbA .DIS_TYPE_M <> 'DIS99999'
                AND tbA .CASE_TIME >= CONVERT(date,GETDATE()-@TheDays)
                AND tbA .CASE_TIME <  CONVERT(date,GETDATE()+1)
                AND tbD.Distance >= 0 AND tbD.Distance <=@Distance
                ORDER BY tbA.CASE_TIME DESC;";

            var parameters = new
            {
                model.Place_X,
                model.Place_Y,
                model.CITY_ID,
                model.TOWN_ID,
                model.TheDays,
                model.Distance
            };

            return ConnectionHelper
                .Connect(DBHelper.GetEMIC2DBConnection(),
                    c => c.Query<DIMCaseDto>(sql, parameters).ToList());
        }

        public int GetIsAdminOrg(int prjno, int orgid)
            => ConnectionHelper
                .Connect(c => c.Query<int>(
                    "select dbo.eem2_isAdminOrg(@P_PRJ_NO,@P_ORG_ID)",
                        new { P_PRJ_NO = prjno, P_ORG_ID = orgid })).FirstOrDefault();

        public List<ProjectModelDto> GetProjectItem(SearchProjectDto searchProjectDto)
        {
            string sql =
                 $@"select PRJ_NO, EOC_ID, CASE_NAME, PRJ_STIME, PRJ_ETIME, OPEN_LV,
                        case when PRJ_ETIME is NULL then '開設中' else '已撤除' end as OPEN_STATUS,
                        dis.DIS_DATA_UID, dis.DIS_NAME
                       from [dbo].[EEM2_EOC_PRJ] prj inner join EEM2_DIS_DATA dis
                       on prj.DIS_DATA_UID = dis.DIS_DATA_UID
                       where EOC_ID = @EOC_ID
                       and PRJ_STIME >= convert(datetime,'{GetTimePara(searchProjectDto.RPT_TIME_S)}')
                       and ISNULL(PRJ_ETIME,convert(datetime,'{GetTimePara(searchProjectDto.RPT_TIME_E)}')) <= convert(datetime,'{GetTimePara(searchProjectDto.RPT_TIME_E)}')
                       and prj.DIS_DATA_UID = @DIS_DATA_UID";

            if (!string.IsNullOrWhiteSpace(searchProjectDto.CASE_NAME))
            {
                sql += $" and CASE_NAME like '%{searchProjectDto.CASE_NAME}%' ";
            }

           return ConnectionHelper.Connect(c => c.Query<ProjectModelDto>(sql, searchProjectDto)).ToList();
        }
           

        /// <summary>
        /// 取得總災情數.
        /// </summary>
        /// <param name="eocId">應變中心.</param>
        /// <param name="prjNo">專案代號.</param>
        /// <returns></returns>
        public Int32 GetDIM2_QRY_CASE_COUNT(string eocId, decimal prjNo)
        {
            Int32 resultRoles = 0;

            SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection());
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DIM2_QRY_CASE_COUNT", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // add parameter for return value
                SqlParameter rv = new SqlParameter("@returnvalue", SqlDbType.Int);
                rv.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(rv);

                // add input parameter
                cmd.Parameters.AddWithValue("@P_EOC_ID", eocId);
                cmd.Parameters.AddWithValue("@P_PRJ_NO", prjNo);

                cmd.ExecuteNonQuery();

                // return value is in the parameter @returnvalue
                Object roleCode = cmd.Parameters["@returnvalue"].Value;

                // If return value is not null then use value
                if (DBNull.Value != roleCode)
                    resultRoles = Convert.ToInt32(roleCode);
                else
                    resultRoles = 0;
            }
            catch (Exception ex)
            {
                resultRoles = 0;
            }
            finally
            {
                con.Close();
            }

            return resultRoles;
        }

        /// <summary>
        /// 取得災情未回覆數.
        /// </summary>
        /// <param name="prjNo">專案代號.</param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        /// 開發人員            日期           異動內容                    解決的問題
        /// KEN                 2019-10-04     int32 改 int64               prjNo=9999999999  parse問題
        public Int64 GetDIM2_QRY_CASE_NOT_REPLY_COUNT(string prjNo, string orgID)
        {
            Int32 resultRoles = 0;
            Int64 nPrjNo = Convert.ToInt64(prjNo);
            Int64 nOrgID = Convert.ToInt64(orgID);

            SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection());
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DIM2_QRY_CASE_NOT_REPLY_COUNT", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // add parameter for return value
                SqlParameter rv = new SqlParameter("@returnvalue", SqlDbType.Int);
                rv.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(rv);

                // add input parameter
                cmd.Parameters.AddWithValue("@P_PRJ_NO", nPrjNo);
                cmd.Parameters.AddWithValue("@P_ORG_ID", nOrgID);

                cmd.ExecuteNonQuery();

                // return value is in the parameter @returnvalue
                Object roleCode = cmd.Parameters["@returnvalue"].Value;

                // If return value is not null then use value
                if (DBNull.Value != roleCode)
                    resultRoles = Convert.ToInt32(roleCode);
                else
                    resultRoles = 0;
            }
            catch (Exception ex)
            {
                resultRoles = 0;
            }
            finally
            {
                con.Close();
            }

            return resultRoles;
        }

        /// <summary>
        /// 取得災情未指派數.
        /// </summary>
        /// <param name="eocID">應變中心.</param>
        /// <param name="prjNo">專案代號.</param>
        /// <returns></returns>
        /// 開發人員            日期           異動內容                    解決的問題
        /// KEN                 2019-10-04     int32 改 int64               prjNo=9999999999  parse問題
        public Int64 GetDIM2_QRY_CASE_NOT_ASSIGN_COUNT(string eocID, string prjNo)
        {
            Int32 resultRoles = 0;
            Int64 nPrjNo = Convert.ToInt64(prjNo);

            SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection());
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DIM2_QRY_CASE_NOT_ASSIGN_COUNT", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // add parameter for return value
                SqlParameter rv = new SqlParameter("@returnvalue", SqlDbType.Int);
                rv.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(rv);

                // add input parameter
                cmd.Parameters.AddWithValue("@P_PRJ_NO", nPrjNo);
                cmd.Parameters.AddWithValue("@P_EOC_ID", eocID);

                cmd.ExecuteNonQuery();

                // return value is in the parameter @returnvalue
                Object roleCode = cmd.Parameters["@returnvalue"].Value;

                // If return value is not null then use value
                if (DBNull.Value != roleCode)
                    resultRoles = Convert.ToInt32(roleCode);
                else
                    resultRoles = 0;
            }
            catch (Exception ex)
            {
                resultRoles = 0;
            }
            finally
            {
                con.Close();
            }

            return resultRoles;
        }

        /// <summary>
        /// 取得任務未回覆數.
        /// </summary>
        /// <param name="prjNo">專案代號.</param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public Int32 GetDIM2_QRY_TASK_NOT_REPLY_COUNT(string prjNo, string orgID)
        {
            Int32 resultRoles = 0;
            Int64 nPrjNo = Convert.ToInt64(prjNo);
            Int64 nOrgID = Convert.ToInt64(orgID);

            SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection());
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DIM2_QRY_TASK_NOT_REPLY_COUNT", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // add parameter for return value
                SqlParameter rv = new SqlParameter("@returnvalue", SqlDbType.Int);
                rv.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(rv);

                // add input parameter
                cmd.Parameters.AddWithValue("@P_PRJ_NO", nPrjNo);
                cmd.Parameters.AddWithValue("@P_ORG_ID", nOrgID);

                cmd.ExecuteNonQuery();

                // return value is in the parameter @returnvalue
                Object roleCode = cmd.Parameters["@returnvalue"].Value;

                // If return value is not null then use value
                if (DBNull.Value != roleCode)
                    resultRoles = Convert.ToInt32(roleCode);
                else
                    resultRoles = 0;
            }
            catch (Exception ex)
            {
                resultRoles = 0;
            }
            finally
            {
                con.Close();
            }

            return resultRoles;
        }

        /// <summary>
        /// 取得附近資源項目
        /// </summary>
        /// <param name="searchResourceDto"></param>
        /// <returns></returns>
        public List<ResultResourceDto> GetResourceItem(SearchResourceDto searchResourceDto)
        {
            string sql =
                 $@"select * from DIM2_FIND_RESOURCE('{searchResourceDto.X}','{searchResourceDto.Y}','{searchResourceDto.TYPE_M_NAME}','{searchResourceDto.TYPE_D_NAME}','{searchResourceDto.DISTANCE}')";

            return ConnectionHelper.Connect(c => c.Query<ResultResourceDto>(sql, searchResourceDto)).ToList();
        }

        private int GetTimePara(DateTime dateTime)
        {
            string str = string.Format("{0,4:0000}", dateTime.Year);
            str += string.Format("{0,2:00}", dateTime.Month);
            str += string.Format("{0,2:00}", dateTime.Day);

            return Convert.ToInt32(str);
        }


        /// <summary>
        ///  Create Case No
        /// </summary>
        /// <param name="type">type</param>
        /// <returns>NO</returns>
        public string GetCaseNo(string type)
        {
            string result = string.Empty;
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();

                DynamicParameters parameters = new DynamicParameters();

                sql.Append(" declare  @P_SN_NO varchar(20); ");
                sql.Append(" exec usp_GetDIM2_SN_NO @key , @type ,@P_SN_NO output; ");
                sql.Append(" select @P_SN_NO as NO; ");

                parameters.Add("key", "CASE_NO");
                parameters.Add("type", type);

                result = conn.Query<string>(sql.ToString(), parameters).ToList().FirstOrDefault();

                return result == null ? string.Empty : result;
            }
        }

        /// <summary>
        ///  Create RENEW_ID
        /// </summary>
        /// <returns>NO</returns>
        public string GetRENEW_ID()
        {
            string result = string.Empty;
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();

                DynamicParameters parameters = new DynamicParameters();

                sql.Append(" declare  @P_SN_NO varchar(20); ");
                sql.Append(" exec usp_GetDIM2_SN_NO @key , @type ,@P_SN_NO output; ");
                sql.Append(" select @P_SN_NO as NO; ");

                parameters.Add("key", "CASE_RENEW_ID");
                parameters.Add("type", "00");

                result = conn.Query<string>(sql.ToString(), parameters).ToList().FirstOrDefault();

                return result == null ? string.Empty : result;
            }
        }
    }
}