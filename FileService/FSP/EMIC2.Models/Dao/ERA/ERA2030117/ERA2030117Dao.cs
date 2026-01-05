///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030117Dao.cs
//  程式名稱：
//  ERA2030117Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-30       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2030117Dao
///////////////////////////////////////////////////////////////////////////////////////
using Dapper;
using EMIC2.Models.Dao.Dto.ERA;
using EMIC2.Models.Dao.Dto.ERA.ERA2030117;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA2.ERA2030117;
using EMIC2.Result;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EMIC2.Models.Dao.ERA.ERA2030117
{
    public class ERA2030117Dao : IERA2030117Dao
    {
        /// <summary>
        /// 查詢通報表A2
        /// </summary>
        /// <returns> ERA2030117Dto</returns>
        public List<ERA2030117Dto> ERA2_QRY_MAX_E1_E2_E4_J2_J3(ERA2030117SearchModelDto data)
        {
            List<ERA2030117Dto> result = new List<ERA2030117Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql =
                    @"SELECT 
                               '瓦斯'                               AS        CODE_NAME
                              ,SUM(ISNULL(GAS_DAMAGE_USED,0))       AS        REPAIR_NUM
                              ,SUM(ISNULL(GAS_DAMAGE_NOW,0))        AS        WAIT_REPAIR
                              ,'' AS REMARK
                      FROM ERA2_QRY_MAX_E1(@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID)
                      UNION ALL
                      SELECT 
                               '電力'                               AS        CODE_NAME
                              ,SUM(ISNULL(ELECTRO_DMG_USED,0))      AS        REPAIR_NUM
                              ,SUM(ISNULL(ELECTRO_DMG_NOW,0))       AS        WAIT_REPAIR
                              ,'' AS REMARK
                      FROM ERA2_QRY_MAX_E2(@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID)
                      UNION ALL
                      SELECT 
                               '自來水'                             AS        CODE_NAME
                              ,SUM(ISNULL(WATER_RECVY,0))           AS        REPAIR_NUM
                              ,SUM(ISNULL(WATER_STOP,0))            AS        WAIT_REPAIR
                              ,''                                   AS        REMARK
                      FROM ERA2_QRY_MAX_E4(@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID)
                      UNION ALL
                      SELECT 
                               '電信(市話)'                         AS        CODE_NAME
                              ,SUM(ISNULL(PHONE_FIXED,0))           AS        REPAIR_NUM
                              ,SUM(ISNULL(PHONE_DAMAGE,0))          AS        WAIT_REPAIR
                              ,''                                   AS        REMARK
                      FROM ERA2_QRY_MAX_J2(@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID)
                      UNION ALL
                      SELECT 
                               '電信(基地台)'                       AS        CODE_NAME
                              ,SUM(ISNULL(SITE_FIXED,0))            AS        REPAIR_NUM
                              ,SUM(ISNULL(SITE_FIXED_NOT,0)) +                
                               SUM(ISNULL(STATION_FIXED_NOT,0)) +             
                               SUM(ISNULL(OTHERS_FIXED_NOT,0))      AS        WAIT_REPAIR
                              ,''                                   AS        REMARK
                      FROM ERA2_QRY_MAX_J3(@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ";

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID,
                    P_PRJ_NO = data.PRJ_NO,
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID,    //預設 null
                };

                result = conn.Query<ERA2030117Dto>(sql, parameters).ToList();

                return result;
            }
        }

       
        /// <summary>
        /// 匯入通報表，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult ImportRptToDisp(ERA2030117SearchModelDto data)
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
