
using Dapper;
using EMIC2.Models.Dao.Dto;
using EMIC2.Models.Dao.Dto.ERA.ERA20521;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA2;
using EMIC2.Result;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Util.WebClass;

namespace EMIC2.Models.Dao.ERA
{
    public class ERA2Dao : IERA2
    {
        //public class aa
        //{
        //    public int ISSUCCESSFUL { get; set; }
        //    public string MSG { get; set; }
        //}
        /// <summary>
        /// 審核前確認
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult Confirmation(int RPT_MAIN_ID, DateTime? RPT_TIME)
        {
            var Time = RPT_TIME?.ToString("yyyyMMddHHmmss");

            IResult result = new Result.Result(false);
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                var sql = "select ISSUCCESSFUL, MSG from ERA2_CHECK_CONFIRM(@RPT_MAIN_ID, @Time)";
                var query = conn.Query<ERA2_CHECK_CONFIRM>(sql, new { RPT_MAIN_ID = RPT_MAIN_ID, Time = Time }).FirstOrDefault();

                //接回Output值1
                //query.ISSUCCESSFUL = 2;
                if (query.ISSUCCESSFUL == 1)
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                }

                //接回Return值
                result.ReturnValue = query.ISSUCCESSFUL.ToString();
                result.Message = query.MSG;

