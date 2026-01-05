using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA;
using EMIC2.Result;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Transactions;
using Util.WebClass;

namespace EMIC2.Models.Dao.ERA
{
    public class ERA20302Dao : ERA203Dao, IERA20302Dao
    {
        /// <summary>
        /// 查詢最新報表資料 ok
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <returns>資料集</returns>
        public List<List<object>> ERA2_0302_M(string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID, string p_USER_ID)
        {
            List<string> inputParas = new List<string>()
            {
                "@P_EOC_ID", "@P_PRJ_NO", "@P_ORG_ID"
            };

            List<object> inputParaValues = new List<object>()
            {
                p_EOC_ID, p_PRJ_NO, p_ORG_ID
            };

            //string query = ConcatSelectFuncQuery("[dbo].[ERA2_0302_M]", parameters);
            string query = ConcatSelectQuery("[dbo].[ERA2_0302_M]", inputParas, useEnd: true);

            IResult result = GetDispMainData(out List<List<object>> tbData, query, inputParas, inputParaValues, p_USER_ID);

            return tbData;
        }

        /// <summary>
        /// 查詢最新報表資料
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <returns>資料集</returns>
        public List<List<object>> ERA2_0302_STYLE(string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID, int p_DISP_MAIN_ID)
        {
            List<string> inputParas = new List<string>()
            {
                "@P_EOC_ID",
                "@P_PRJ_NO",
                "@P_ORG_ID",
                "@P_DISP_MAIN_ID"
            };

            List<object> inputParaValues = new List<object>()
            {
                p_EOC_ID,
                p_PRJ_NO,
                p_ORG_ID,
                p_DISP_MAIN_ID
            };

            string query = ConcatSelectQuery("[dbo].[ERA2_0302_STYLE]", inputParas, useEnd: true);

            IResult result = GetTableDataWithParameter(out List<List<object>> tbData, query, inputParas, inputParaValues);

            return tbData;
        }

        /// <summary>
        /// 查詢單筆明細資料
        /// </summary>
        /// <param name="p_DISP_MAIN_ID">主表Id</param>
        /// <param name="p_DISP_STYLE_ID">明細style Id</param>
        /// <param name="p_DISP_CAT_ID"></param>
        /// <param name="p_EOC_LEVEL"></param>
        /// <returns></returns>
        public List<List<dynamic>> ERA2_GET_DISP_DATA_ONE(int p_DISP_MAIN_ID,int p_DISP_STYLE_ID,string p_DISP_CAT_ID,string p_EOC_LEVEL)
        {
            List<string> parameters = new List<string>()
            {
                "@P_DISP_MAIN_ID",
                "@P_DISP_STYLE_ID",
                "@P_DISP_CAT_ID",
                "@P_EOC_LEVEL"
            };

            List<object> paraValues = new List<object>()
            {
                p_DISP_MAIN_ID,
                p_DISP_STYLE_ID,
                p_DISP_CAT_ID,
                p_EOC_LEVEL
            };

            IResult result = GetTableCollectionWithParameter(out List<List<dynamic>> dataCollection, "dbo.[ERA2_GET_DISP_DATA_ONE]", parameters, paraValues, CommandType.StoredProcedure);

            return dataCollection;
        }

        /// <summary>
        /// 查詢多筆明細資料
        /// </summary>
        /// <param name="p_DISP_MAIN_ID">主表Id</param>
        /// <param name="p_TITLE_ID">頁籤id</param>
        /// <param name="p_DISP_CAT_ID"></param>
        /// <param name="p_EOC_LEVEL"></param>
        /// <returns></returns>
        public List<List<dynamic>> ERA2_GET_DISP_DATA_BLOCK(int p_DISP_MAIN_ID, int p_TITLE_ID, string p_DISP_CAT_ID, string p_EOC_LEVEL)
        {
            List<string> parameters = new List<string>()
            {
                "@P_DISP_MAIN_ID",
                "@P_TITLE_ID",
                "@P_DISP_CAT_ID",
                "@P_EOC_LEVEL"
            };

            List<object> paraValues = new List<object>()
            {
                p_DISP_MAIN_ID,
                p_TITLE_ID,
                p_DISP_CAT_ID,
                p_EOC_LEVEL
            };

            IResult result = GetTableCollectionWithParameter(out List<List<dynamic>> dataCollection, "dbo.[ERA2_GET_DISP_DATA_BLOCK]", parameters, paraValues, CommandType.StoredProcedure);

            return dataCollection;
        }

