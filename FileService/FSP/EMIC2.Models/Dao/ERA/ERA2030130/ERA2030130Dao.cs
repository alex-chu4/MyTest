///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030130Dao.cs
//  程式名稱：
//  ERA2030130Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-20       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2030130Dao
///////////////////////////////////////////////////////////////////////////////////////
using Dapper;
using EMIC2.Models.Dao.Dto.ERA;
using EMIC2.Models.Dao.Dto.ERA.ERA2030130;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA2.ERA2030130;
using EMIC2.Result;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EMIC2.Models.Dao.ERA.ERA2030130
{
    public class ERA2030130Dao : IERA2030130Dao
    {
        /// <summary>
        /// 查詢通報表A2
        /// </summary>
        /// <returns> ERA2030130Dto</returns>
        public List<ERA2030130Dto> ERA2_QRY_MAX_H1(ERA2030130SearchModelDto data)
        {
            List<ERA2030130Dto> result = new List<ERA2030130Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql =
                    @"SELECT
                       ERA_H1.CITY_NAME				AS       CITY_NAME		 --縣市
					  ,ERA_H1.TOWN_NAME				AS       TOWN_NAME		 --鄉鎮
					  ,ERA_H1.MILITARY_CASES		AS       COUNTRYARMY	 --國軍	    執行案件
					  ,ERA_H1.AIRBORNE_CASES		AS       AIRDUTY		 --空勤總隊	執行案件
					  ,ERA_H1.MILITARY_SUPPLY		AS       COUNTRYARMY1	 --國軍	    物資運送(公斤)
					  ,ERA_H1.AIRBORNE_SUPPLY		AS       AIRDUTY1		 --空勤總隊	物資運送(公斤)
					  ,ERA_H1.MILITARY_TRANSPORT	AS       COUNTRYARMY2	 --國軍	    運送救災人員(人)
					  ,ERA_H1.AIRBORNE_TRANSPORT	AS       AIRDUTY2		 --空勤總隊 運送救災人員(人)
					  ,ERA_H1.MILITARY_INJURED		AS       COUNTRYARMY3	 --國軍	    傷患及災民救援後送(人)
					  ,ERA_H1.AIRBORNE_INJURED		AS       AIRDUTY3		 --空勤總隊 傷患及災民救援後送(人)
					  ,ERA_H1.MILITARY_SURVEY		AS       COUNTRYARMY4	 --國軍	    空勘
					  ,ERA_H1.AIRBORNE_SURVEY		AS       AIRDUTY4		 --空勤總隊 空勘
					  ,ERA_H1.MILITARY_DEAD			AS       COUNTRYARMY5	 --國軍	    屍體運送
					  ,ERA_H1.AIRBORNE_DEAD			AS       AIRDUTY5		 --空勤總隊 屍體運送
					  ,ERA_H1.MILITARY_WORKERS		AS       COUNTRYARMY6	 --國軍	    出動空中救災人次
					  ,ERA_H1.AIRBORNE_WORKERS		AS       AIRDUTY6		 --空勤總隊 出動空中救災人次
					  ,ERA_H1.MILITARY_FLIGHTS		AS       COUNTRYARMY7	 --國軍	    出動總架次
					  ,ERA_H1.AIRBORNE_FLIGHTS		AS       AIRDUTY7		 --空勤總隊 出動總架次
                      FROM ERA2_QRY_MAX_H1 (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) AS ERA_H1
                      WHERE NO_DATA_MARK is null";

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID,
                    P_PRJ_NO = data.PRJ_NO,
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID,    //預設 null
                };

                result = conn.Query<ERA2030130Dto>(sql, parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 匯入通報表，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult ImportRptToDisp(ERA2030130SearchModelDto data)
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
