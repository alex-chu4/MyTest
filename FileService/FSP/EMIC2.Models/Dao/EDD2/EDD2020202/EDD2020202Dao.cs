using Dapper;
using EMIC2.Models.Dao.Dto.EDD2.EDD2020202;
using EMIC2.Models.Helper;
using EMIC2.Models;
using EMIC2.Models.Interface.EDD2.EDD2020202;
using EMIC2.Result;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace EMIC2.Models.Dao.EDD2.EDD2020202
{
    public class EDD2020202Dao : IEDD2020202Dao
    {
        /// <summary>
        /// 稽催填報查詢
        /// </summary>
        /// <returns>List<EDD2020202Dto></returns>
        public List<EDD2020202Dto> EDD2_020202_M(EDD2020202SearchModelDto model)
        {
            List<EDD2020202Dto> result = new List<EDD2020202Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();

                sql.Append("select * from EDD2_020202_M (@UNIT_ID) ");
                parameters.Add("UNIT_ID", model.UNIT_ID);

                result = conn.Query<EDD2020202Dto>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 匯入外部資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult EDD2_UPDATE_STOCK(EDD2020202SearchModelDto model)
        {
            IResult result = new EMIC2.Result.Result(false);

            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("EDD2_UPDATE_STOCK", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@P_FUNCTION", model.FUNCTION);
                    cmd.Parameters.AddWithValue("@P_ACTION", model.ACTION);
                    cmd.Parameters.AddWithValue("@P_UNIT_ID", model.UNIT_ID);
                    cmd.Parameters.AddWithValue("@P_DATA_TBL", model.DATA_TBL);

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