                return result;

            }
        }

        /// <summary>
        /// 匯入下層資訊，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult LowerLevelCityIntegration(ERA2Dto data)
        {
            IResult result = new Result.Result(false);
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@P_RPT_MAIN_ID", data.RPT_MAIN_ID, dbType: DbType.Int32, direction: ParameterDirection.Input);
                parameters.Add("@P_EOC_ID", data.EOC_ID, dbType: DbType.String, direction: ParameterDirection.Input);
                parameters.Add("@P_PRJ_NO", data.PRJ_NO, dbType: DbType.Int64, direction: ParameterDirection.Input);
                parameters.Add("@O_IsSuccessful", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@O_Msg", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                conn.Execute("[dbo].[ERA2_DOWN_COLLECT]", parameters, commandType: CommandType.StoredProcedure);

                //接回Output值      
                int outputResult = parameters.Get<int>("O_IsSuccessful");
                //outputResult = 2;
                if (outputResult == 1)
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                }

                //接回Return值
                result.ReturnValue = outputResult.ToString();
                //result.Success = false;
                //result.ReturnValue = "2";
                result.Message = parameters.Get<string>("O_Msg");

                return result;
            }
        }

        /// <summary>
        /// 專案查詢
        /// </summary>
        /// <returns>List<ERA2Dto></returns>
        public List<ERA2Dto> ERA2_PROJECT_SEARCH(ERA2Dto data)
        {
            List<ERA2Dto> result = new List<ERA2Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql =
                "select PRJ_NO, EOC_ID, CASE_NAME, PRJ_STIME, PRJ_ETIME, OPEN_LV, dis.DIS_NAME, " +
                "case when PRJ_ETIME is NULL then '開設中' else '已撤除' end as OPEN_STATUS " +
                "from [dbo].[EEM2_EOC_PRJ] as prj " +
                "join EEM2_DIS_DATA as dis " +
                "on prj.DIS_DATA_UID = dis.DIS_DATA_UID " +
                "where EOC_ID =@P_EOC_ID " +
                " and " + "PRJ_STIME >= " + "convert(datetime, @PRJ_STIME_A)" +
                " and " + "ISNULL(PRJ_ETIME,convert(datetime, @PRJ_STIME_B)) <= convert(datetime, @PRJ_STIME_B)" +
                " and " + "prj.DIS_DATA_UID = @DIS_DATA_UID";

                if (data.CASE_NAME != null)
                {
                    sql += " and " + "CASE_NAME like @CASE_NAME";
                }

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID,
                    PRJ_STIME_A = data.RPT_TIME_A,
                    PRJ_STIME_B = data.RPT_TIME_B,
                    DIS_DATA_UID = data.DIS_DATA_UID_DAO,
                    CASE_NAME = $"%{data.CASE_NAME}%"
                };

                var query = conn.Query<ERA2Dto>(sql, parameters);


                foreach (var item in query)
                {
                    var stime = item.PRJ_STIME != null ? item.PRJ_STIME?.ToString("yyyy-MM-dd HH:ss") : null;
                    var etime = item.PRJ_ETIME != null ? " ~ " + item.PRJ_ETIME?.ToString("yyyy-MM-dd HH:ss") : null;

                    item.RPT_TIME_TEXT = stime + etime;
                }

                result = query.ToList();

                return result;
            }
        }

        private int GetTimePara(DateTime dateTime)
        {
            string str = string.Format("{0,4:0000}", dateTime.Year);
            str += string.Format("{0,2:00}", dateTime.Month);
            str += string.Format("{0,2:00}", dateTime.Day);

            return Convert.ToInt32(str);
        }

        /// <summary>
        /// 匯入通報表，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult ImportRptToDisp(string DISP_MAIN_ID, string DISP_DETAIL_ID, string DISP_STYLE_ID, string EOC_LEVEL)
        {
            IResult result = new Result.Result(false);

            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("ERA2_IMP_RPT_TO_DISP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@P_DISP_MAIN_ID", DISP_MAIN_ID);
                    cmd.Parameters.AddWithValue("@P_DISP_DETAIL_ID", DISP_DETAIL_ID);
                    cmd.Parameters.AddWithValue("@P_DISP_STYLE_ID", DISP_STYLE_ID);
                    cmd.Parameters.AddWithValue("@P_EOC_LEVEL", EOC_LEVEL);

                    SqlParameter returnParameter1 = cmd.Parameters.Add("@O_IsSuccessful", SqlDbType.Int);
                    returnParameter1.Direction = ParameterDirection.Output;
                    SqlParameter returnParameter2 = cmd.Parameters.Add("@O_Msg", SqlDbType.NVarChar, 4000);
                    returnParameter2.Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();

                    //接回Output值
                    int outputResult = (int)returnParameter1.Value;
                    //接回Return值
                    var returnResult = returnParameter2.Value.ToString();
                    if (outputResult == 1)
                    {
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = returnResult;
                    }
                    con.Close();
                    con.Dispose();

                    return result;

                }
            }
        }


        /// <summary>
        /// 匯入外部資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult ImportNccToRpt(string P_MTMP_REC_ID, string P_RPT_MAIN_ID, string P_RPT_CODE)
        {
            IResult result = new Result.Result(false);

            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("ERA2_IMP_NCC_TO_RPT", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@P_MTMP_REC_ID", P_MTMP_REC_ID);
                    cmd.Parameters.AddWithValue("@P_RPT_MAIN_ID", P_RPT_MAIN_ID);
                    cmd.Parameters.AddWithValue("@P_RPT_CODE", P_RPT_CODE);

                    SqlParameter returnParameter1 = cmd.Parameters.Add("@O_IsSuccessful", SqlDbType.Int);
                    returnParameter1.Direction = ParameterDirection.Output;
                    SqlParameter returnParameter2 = cmd.Parameters.Add("@O_Msg", SqlDbType.NVarChar, 4000);
                    returnParameter2.Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();

                    //接回Output值
                    int outputResult = (int)returnParameter1.Value;
                    //接回Return值
                    var returnResult = returnParameter2.Value.ToString();
                    if (outputResult == 1)
                    {
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = returnResult;
                    }
                    con.Close();
                    con.Dispose();

                    return result;

                }
            }
        }

        /// <summary>
        /// 匯入外部 F2 - F8 資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult ImportMOTCToRpt(string P_MTMP_REC_ID, string P_RPT_MAIN_ID, string P_RPT_CODE)
        {
            IResult result = new Result.Result(false);

            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {

                // 2019-11-15 修正弱掃 cmd.CommandType = CommandType.StoredProcedure 問題，換一種寫法
                //設定參數
                //DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@P_MTMP_REC_ID", P_MTMP_REC_ID, DbType.String, ParameterDirection.Input);
                //parameters.Add("@P_RPT_MAIN_ID", P_RPT_MAIN_ID, DbType.String, ParameterDirection.Input);
                //parameters.Add("@P_RPT_CODE", P_RPT_CODE, DbType.String, ParameterDirection.Input);

                //parameters.Add("@O_IsSuccessful", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //parameters.Add("@O_Msg", dbType: DbType.String, direction: ParameterDirection.Output,size:4000);

                //con.Execute("ERA2_IMP_MOTC_TO_RPT", parameters, commandType: CommandType.StoredProcedure);
                //int outputResult = parameters.Get<int>("@O_IsSuccessful");
                //var returnResult = parameters.Get<string>("@O_Msg");

                //con.Close();
                //con.Dispose();

                //return result;

                using (SqlCommand cmd = new SqlCommand("ERA2_IMP_MOTC_TO_RPT", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@P_MTMP_REC_ID", P_MTMP_REC_ID);
                    cmd.Parameters.AddWithValue("@P_RPT_MAIN_ID", P_RPT_MAIN_ID);
                    cmd.Parameters.AddWithValue("@P_RPT_CODE", P_RPT_CODE);

                    SqlParameter returnParameter1 = cmd.Parameters.Add("@O_IsSuccessful", SqlDbType.Int);
                    returnParameter1.Direction = ParameterDirection.Output;
                    SqlParameter returnParameter2 = cmd.Parameters.Add("@O_Msg", SqlDbType.NVarChar, 4000);
                    returnParameter2.Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();


                    //接回Output值
                    int outputResult = (int)returnParameter1.Value;
                    //接回Return值
                    var returnResult = returnParameter2.Value.ToString();
                    if (outputResult == 1)
                    {
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = returnResult;
                    }
                    con.Close();
                    con.Dispose();

                    return result;
                }
            }
        }

        /// <summary>
        /// 收容資料查詢 中央 D3, 縣市 D3a, 鄉鎮 D3a, 都要有該功能
        /// </summary>
        /// <returns>List<EEA2_SHELTER></returns>
        public List<EEA2_SHELTER> ERA2_QRY_EEA2_SHELTER(ERA2Dto data)
        {
            List<EEA2_SHELTER> result = new List<EEA2_SHELTER>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql = string.Empty;
                sql = "select * from ERA2_QRY_EEA2_SHELTER (@EOC_ID, @PRJ_NO, @RPT_MAIN_ID, @PRJ_STIME)";


                //data.RPT_MAIN_ID = 525;
                //data.PRJ_STIME_TEXT = "20190907101010";
                var parameters = new
                {
                    EOC_ID = data.EOC_ID,
                    PRJ_NO = data.PRJ_NO,
                    RPT_MAIN_ID = data.RPT_MAIN_ID,
                    PRJ_STIME = data.PRJ_STIME_TEXT,
                };

                var query = conn.Query<EEA2_SHELTER>(sql, parameters);
                result = query.ToList();

                return result;
            }
        }

        /// <summary>
        /// 匯入外部 中央 D3, 縣市 D3a, 鄉鎮 D3a 資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult ERA2_IMP_EEA2_TO_RPT(ERA2Dto data)
        {
            IResult result = new Result.Result(false);

            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("ERA2_IMP_EEA2_TO_RPT", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@P_EOC_ID", data.EOC_ID);
                    cmd.Parameters.AddWithValue("@P_PRJ_NO", data.PRJ_NO);
                    cmd.Parameters.AddWithValue("@P_RPT_CODE", data.RPT_CODE);
                    cmd.Parameters.AddWithValue("@P_RPT_MAIN_ID", data.RPT_MAIN_ID);
                    cmd.Parameters.AddWithValue("@P_TIME_S", data.PRJ_STIME_TEXT);

                    SqlParameter returnParameter1 = cmd.Parameters.Add("@O_IsSuccessful", SqlDbType.Int);
                    returnParameter1.Direction = ParameterDirection.Output;
                    SqlParameter returnParameter2 = cmd.Parameters.Add("@O_Msg", SqlDbType.NVarChar, 4000);
                    returnParameter2.Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();

                    //接回Output值
                    int outputResult = (int)returnParameter1.Value;
                    //接回Return值
                    var returnResult = returnParameter2.Value.ToString();
                    if (outputResult == 1)
                    {
                        result.Success = true;
                        result.Message = returnResult;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = returnResult;
                    }
                    con.Close();
                    con.Dispose();

                    return result;

                }
            }
        }

        /// <summary>
        /// 收容資料查詢 中央 A4, 縣市 A4a, 鄉鎮 A4a 都要有該功能
        /// </summary>
        /// <returns>List<EEA2_EVACUATE></returns>
        public List<EEA2_EVACUATE> ERA2_QRY_EEA2_EVACUATE(ERA2Dto data)
        {
            List<EEA2_EVACUATE> result = new List<EEA2_EVACUATE>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                var sql = "select * from ERA2_QRY_EEA2_EVACUATE (@EOC_ID, @PRJ_NO, @RPT_MAIN_ID, @EVAUCATE_TIME)";


                //data.RPT_MAIN_ID = 525;
                //data.PRJ_STIME_TEXT = "20190907101010";
                var parameters = new
                {
                    EOC_ID = data.EOC_ID,
                    PRJ_NO = data.PRJ_NO,
                    RPT_MAIN_ID = data.RPT_MAIN_ID,
                    EVAUCATE_TIME = data.EVACUATE_TIME_TEXT,
                };

                var query = conn.Query<EEA2_EVACUATE>(sql, parameters);
                result = query.ToList();

                foreach (var item in result)
                {
                    item.CITY_TOWN_VILLAGE_NAME = item.CITY_NAME + "/" + item.TOWN_NAME + "/" + item.VILLAGE_NAME;
                    item.EVACUATE_TIME_TEXT = item.EVACUATE_TIME?.ToString("yyyy-MM-dd HH:mm:ss");
                }

                return result;
            }
        }

        /// <summary>
        /// 外部匯入API C1-C2
        /// 匯入外部資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult ERA2_IMP_EXT_TO_RPT(string P_ATMP_REC_ID, string P_RPT_CODE)
        {
            IResult result = new Result.Result(false);

            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("ERA2_IMP_EXT_TO_RPT", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@P_ATMP_REC_ID", P_ATMP_REC_ID);
                    switch (P_RPT_CODE)
                    {
                        case "C1":
                        case "C2":
                            cmd.Parameters.AddWithValue("@P_EXT_CODE", "COA");
                            break;
                        case "C4":
                            cmd.Parameters.AddWithValue("@P_EXT_CODE", "SWCB");
                            break;
                        case "E2":
                            cmd.Parameters.AddWithValue("@P_EXT_CODE", "TPC");
                            break;
                        case "G1":
                            cmd.Parameters.AddWithValue("@P_EXT_CODE", "MOE");
                            break;
                    }

                    cmd.Parameters.AddWithValue("@P_RPT_CODE", P_RPT_CODE);
                    cmd.Parameters.AddWithValue("@P_EOC_ID", "00000");
                    cmd.Parameters.AddWithValue("@P_PRJ_NO", string.Empty);

                    SqlParameter returnParameter1 = cmd.Parameters.Add("@O_IsSuccessful", SqlDbType.Int);
                    returnParameter1.Direction = ParameterDirection.Output;
                    SqlParameter returnParameter2 = cmd.Parameters.Add("@O_Msg", SqlDbType.NVarChar, 4000);
                    returnParameter2.Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();

                    //接回Output值
                    int outputResult = (int)returnParameter1.Value;
                    //接回Return值
                    var returnResult = returnParameter2.Value.ToString();
                    if (outputResult == 1)
                    {
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = returnResult;
                    }
                    con.Close();
                    con.Dispose();

                    return result;

                }
            }
        }


        /// <summary>
        /// 外部匯入API 小A表
        /// 匯入外部資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult ERA2_IMP_EXT_TO_RPTa(string P_ATMP_REC_ID, string P_RPT_CODE, string P_EOC_ID, string P_PRJ_NO)
        {
            IResult result = new Result.Result(false);

            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("ERA2_IMP_EXT_TO_RPTa", con))
                {
                    DBDataUtility util = new DBDataUtility();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@P_ATMP_REC_ID", P_ATMP_REC_ID);
                    //cmd.Parameters.AddWithValue("@P_EXT_CODE", util.CheckString(string.Empty));
                    cmd.Parameters.AddWithValue("@P_EXT_CODE", "");
                    cmd.Parameters.AddWithValue("@P_RPT_CODE", util.CheckString(P_RPT_CODE));
                    cmd.Parameters.AddWithValue("@P_EOC_ID", util.CheckString(P_EOC_ID));
                    cmd.Parameters.AddWithValue("@P_PRJ_NO", P_PRJ_NO);

                    SqlParameter returnParameter1 = cmd.Parameters.Add("@O_IsSuccessful", SqlDbType.Int);
                    returnParameter1.Direction = ParameterDirection.Output;
                    SqlParameter returnParameter2 = cmd.Parameters.Add("@O_Msg", SqlDbType.NVarChar, 4000);
                    returnParameter2.Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();

                    //接回Output值
                    int outputResult = (int)returnParameter1.Value;
                    //接回Return值
                    var returnResult = returnParameter2.Value.ToString();
                    if (outputResult == 1)
                    {
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = returnResult;
                    }
                    con.Close();
                    con.Dispose();

                    return result;

                }
            }
        }

        /// <summary>
        /// 確認是否為指定的災害應變中心EOC_ID、是否為正確的RPT_CODE 資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public ERA2_CHECK_EXT_TAG ERA2_CHECK_EXT_TAG(string P_EOC_NAME, string P_PRJ_NO, string P_RPT_CODE)
        {
            ERA2_CHECK_EXT_TAG result = new ERA2_CHECK_EXT_TAG();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                var sql = "SELECT * from ERA2_CHECK_EXT_TAG (null, @P_EOC_NAME, @P_PRJ_NO, @P_RPT_CODE)";

                var parameters = new
                {
                    P_EOC_NAME = P_EOC_NAME,
                    P_PRJ_NO = P_PRJ_NO,
                    P_RPT_CODE = P_RPT_CODE,
                };

                result = conn.Query<ERA2_CHECK_EXT_TAG>(sql, parameters).FirstOrDefault();

                return result;
            }
        }
    }
}