        /// <summary>
        /// 製作處置報告
        /// </summary>
        /// <param name="data">製作完成資料</param>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <param name="p_MODIFIED_USER"></param>
        /// <returns>製作狀態</returns>
        public IResult ERA2_SetDispMain(out List<List<object>> data, string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID, string p_MODIFIED_USER)
        {
            List<string> selectParas = new List<string>()
            {
                "@P_EOC_ID", "@P_PRJ_NO", "@P_ORG_ID"
            };

            List<object> selectParaValues = new List<object>()
            {
                p_EOC_ID, p_PRJ_NO, p_ORG_ID
            };

            List<string> updateParas = new List<string>()
            {
                "@DISP_TIME", "@MODIFIED_USER", "@MODIFIED_TIME"
            };

            List<object> updateParaValues = new List<object>()
            {
                DateTime.Now, p_MODIFIED_USER, DateTime.Now
            };

            List<string> whereParas = new List<string>()
            {
                "@DISP_MAIN_ID"
            };

            string querySelect = ConcatSelectQuery("[dbo].[ERA2_0302_M]", selectParas, useEnd: true);
            string queryUpdate = ConcatUpdateQuery("[dbo].[ERA2_DISP_MAIN]", updateParas, whereParas);

            IResult result = SetDispMainStatus(out data, 
                querySelect, selectParas, selectParaValues,
                queryUpdate, updateParas, updateParaValues, whereParas);

            return result;
        }

        // ---------- Private function ----------
        /// <summary>
        /// 查詢最新主表資料
        /// </summary>
        /// <param name="data">主表資料</param>
        /// <param name="query">查詢條件</param>
        /// <param name="parameters"></param>
        /// <param name="paraValues"></param>
        private IResult GetDispMainData(out List<List<object>> data, string query, List<string> parameters, List<object> paraValues, string userID)
        {
            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                data = new List<List<object>>();
                IResult result = new Result.Result(false);
                // Select var
                SqlDataReader dr;
                SqlCommand cmdSelect = new SqlCommand(query, con);
                // Insert var
                List<string> insertPara = new List<string>()
                {
                    "@PRJ_NO",
                    "@EOC_ID",
                    "@DIS_NAME",
                    "@ORG_LEVEL_ID",
                    "@DISP_NO",
                    "@DIS_DATA_UID",
                    "@CREATED_USER",
                    "@CREATED_TIME",
                    "@MODIFIED_USER",
                    "@MODIFIED_TIME"
                };
                var queryInsert = ConcatInsertQuery("[dbo].[ERA2_DISP_MAIN]", insertPara);
                SqlCommand cmdInsert = new SqlCommand(queryInsert, con);
                int insertPk = 0;

                using (TransactionScope scope = new TransactionScope())
                {
                    cmdSelect.CommandType = CommandType.Text;
                    for (int i = 0; i < parameters.Count; i++)
                        cmdSelect.Parameters.AddWithValue(parameters[i], paraValues[i]);

                    try
                    {
                        if (con.State == System.Data.ConnectionState.Closed)
                            con.Open();

                        dr = cmdSelect.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                List<object> row = new List<object>();
                                for (int i = 0; i < dr.FieldCount; i++)
                                    row.Add(dr.GetValue(i));
                                data.Add(row);
                            }

                            result.Success = true;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Not data.";
                        }
                        
                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Exception = ex;
                        result.Message = "Failed.";
                    }


