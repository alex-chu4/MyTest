///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030114Dao.cs
//  程式名稱：
//  ERA2030114Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-20       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2030114Dao
///////////////////////////////////////////////////////////////////////////////////////
using Dapper;
using EMIC2.Models.Dao.Dto.ERA;
using EMIC2.Models.Dao.Dto.ERA.ERA2030114;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA2.ERA2030114;
using EMIC2.Result;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EMIC2.Models.Dao.ERA.ERA2030114
{
    public class ERA2030114Dao : IERA2030114Dao
    {
        /// <summary>
        /// 查詢通報表A2
        /// </summary>
        /// <returns> ERA2030114Dto</returns>
        public List<ERA2030114Dto> ERA2_QRY_MAX_D3(ERA2030114SearchModelDto data)
        {
            List<ERA2030114Dto> result = new List<ERA2030114Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql =
                    @"SELECT
                       ERA_D3.CITY_NAME				AS        CITY_NAME					--縣市
					  ,ERA_D3.TOWN_NAME				AS        TOWN_NAME					--鄉鎮
					  ,ERA_D3.SHELTER				AS		  ACCCEPT_PLACE				--收容場所
					  ,ERA_D3.OPEN_DATETIME			AS		  ESTABLISH_TIME			--開設時間
					  ,ERA_D3.CLOSE_DATETIME		AS		  RETREAT_TIME				--撤離時間
					  ,ERA_D3.REFUGEE_M				AS		  ACCEPT_PEOPLE_MALE		--目前收容人數(男)
					  ,ERA_D3.REFUGEE_F				AS		  ACCEPT_PEOPLE_FEMALE		--目前收容人數(女)
					  ,ERA_D3.REFUGEE_TOTAL_M		AS		  COUNT_PEOPLE_MALE			--累計收容人數(男)
					  ,ERA_D3.REFUGEE_TOTAL_F		AS		  COUNT_PEOPLE_FEMALE		--累計收容人數(女)
					  ,ERA_D3.SUPPLY_PEOPLE			AS		  PEOPLE_FOOD_NUM			--目前儲糧預估可再供應狀況-人數
					  ,ERA_D3.SUPPLY_DAY			AS		  PEOPLE_FOOD_DAY			--目前儲糧預估可再供應狀況-日數
					  ,ERA_D3.KEEP_SUPPLY			AS		  TRUE_FALSE_CHOOSE			--是否以開口契約或民間團體持續供應
                      FROM ERA2_QRY_MAX_D3 (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) AS ERA_D3
                      WHERE NO_DATA_MARK is null";

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID,
                    P_PRJ_NO = data.PRJ_NO,
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID,    //預設 null
                };

                result = conn.Query<ERA2030114Dto>(sql, parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表A2a
        /// </summary>
        /// <returns> ERA2030114Dto</returns>
        public List<ERA2030114Dto> ERA2_QRY_MAX_D3a(ERA2030114SearchModelDto data)
        {
            List<ERA2030114Dto> result = new List<ERA2030114Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql =
                    @"SELECT  
                       ERA_D3a.CITY_NAME				AS        CITY_NAME					--縣市
					  ,ERA_D3a.TOWN_NAME				AS        TOWN_NAME					--鄉鎮
					  ,ERA_D3a.SHELTER			    	AS		  ACCCEPT_PLACE				--收容場所
					  ,ERA_D3a.OPEN_DATETIME			AS		  ESTABLISH_TIME			--開設時間
					  ,ERA_D3a.CLOSE_DATETIME		    AS		  RETREAT_TIME				--撤離時間
					  ,ERA_D3a.REFUGEE_M				AS		  ACCEPT_PEOPLE_MALE		--目前收容人數(男)
					  ,ERA_D3a.REFUGEE_F				AS		  ACCEPT_PEOPLE_FEMALE		--目前收容人數(女)
					  ,ERA_D3a.REFUGEE_TOTAL_M		    AS		  COUNT_PEOPLE_MALE			--累計收容人數(男)
					  ,ERA_D3a.REFUGEE_TOTAL_F		    AS		  COUNT_PEOPLE_FEMALE		--累計收容人數(女)
					  ,ERA_D3a.SUPPLY_PEOPLE			AS		  PEOPLE_FOOD_NUM			--目前儲糧預估可再供應狀況-人數
					  ,ERA_D3a.SUPPLY_DAY			    AS		  PEOPLE_FOOD_DAY			--目前儲糧預估可再供應狀況-日數
					  ,ERA_D3a.KEEP_SUPPLY			    AS		  TRUE_FALSE_CHOOSE			--是否以開口契約或民間團體持續供應
                      FROM ERA2_QRY_MAX_D3a (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) AS ERA_D3a
                      WHERE NO_DATA_MARK is null";

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID,
                    P_PRJ_NO = data.PRJ_NO,
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID,    //預設 null
                };

                result = conn.Query<ERA2030114Dto>(sql, parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 匯入通報表，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult ImportRptToDisp(ERA2030114SearchModelDto data)
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
