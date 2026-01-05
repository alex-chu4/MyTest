///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030118Dao.cs
//  程式名稱：
//  ERA2030118Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-29       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2030118Dao
///////////////////////////////////////////////////////////////////////////////////////
using Dapper;
using EMIC2.Models.Dao.Dto.ERA;
using EMIC2.Models.Dao.Dto.ERA.ERA2030118;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA2.ERA2030118;
using EMIC2.Result;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EMIC2.Models.Dao.ERA.ERA2030118
{
    public class ERA2030118Dao : IERA2030118Dao
    {
        /// <summary>
        /// 查詢通報表A2
        /// </summary>
        /// <returns> ERA2030118Dto</returns>
        public List<ERA2030118Dto> ERA2_QRY_MAX_F1s(ERA2030118SearchModelDto data)
        {
            List<ERA2030118Dto> result = new List<ERA2030118Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql =
                    @"SELECT 
        B.CODE_NAME			 AS       ROADTYPE_NAME
       ,A.BLOCK_TOTAL		 AS       BLOCK_TOTAL
       ,A.UNBLOCK_COUNT		 AS       FINISH_REPAIR
       ,A.BLOCK_COUNT		 AS       ON_REPAIR
       ,A.COMMENT			 AS       REMARK
FROM(
       SELECT ROADTYPE_ID, BLOCK_TOTAL, UNBLOCK_COUNT, BLOCK_COUNT, COMMENT
       FROM ERA2_QRY_MAX_F1_S(@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ) A
       LEFT JOIN(
                    SELECT CODE_VALUE, CODE_NAME
                    FROM ERA2_CODETABLE
                    WHERE CODE_USEEN = 'ROADTYPE') B ON A.ROADTYPE_ID = B.CODE_VALUE ";

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID,
                    P_PRJ_NO = data.PRJ_NO,
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID,    //預設 null
                };

                result = conn.Query<ERA2030118Dto>(sql, parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 匯入通報表，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult ImportRptToDisp(ERA2030118SearchModelDto data)
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