                    if (data.Count == 1 && result.Success)
                    {
                        if (data[0][0].GetType().Name == "DBNull")
                        {
                            DateTime currentTime = DateTime.Now;
                            //List<object> valPara = new List<object>()
                            //{
                            //    data[0][1],
                            //    data[0][2],
                            //    data[0][3],
                            //    data[0][4],
                            //    data[0][5],
                            //    data[0][6],
                            //    userID,
                            //    currentTime,
                            //    userID,
                            //    currentTime,
                            //};
                            cmdInsert.CommandType = CommandType.Text;
                            DBDataUtility util = new DBDataUtility();
                            cmdInsert.Parameters.AddWithValue(insertPara[0], Convert.ToInt64(util.CheckString(data[0][1])));
                            cmdInsert.Parameters.AddWithValue(insertPara[1], util.CheckString(data[0][2]));
                            cmdInsert.Parameters.AddWithValue(insertPara[2], util.CheckString(data[0][3]));
                            cmdInsert.Parameters.AddWithValue(insertPara[3], Convert.ToInt32(util.CheckString(data[0][4])));
                            cmdInsert.Parameters.AddWithValue(insertPara[4], Convert.ToInt32(util.CheckString(data[0][5])));
                            cmdInsert.Parameters.AddWithValue(insertPara[5], Convert.ToInt32(util.CheckString(data[0][6])));
                            cmdInsert.Parameters.AddWithValue(insertPara[6], userID);
                            cmdInsert.Parameters.AddWithValue(insertPara[7], currentTime);
                            cmdInsert.Parameters.AddWithValue(insertPara[8], userID);
                            cmdInsert.Parameters.AddWithValue(insertPara[9], currentTime);
                            
                            //for (int i = 0; i < insertPara.Count; i++)
                            //    cmdInsert.Parameters.AddWithValue(insertPara[i], valPara[i]);

                            try
                            {
                                insertPk = (int)cmdInsert.ExecuteScalar();
                                
                                data[0][0] = insertPk;
                            }
                            catch (Exception ex)
                            {
                                result.Success = false;
                                result.Exception = ex;
                                result.Message = "Failed.";
                            }
                        }
                    }
                    else
                    {
                        data = new List<List<object>>();
                        result.Success = false;
                        result.Message = "Data error.";
                    }

                    scope.Complete();
                }

                cmdSelect.Dispose();
                cmdInsert.Dispose();
                con.Close();
                con.Dispose();

                return result;
            }
        }

        /// <summary>
        /// 製作處置報表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="querySelect"></param>
        /// <param name="selectParas"></param>
        /// <param name="selectParaValues"></param>
        /// <param name="queryUpdate"></param>
        /// <param name="updateParas"></param>
        /// <param name="updateParaValues"></param>
        /// <param name="whereParas"></param>
        /// <returns></returns>
        private IResult SetDispMainStatus(
            out List<List<object>> data, 
            string querySelect, 
            List<string> selectParas, 
            List<object> selectParaValues,
            string queryUpdate,
            List<string> updateParas,
            List<object> updateParaValues,
            List<string> whereParas)
        {
            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                data = new List<List<object>>();
                IResult result = new Result.Result(false);
                // select
                SqlDataReader dr;
                SqlCommand cmdSelect = new SqlCommand(querySelect, con);
                cmdSelect.CommandType = CommandType.Text;
                for (int i = 0; i < selectParas.Count; i++)
                    cmdSelect.Parameters.AddWithValue(selectParas[i], selectParaValues[i]);

                //update
                int executedSuccess = 0;
                SqlCommand cmdUpdate = new SqlCommand(queryUpdate, con);
                cmdUpdate.CommandType = CommandType.Text;
                for (int i = 0; i < updateParas.Count; i++)
                    cmdUpdate.Parameters.AddWithValue(updateParas[i], updateParaValues[i]);

                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        if (con.State == System.Data.ConnectionState.Closed)
                            con.Open();

                        dr = cmdSelect.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                List<object> row = new List<object>();
                                for (int i = 0; i < dr.FieldCount; i++)
                                    row.Add(dr.GetValue(i));
                                data.Add(row);
                            }

                            result.Success = true;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Not main info data.";
                        }
                        dr.Close();
                        DBDataUtility util = new DBDataUtility();
                        if (result.Success)
                        {
                            if ("1" == data[0][10].ToString())
                            {
                                cmdUpdate.Parameters.AddWithValue(whereParas[0], Convert.ToInt64(util.CheckString(data[0][0])));
                                //for (int i = 0; i < whereParas.Count; i++)
                                //    cmdUpdate.Parameters.AddWithValue(whereParas[i], data[0][0]);

                                executedSuccess = cmdUpdate.ExecuteNonQuery();

                                result.Success = true;
                            }
                            else
                            {
                                result.Success = false;
                                result.Message = "Status: 0.\n" + data[0][11].ToString();
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Exception = ex;
                        result.Message = "Failed.";
                    }

                    
                    scope.Complete();
                }

                cmdSelect.Dispose();
                cmdUpdate.Dispose();
                con.Close();
                con.Dispose();

                return result;
            }
        }
    }
}
