///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030113Dao.cs
//  程式名稱：
//  ERA2030113Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-20       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2030113Dao
///////////////////////////////////////////////////////////////////////////////////////
using Dapper;
using EMIC2.Models.Dao.Dto.ERA;
using EMIC2.Models.Dao.Dto.ERA.ERA2030113;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA2.ERA2030113;
using EMIC2.Result;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EMIC2.Models.Dao.ERA.ERA2030113
{
    public class ERA2030113Dao : IERA2030113Dao
    {
        /// <summary>
        /// 查詢通報表A2
        /// </summary>
        /// <returns> ERA2030113Dto</returns>
        public List<ERA2030113Dto> ERA2_QRY_MAX_A4(ERA2030113SearchModelDto data)
        {
            List<ERA2030113Dto> result = new List<ERA2030113Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql =
                    @"SELECT 
                             ERA_A4.CITY_NAME              AS      CITY_NAME,
                             SUM(ERA_A4.EVACUATE_EST)      AS      ESTIMATE_PNUM, 
                             SUM(ERA_A4.EVACUATE_REAL)     AS      RETREAT_PNUM,
                             SUM(ERA_A4.EVACUATE_TOTAL)    AS      SUM_PNUM
                      FROM ERA2_QRY_MAX_A4 (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) AS ERA_A4
                      WHERE NO_DATA_MARK is null
                      GROUP BY CITY_NAME";

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID,
                    P_PRJ_NO = data.PRJ_NO,
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID,    //預設 null
                };

                result = conn.Query<ERA2030113Dto>(sql, parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表A2a
        /// </summary>
        /// <returns> ERA2030113Dto</returns>
        public List<ERA2030113Dto> ERA2_QRY_MAX_A4a(ERA2030113SearchModelDto data)
        {
            List<ERA2030113Dto> result = new List<ERA2030113Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql =
                    @"select 
                             ERA_A4a.CITY_NAME		    AS     CITY_NAME
                            ,ERA_A4a.TOWN_NAME		    AS     TOWN_NAME
                            ,ERA_A4a.VILLAGE_NAME	    AS     VILLAGE_NAME
                            ,ERA_A4a.EVACUATE_TIME	    AS     INPUTDATE
                            ,ERA_A4a.LOCATION		    AS     LOCATION
                            ,ERA_A4a.SHELTER		    AS     SHELTER
                            ,ERA_A4a.EVACUATE_EST       AS     ESTIMATE_PNUM
                            ,ERA_A4a.EVACUATE_REAL      AS     RETREAT_PNUM
                            ,ERA_A4a.EVACUATE_TOTAL     AS     SUM_PNUM
                      FROM ERA2_QRY_MAX_A4a (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) AS ERA_A4a
                      WHERE NO_DATA_MARK is null";

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID,
                    P_PRJ_NO = data.PRJ_NO,
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID,    //預設 null
                };

                result = conn.Query<ERA2030113Dto>(sql, parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 匯入通報表，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult ImportRptToDisp(ERA2030113SearchModelDto data)
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
