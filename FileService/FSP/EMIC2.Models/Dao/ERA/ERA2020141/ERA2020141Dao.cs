
///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2020141Dao.cs
//  程式名稱：
//  A3通報表下層匯整實作
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-04-26       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  A3通報表下層匯整，時所使用Dao
///////////////////////////////////////////////////////////////////////////////////////

using EMIC2.Models.Dao.Dto.ERA;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA;
using System;
using System.Collections.Generic;
using System.Data;
using EMIC2.Result;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.ERA
{
    public class ERA2020141Dao : IERA2020141Dao
    {
        /// <summary>
        /// 匯入下層資訊，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult LowerLevelCity(ERA2020141Dto data)
        {
            IResult result = new Result.Result(false);
            //using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            //{
            //    //設定參數
            //    DynamicParameters parameters = new DynamicParameters();
            //    parameters.Add("P_RPT_MAIN_ID", data.RPT_MAIN_ID, DbType.Int32, ParameterDirection.Input);
            //    parameters.Add("P_EOC_ID", data.EOC_ID, DbType.String, ParameterDirection.Input);
            //    parameters.Add("P_PRJ_NO", data.PRJ_NO, DbType.Int64, ParameterDirection.Input);
            //    parameters.Add("O_IsSuccessful", SqlDbType.Int, direction: ParameterDirection.Output);
            //    parameters.Add("O_Msg", DbType.Int32, direction: ParameterDirection.Input);
            //    conn.Execute("[dbo].[ERA2_DOWN_COLLECT]", parameters, commandType: CommandType.StoredProcedure);

            //    //接回Output值
            //    int outputResult = parameters.Get<int>("O_IsSuccessful");
            //    //接回Return值
            //    int returnResult = parameters.Get<int>("O_Msg");


            //    return outputResult;
            //}

            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("ERA2_DOWN_COLLECT", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@P_RPT_MAIN_ID", data.RPT_MAIN_ID);
                    cmd.Parameters.AddWithValue("@P_EOC_ID", data.EOC_ID);
                    cmd.Parameters.AddWithValue("@P_PRJ_NO", data.PRJ_NO);

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
