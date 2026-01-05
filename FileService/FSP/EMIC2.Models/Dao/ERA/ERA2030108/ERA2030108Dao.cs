///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030108Dao.cs
//  程式名稱：
//  ERA2030108Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-20       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2030108Dao
///////////////////////////////////////////////////////////////////////////////////////
using Dapper;
using EMIC2.Models.Dao.Dto.ERA;
using EMIC2.Models.Dao.Dto.ERA.ERA2030108;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA2.ERA2030108;
using EMIC2.Result;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EMIC2.Models.Dao.ERA.ERA2030108
{
    public class ERA2030108Dao : IERA2030108Dao
    {
        /// <summary>
        /// 查詢通報表A2
        /// </summary>
        /// <returns> ERA2030108Dto</returns>
        public List<ERA2030108Dto> ERA2_QRY_MAX_A2(ERA2030108SearchModelDto data)
        {
            List<ERA2030108Dto> result = new List<ERA2030108Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql =
                    @"SELECT  
                              ERA_A2.CITY_NAME		 AS         CITY_NAME           --縣市
                             ,ERA_A2.LOW_LEVEL		 AS         DELIMIT_D_NUM 		--低窪地區
                             ,ERA_A2.MOUNTAIN		 AS         DELIMIT_M_NUM		--山區
                             ,ERA_A2.SEASIDE	     AS         DELIMIT_S_NUM		--海邊
                             ,ERA_A2.RELIEVES		 AS         DELIMIT_R_NUM		--河川
                             ,ERA_A2.BUILDS			 AS         DELIMIT_B_NUM		--建築物
                             ,ERA_A2.OTHERS			 AS         DELIMIT_O_NUM		--其他
                             ,ERA_A2.SUGGESTS_ALL	 AS         EC_W_NUM			--勸導執行
                             ,ERA_A2.EXECUTES_ALL	 AS         EC_P_NUM			--開立舉發
                      FROM ERA2_QRY_MAX_A2 (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) AS ERA_A2
                      WHERE NO_DATA_MARK is null";

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID,
                    P_PRJ_NO = data.PRJ_NO,
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID,    //預設 null
                };

                result = conn.Query<ERA2030108Dto>(sql, parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表A2a
        /// </summary>
        /// <returns> ERA2030108Dto</returns>
        public List<ERA2030108Dto> ERA2_QRY_MAX_A2a(ERA2030108SearchModelDto data)
        {
            List<ERA2030108Dto> result = new List<ERA2030108Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql =
                    @"SELECT  
                              ERA_A2.CITY_NAME		 AS         CITY_NAME           --縣市
                             ,ERA_A2.TOWN_NAME       AS         TOWN_NAME           --鄉鎮
                             ,ERA_A2.LOW_LEVEL		 AS         DELIMIT_D_NUM 		--低窪地區
                             ,ERA_A2.MOUNTAIN		 AS         DELIMIT_M_NUM		--山區
                             ,ERA_A2.SEASIDE	     AS         DELIMIT_S_NUM		--海邊
                             ,ERA_A2.RELIEVES		 AS         DELIMIT_R_NUM		--河川
                             ,ERA_A2.BUILDS			 AS         DELIMIT_B_NUM		--建築物
                             ,ERA_A2.OTHERS			 AS         DELIMIT_O_NUM		--其他
                             ,ERA_A2.SUGGESTS_ALL	 AS         EC_W_NUM			--勸導執行
                             ,ERA_A2.EXECUTES_ALL	 AS         EC_P_NUM			--開立舉發
                      FROM ERA2_QRY_MAX_A2a (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) AS ERA_A2
                      WHERE NO_DATA_MARK is null";


                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID,
                    P_PRJ_NO = data.PRJ_NO,
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID,    //預設 null
                };

                result = conn.Query<ERA2030108Dto>(sql, parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 匯入通報表，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult ImportRptToDisp(ERA2030108SearchModelDto data)
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
