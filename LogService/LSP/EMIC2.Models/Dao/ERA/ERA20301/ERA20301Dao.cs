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
    public class ERA20301Dao : ERA203Dao, IERA20301Dao
    {
        /// <summary>
        /// 查詢最新報表資料 ok
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <param name="p_USER_ID"></param>
        /// <returns>資料集</returns>
        public IResult ERA2_0301_M(out List<List<object>> tbData, string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID, string p_USER_ID)
        {
            List<string> inputParas = new List<string>()
            {
                "@P_EOC_ID", "@P_PRJ_NO", "@P_ORG_ID"
            };

            List<object> inputParaValues = new List<object>()
            {
                p_EOC_ID, p_PRJ_NO, p_ORG_ID
            };

            // string query = ConcatSelectFuncQuery("[dbo].[ERA2_0301_M]", parameters);
            string query = ConcatSelectQuery("[dbo].[ERA2_0301_M]", inputParas, useEnd: true);

            IResult result = GetDispMainData(out tbData, query, inputParas, inputParaValues, p_USER_ID);

            return result;
        }

        /// <summary>
        /// 查詢最新報表資料 ok
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <param name="p_DISP_MAIN_ID"></param>
        /// <returns>資料集</returns>
        public IResult ERA2_0301_STYLE(out List<List<object>> tbData, string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID, int p_DISP_MAIN_ID)
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

            string query = ConcatSelectQuery("[dbo].[ERA2_0301_STYLE]", inputParas, useEnd: true);

            IResult result = GetTableDataWithParameter(out tbData, query, inputParas, inputParaValues);

            return result;
        }

        /// <summary>
        /// 查詢單筆明細資料
        /// </summary>
        /// <param name="p_DISP_MAIN_ID">主表Id</param>
        /// <param name="p_DISP_STYLE_ID">明細style Id</param>
        /// <param name="p_DISP_CAT_ID"></param>
        /// <param name="p_EOC_LEVEL"></param>
        /// <returns></returns>
        public IResult ERA2_GET_DISP_DATA_ONE(out List<List<dynamic>> dataCollection, int p_DISP_MAIN_ID, int p_DISP_STYLE_ID, string p_DISP_CAT_ID, string p_EOC_LEVEL)
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

            IResult result = GetTableCollectionWithParameter(out dataCollection, "dbo.[ERA2_GET_DISP_DATA_ONE]", parameters, paraValues, CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// 查詢多筆明細資料
        /// </summary>
        /// <param name="p_DISP_MAIN_ID">主表Id</param>
        /// <param name="p_TITLE_ID">頁籤id</param>
        /// <param name="p_DISP_CAT_ID"></param>
        /// <param name="p_EOC_LEVEL"></param>
        /// <returns></returns>
        public IResult ERA2_GET_DISP_DATA_BLOCK(out List<List<dynamic>> dataCollection, int p_DISP_MAIN_ID, int p_TITLE_ID, string p_DISP_CAT_ID, string p_EOC_LEVEL)
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

            IResult result = GetTableCollectionWithParameter(out dataCollection, "dbo.[ERA2_GET_DISP_DATA_BLOCK]", parameters, paraValues, CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// 檢查設置無資料可填報
        /// </summary>
        /// <param name="result"></param>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <param name="p_DISP_MAIN_ID">主表Id</param>
        /// <param name="p_DISP_STYLE_ID">明細style Id</param>
        /// <returns></returns>
        public IResult ERA2_0301_D_NoData(out List<List<object>> tbData, string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID, int p_DISP_MAIN_ID, int p_DISP_STYLE_ID, string p_USER_ID)
        {
            List<string> inputParas = new List<string>()
            {
                "@P_EOC_ID", "@P_PRJ_NO", "@P_ORG_ID", "@P_DISP_MAIN_ID", "@P_DISP_STYLE_ID"
            };

            List<object> inputParaValues = new List<object>()
            {
                p_EOC_ID, p_PRJ_NO, p_ORG_ID, p_DISP_MAIN_ID, p_DISP_STYLE_ID
            };

            string query = ConcatSelectQuery("[dbo].[ERA2_0301_D]", inputParas, useEnd: true);

            IResult result = SetNoDataMark(out tbData, query, inputParas, inputParaValues, p_USER_ID);

            return result;
        }

        /// <summary>
        /// 檢查維護狀態
        /// </summary>
        /// <param name="result"></param>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <param name="p_DISP_MAIN_ID">主表Id</param>
        /// <param name="p_DISP_STYLE_ID">明細style Id</param>
        /// <returns></returns>
        public IResult ERA2_0301_D_maintain(out List<List<object>> tbData, string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID, int p_DISP_MAIN_ID, int p_DISP_STYLE_ID, string p_USER_ID)
        {
            List<string> inputParas = new List<string>()
            {
                "@P_EOC_ID", "@P_PRJ_NO", "@P_ORG_ID", "@P_DISP_MAIN_ID", "@P_DISP_STYLE_ID"
            };

            List<object> inputParaValues = new List<object>()
            {
                p_EOC_ID, p_PRJ_NO, p_ORG_ID, p_DISP_MAIN_ID, p_DISP_STYLE_ID
            };

            string query = ConcatSelectQuery("[dbo].[ERA2_0301_D]", inputParas, useEnd: true);

            IResult result = CheckMaintainStatue(out tbData, query, inputParas, inputParaValues, p_USER_ID);

            return result;
        }

        /// <summary>
        /// 歷史紀錄查詢
        /// </summary>
        /// <param name="result"></param>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_DISP_NO"></param>
        /// <param name="p_DISP_STYLE_ID"></param>
        /// <param name="p_DISP_CAT_ID"></param>
        /// <param name="p_EOC_LEVEL"></param>
        /// <returns></returns>
        public IResult ERA2_history(out List<List<dynamic>> tbData, string p_EOC_ID, long p_PRJ_NO, int p_DISP_NO, int p_DISP_STYLE_ID, string p_DISP_CAT_ID, string p_EOC_LEVEL)
        {
            List<string> parametersMainId = new List<string>()
            {
                "@DISP_MAIN_ID"
            };

            List<string> whereParasMainId = new List<string>()
            {
                "@EOC_ID", "@PRJ_NO", "@DISP_NO"
            };

            List<object> whereParaValuesMainId = new List<object>()
            {
                p_EOC_ID, p_PRJ_NO, p_DISP_NO
            };

            List<string> parametersDataOne = new List<string>()
            {
                "@P_DISP_MAIN_ID",
                "@P_DISP_STYLE_ID",
                "@P_DISP_CAT_ID",
                "@P_EOC_LEVEL"
            };

            List<object> paraValuesDataOne = new List<object>()
            {
                null,
                p_DISP_STYLE_ID,
                p_DISP_CAT_ID,
                p_EOC_LEVEL
            };

            string queryMainId = ConcatSelectQuery("[dbo].[ERA2_DISP_MAIN]", selectParas: parametersMainId);
            queryMainId = ConcatWhereQuery(queryMainId, useEnd: true, whereAndParas: whereParasMainId);

            IResult result = GetHistoryData(
                out tbData,
                queryMainId,
                whereParasMainId,
                whereParaValuesMainId,
                "dbo.[ERA2_GET_DISP_DATA_ONE]",
                parametersDataOne,
                paraValuesDataOne);

            return result;
        }

        //******************************for export word******************************

        /// <summary>
        /// 查詢最新報表資料 FOR 匯出
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="ORG_LEVEL_ID"></param>
        /// <param name="p_USER_ID"></param>
        /// <returns>資料集</returns>
        public IResult ERA2_0301_Export(out List<List<object>> tbData, string p_EOC_ID, long p_PRJ_NO, string ORG_LEVEL_ID, int DISP_NO)
        {
            List<string> inputParas = new List<string>()
            {
                "@PRJ_NO", "@EOC_ID", "@ORG_LEVEL_ID","@DISP_NO"
            };

            List<object> inputParaValues = new List<object>()
            {
               p_PRJ_NO, p_EOC_ID,  ORG_LEVEL_ID,DISP_NO
            };

            string query = @"select DISP_MAIN_ID, PRJ_NO, EOC_ID,case when DIS_DATA_UID = 11 then 'T'
            when DIS_DATA_UID = 13 then 'Q' else 'U' end DISP_CAT_ID,
            DIS_DATA_UID, DIS_NAME, DISP_NO, ISNULL(DISP_TIME, getdate()) DISP_TIME
            from [dbo].[ERA2_DISP_MAIN]
            where PRJ_NO = @PRJ_NO
            and EOC_ID = @EOC_ID
            and ORG_LEVEL_ID =@ORG_LEVEL_ID
            and DISP_NO = @DISP_NO";
            //STEP 1:先抓主檔相關KEY(Q: 震災 T: 風災 U: 通用)
            IResult result = GetTableDataWithParameter(
                 out tbData,
                 query,
                inputParas,
                inputParaValues);

            return result;
        }

        /// <summary>
        /// 查ITEM_LEVEL = 1
        /// </summary>
        /// <param name="tbData"></param>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <param name="p_DISP_MAIN_ID"></param>
        /// <returns></returns>
        public IResult ERA2_0301_STYLE_TITLE(out List<List<object>> tbData, string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID, int p_DISP_MAIN_ID)
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

            string query = @"SELECT * FROM [dbo].[ERA2_0302_STYLE](@P_EOC_ID,@P_PRJ_NO,@P_ORG_ID,@P_DISP_MAIN_ID) where ITEM_LEVEL = 1";

            IResult result = GetTableDataWithParameter(out tbData, query, inputParas, inputParaValues);

            return result;
        }

        /// <summary>
        /// 查詢多筆明細資料
        /// </summary>
        /// <param name="p_DISP_MAIN_ID">主表Id</param>
        /// <param name="p_TITLE_ID">頁籤id</param>
        /// <param name="p_DISP_CAT_ID"></param>
        /// <param name="p_EOC_LEVEL"></param>
        /// <returns></returns>
        public IResult ERA2_GET_DISP_DATA_BLOCK_DS(out DataSet dataCollection, int p_DISP_MAIN_ID, int p_TITLE_ID, string p_DISP_CAT_ID, string p_EOC_LEVEL)
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

            IResult result = GetDataSetWithParameter(out DataSet DS, "dbo.[ERA2_GET_DISP_DATA_BLOCK]", parameters, paraValues, CommandType.StoredProcedure);
            dataCollection = DS;
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
                        result.Message = ex.Message;
                    }

                    if (data.Count == 1 && result.Success)
                    {
                        //if (Convert.ToInt32(data[0][10]) != 0)
                        //{
                        if (data[0][0].GetType().Name == "DBNull")
                        {
                            DateTime currentTime = DateTime.Now;
                            //    List<object> valPara = new List<object>()
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
                                result.Message = ex.Message;
                            }
                        }
                        //}
                        //else
                        //{
                        //    data = new List<List<object>>();
                        //    result.Message = data[0][11].ToString();
                        //}
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
        /// 設置無資料可填報
        /// </summary>
        /// <param name="data">主表資料</param>
        /// <param name="query">查詢條件</param>
        /// <param name="parameters"></param>
        /// <param name="paraValues"></param>
        /// <returns></returns>
        private IResult SetNoDataMark(out List<List<object>> data, string query, List<string> parameters, List<object> paraValues, string userID)
        {
            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                data = new List<List<object>>();
                IResult result = new Result.Result(false);
                // select
                SqlDataReader dr;
                SqlCommand cmdSelect = new SqlCommand(query, con);

                // update
                // 1. set NO_DATA_MARK from ERA2_DISP_DETAIL where DISP_DETAIL_ID
                DateTime currentTime = DateTime.Now;
                List<string> updatePara = new List<string>()
                {
                    "@NO_DATA_MARK", "@COUNT_SDATE", "@COUNT_EDATE",
                    "@MODIFIED_USER", "@MODIFIED_TIME",
                };
                List<object> updateValPara = new List<object>()
                {
                    "1", DBNull.Value, DBNull.Value,
                    userID, currentTime
                };
                List<string> updateWherePara = new List<string>() { "@DISP_DETAIL_ID" };
                string queryUpdate = ConcatUpdateQuery("[dbo].[ERA2_DISP_DETAIL]", updatePara, updateWherePara);
                SqlCommand cmdUpdate = new SqlCommand(queryUpdate, con);
                cmdUpdate.CommandType = CommandType.Text;
                for (int i = 0; i < updatePara.Count; i++)
                    cmdUpdate.Parameters.AddWithValue(updatePara[i], updateValPara[i]);
                string queryDelete = ConcatDeleteQuery("@DeleteTableName", updateWherePara);
                // 2. use TABLE_NAME where DISP_DETAIL_ID to delete data
                SqlCommand cmdDelete = new SqlCommand(queryDelete, con);

                //insert
                List<string> insertPara = new List<string>()
                {
                    "@DISP_MAIN_ID", "@DISP_STYLE_ID", "@NO_DATA_MARK",
                    "@CREATED_USER", "@CREATED_TIME", "@MODIFIED_USER",
                    "@MODIFIED_TIME",
                };
                List<object> insertParaValues = new List<object>()
                {
                    paraValues[3], paraValues[4], "1",
                    userID, currentTime, userID,
                    currentTime
                };
                int insertPk = 0;
                string queryInsert = ConcatInsertQuery("[dbo].[ERA2_DISP_DETAIL]", insertPara);
                SqlCommand cmdInsert = new SqlCommand(queryInsert, con);
                cmdInsert.CommandType = CommandType.Text;
                for (int i = 0; i < insertPara.Count; i++)
                    cmdInsert.Parameters.AddWithValue(insertPara[i], insertParaValues[i]);

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

                        DBDataUtility util = new DBDataUtility();
                        if ("DBNull" != data[0][2].GetType().Name && result.Success)
                        {
                            // ISSUCCESSFUL = 1
                            if ("1" == (data[0][2]).ToString())
                            {
                                // DISP_DETAIL_ID != null
                                if ("DBNull" != data[0][0].GetType().Name)
                                {
                                    //List<object> updateWhereValPara = new List<object>() { data[0][0] };

                                    //cmdDelete.CommandText = ConcatDeleteQuery(data[0][1].ToString(), updateWherePara);
                                    cmdDelete.CommandText = ConcatDeleteQuery(util.CheckString(data[0][1]), updateWherePara);

                                    cmdUpdate.Parameters.AddWithValue(updateWherePara[0], Convert.ToInt64(util.CheckString(data[0][0])));
                                    cmdDelete.Parameters.AddWithValue(updateWherePara[0], Convert.ToInt64(util.CheckString(data[0][0])));
                                    //for (int i = 0; i < updateWherePara.Count; i++)
                                    //    cmdUpdate.Parameters.AddWithValue(updateWherePara[i], updateWhereValPara[i]);
                                    //for (int i = 0; i < updateWherePara.Count; i++)
                                    //    cmdDelete.Parameters.AddWithValue(updateWherePara[i], updateWhereValPara[i]);

                                    cmdUpdate.ExecuteNonQuery();
                                    cmdDelete.ExecuteNonQuery();
                                }
                                else
                                {
                                    // only set NO_DATA_MAEK
                                    insertPk = (int)cmdInsert.ExecuteScalar();

                                    data[0][0] = insertPk;
                                }

                                result.Success = true;
                                result.Message = string.Empty;
                            }
                            else
                            {
                                result.Success = true;
                                result.Message = data[0][3].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Exception = ex;
                        result.Message = ex.Message;
                    }

                    scope.Complete();
                }

                cmdSelect.Dispose();
                cmdUpdate.Dispose();
                cmdInsert.Dispose();
                con.Close();
                con.Dispose();

                return result;
            }
        }

        /// <summary>
        /// 檢查維護狀態
        /// </summary>
        /// <param name="data">主表資料</param>
        /// <param name="query">查詢條件</param>
        /// <param name="parameters"></param>
        /// <param name="paraValues"></param>
        private IResult CheckMaintainStatue(out List<List<object>> data, string query, List<string> parameters, List<object> paraValues, string userID)
        {
            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                data = new List<List<object>>();
                IResult result = new Result.Result(false);
                // select
                SqlDataReader dr;
                SqlCommand cmdSelect = new SqlCommand(query, con);
                cmdSelect.CommandType = CommandType.Text;
                for (int i = 0; i < parameters.Count; i++)
                    cmdSelect.Parameters.AddWithValue(parameters[i], paraValues[i]);

                //insert
                DateTime currentTime = DateTime.Now;
                List<string> insertPara = new List<string>()
                {
                    "@DISP_MAIN_ID", "@DISP_STYLE_ID", "@CREATED_USER",
                    "@CREATED_TIME", "@MODIFIED_USER", "@MODIFIED_TIME",
                };

                List<object> insertParaValues = new List<object>()
                {
                    paraValues[3], paraValues[4], userID,
                    currentTime, userID, currentTime
                };

                /*switch(paraValues[4]){
					case 4:
					case 44:
					case 85:
					case 122:
					case 152:
					case 183:
                        insertPara.Add("@COUNT_SDATE");
                        insertPara.Add("@COUNT_EDATE");
                        insertParaValues.Add(currentTime);
                        insertParaValues.Add(currentTime);
                        break;

                    default:
                        break;
				}*/

                int insertPk = 0;
                string queryInsert = ConcatInsertQuery("[dbo].[ERA2_DISP_DETAIL]", insertPara);
                SqlCommand cmdInsert = new SqlCommand(queryInsert, con);
                cmdInsert.CommandType = CommandType.Text;
                for (int i = 0; i < insertPara.Count; i++)
                    cmdInsert.Parameters.AddWithValue(insertPara[i], insertParaValues[i]);

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
                            result.Message = "Not data.";
                        }

                        dr.Close();

                        if ("DBNull" != data[0][2].GetType().Name && result.Success)
                        {
                            // ISSUCCESSFUL = 1
                            if ("1" == (data[0][2]).ToString())
                            {
                                // DISP_DETAIL_ID != null
                                if ("DBNull" != data[0][0].GetType().Name)
                                {
                                }
                                else
                                {
                                    // only insert detail
                                    insertPk = (int)cmdInsert.ExecuteScalar();

                                    data[0][0] = insertPk;
                                }

                                result.Success = true;
                                result.Message = string.Empty;
                            }
                            else
                            {
                                result.Success = true;
                                result.Message = data[0][3].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Exception = ex;
                        result.Message = ex.Message;
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
        /// To connect db and get table data by query command.
        /// </summary>
        /// <param name="data">Output table data</param>
        /// <param name="query">sql command query</param>
        private IResult GetHistoryData(
            out List<List<dynamic>> data,
            string queryMainId,
            List<string> whereParas,
            List<object> whereParasValuesM,
            string querySp,
            List<string> parameters,
            List<object> paraValues)
        {
            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                data = null;
                IResult result = new Result.Result(false);
                DataTableCollection temp = null;
                List<List<object>> getMain = new List<List<object>>();

                // get main id
                SqlDataReader dr;
                // get sp
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                SqlCommand cmdMainId = new SqlCommand(queryMainId, con);
                cmdMainId.CommandType = CommandType.Text;
                for (int i = 0; i < whereParas.Count; i++)
                    cmdMainId.Parameters.AddWithValue(whereParas[i], whereParasValuesM[i]);

                SqlCommand cmdSp = new SqlCommand(querySp, con);
                cmdSp.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < parameters.Count; i++)
                    cmdSp.Parameters.AddWithValue(parameters[i], paraValues[i]);

                try
                {
                    if (con.State == System.Data.ConnectionState.Closed)
                        con.Open();

                    dr = cmdMainId.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            List<object> row = new List<object>();
                            for (int i = 0; i < dr.FieldCount; i++)
                                row.Add(dr.GetValue(i));
                            getMain.Add(row);
                        }

                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Not data.";
                    }

                    dr.Close();
                    DBDataUtility util = new DBDataUtility();
                    if ("DBNull" != getMain[0][0].GetType().Name && result.Success)
                    {
                        cmdSp.Parameters[parameters[0]].Value = Convert.ToInt64(util.CheckString(getMain[0][0]));
                        da.SelectCommand = cmdSp;
                        da.Fill(ds);
                        temp = ds.Tables;
                    }
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Exception = ex;
                    result.Message = ex.Message;
                }

                if (temp != null)
                {
                    // table collection
                    List<List<dynamic>> tables = new List<List<dynamic>>();
                    for (int i = 0; i < temp.Count; i++)
                    {
                        // table
                        List<dynamic> table = new List<dynamic>();

                        for (int j = 0; j < temp[i].Rows.Count; j++)
                        {
                            // row
                            dynamic row = new ExpandoObject();
                            for (int k = 0; k < temp[i].Rows[j].ItemArray.Length; k++)
                            {
                                // add col
                                AddProperty(row, temp[i].Columns[k].ColumnName, temp[i].Rows[j].ItemArray[k]);
                            }
                            table.Add(row);
                        }
                        tables.Add(table);
                    }

                    data = tables;
                }

                cmdMainId.Dispose();
                cmdSp.Dispose();
                con.Close();
                con.Dispose();

                return result;
            }
        }
    }
}