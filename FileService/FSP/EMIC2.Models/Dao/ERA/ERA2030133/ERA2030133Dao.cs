///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030133Dao.cs
//  程式名稱：
//  ERA2030133Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-30       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2030133Dao
///////////////////////////////////////////////////////////////////////////////////////
using Dapper;
using EMIC2.Models.Dao.Dto.ERA;
using EMIC2.Models.Dao.Dto.ERA.ERA2030133;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA2.ERA2030133;
using EMIC2.Result;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EMIC2.Models.Dao.ERA.ERA2030133
{
    public class ERA2030133Dao : IERA2030133Dao
    {
        /// <summary>
        /// 查詢通報表A2
        /// </summary>
        /// <returns> ERA2030133Dto</returns>
        public List<ERA2030133Dto> ERA2_QRY_MAX_F1(ERA2030133SearchModelDto data)
        {
            List<ERA2030133Dto> result = new List<ERA2030133Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql =
                    @"SELECT
                                ERA_F1.CITY_NAME			AS       CITY_NAME
	                           ,ERA_F1.TOWN_NAME			AS       TOWN_NAME
	                           ,ERA_F1.LINECODE			    AS       LINECODE
	                           ,ERA_F1.ADDNAME			    AS       ADDNAME
	                           ,ERA_F1.CLOSE_TEXT		    AS       CLOSE_TEXT
	                           ,ERA_F1.CLOSE_DATETIME	    AS       CLOSE_DATETIME
	                           ,ERA_F1.REPAIRE_DAY   	    AS       REPAIRE_DAY
	                           ,ERA_F1.ALT_ROUTE		    AS       ALT_ROUTE
	                           ,ERA_F1.REMARK			    AS       REMARK
                      FROM ERA2_QRY_MAX_F1(@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ERA_F1
                      WHERE NO_DATA_MARK is null ";

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID,
                    P_PRJ_NO = data.PRJ_NO,
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID,    //預設 null
                };

                result = conn.Query<ERA2030133Dto>(sql, parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表A2
        /// </summary>
        /// <returns> ERA2030133Dto</returns>
        public List<ERA2030133Dto> ERA2_QRY_MAX_F1a(ERA2030133SearchModelDto data)
        {
            List<ERA2030133Dto> result = new List<ERA2030133Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql =
                    @"SELECT
                                ERA_F1a.CITY_NAME			AS       CITY_NAME
	                           ,ERA_F1a.TOWN_NAME			AS       TOWN_NAME
	                           ,ERA_F1a.LINECODE			AS       LINECODE
	                           ,ERA_F1a.ADDNAME			    AS       ADDNAME
	                           ,ERA_F1a.CLOSE_TEXT		    AS       CLOSE_TEXT
	                           ,ERA_F1a.CLOSE_DATETIME	    AS       CLOSE_DATETIME
	                           ,ERA_F1a.REPAIRE_DAY  	    AS       REPAIRE_DAY
	                           ,ERA_F1a.ALT_ROUTE		    AS       ALT_ROUTE
	                           ,ERA_F1a.REMARK			    AS       REMARK
                      FROM ERA2_QRY_MAX_F1a(@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ERA_F1a
                      WHERE NO_DATA_MARK is null ";

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID,
                    P_PRJ_NO = data.PRJ_NO,
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID,    //預設 null
                };

                result = conn.Query<ERA2030133Dto>(sql, parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 匯入通報表，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult ImportRptToDisp(ERA2030133SearchModelDto data)
        {
            IResult result = new Result.Result(false);


            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("ERA2_IMP_RPT_TO_DISP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@P_DISP_MAIN_ID", data.DISP_MAIN_ID);
                    cmd.Parameters.AddWithValue("@P_DISP_DETAIL_ID", data.DISP_DETAIL_ID);
                    cmd.Parameters.AddWithValue("@P_DISP_STYLE_ID", data.DISP_STYLE_ID);
                    cmd.Parameters.AddWithValue("@P_EOC_LEVEL", data.EOC_LEVEL);

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
    }
}
