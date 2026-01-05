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
    public class ERA20303Dao : ERA203Dao, IERA20303Dao
    {
        /// <summary>
        /// 取得應變中心項目資料
        /// </summary>
        /// <param name="result"></param>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_LEVEL">max level</param>
        /// <returns></returns>
        public List<List<object>> GetEOCItems(out IResult result, string p_EOC_ID, string p_LEVEL)
        {
            List<string> selectParas = new List<string>()
            {
                "@EOC_ID", "@EOC_NAME", "@EOC_PARENT", "@EOC_LEVEL"
            };

            List<string> whereAndParas = new List<string>()
            {
                "@IS_ON_OFF", "@EOC_ID", "@EOC_LEVEL"
            };

            List<string> whereAndOperaters = new List<string>()
            {
                "=", "=", "<"
            };

            List<string> whereOrParas = new List<string>()
            {
                "@EOC_PARENT"
            };

            List<string> whereOrderParas = new List<string>()
            {
                "@EOC_LEVEL", "@SHOW_ORDER"
            };

            List<object> whereAndParaValues = new List<object>()
            {
                1, p_EOC_ID, p_LEVEL
            };

            List<object> whereOrParaValues = new List<object>()
            {
                p_EOC_ID
            };

            string query = ConcatSelectQuery("[dbo].[EEM2_EOC_DATA]", selectParas: selectParas);
            query = ConcatWhereQuery(
                query, false,
                whereAndParas, whereAndOperaters, null,
                whereOrParas, null, null);
            query = ConcatOrderByQuery(query, true, whereOrderParas);

            result = GetTableDataWithParameter(
                out List<List<object>> tbData,
                query,
                ConcatList(whereAndParas, whereOrParas),
                ConcatList(whereAndParaValues, whereOrParaValues));

            return tbData;
        }

        /// <summary>
        /// 查詢最新報表資料 ok
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <returns>資料集</returns>
        public List<List<object>> ERA2_SearchMain(string p_EOC_ID, long p_PRJ_NO)
        {
            List<string> selectParas = new List<string>()
            {
                "@DISP_MAIN_ID", "@PRJ_NO", "@EOC_ID", "DIS_NAME", "DISP_NO",
                "CASE WHEN DIS_DATA_UID=11 THEN 'T' WHEN DIS_DATA_UID=13 THEN 'Q' ELSE 'U' END AS DISP_CAT_ID"
            };

            List<string> whereParas = new List<string>()
            {
                "@EOC_ID", "@PRJ_NO", "@DISP_TIME"
            };

            List<string> whereUseNullParas = new List<string>()
            {
                null, null, "not null"
            };

            List<object> whereParaValues = new List<object>()
            {
                p_EOC_ID, p_PRJ_NO, DBNull.Value
            };

            string query = ConcatSelectQuery("[dbo].[ERA2_DISP_MAIN]", selectParas: selectParas);
            query = ConcatWhereQuery(query, useEnd: true, whereParas: whereParas, useNull: whereUseNullParas);

            IResult result = SearchMainInfo(out List<List<object>> tbData, query, whereParas, whereParaValues);

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

            GetTableCollectionWithParameter(out List<List<dynamic>> dataCollection, "dbo.[ERA2_GET_DISP_DATA_ONE]", parameters, paraValues, CommandType.StoredProcedure);

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

            GetTableCollectionWithParameter(out List<List<dynamic>> dataCollection, "dbo.[ERA2_GET_DISP_DATA_BLOCK]", parameters, paraValues, CommandType.StoredProcedure);

            return dataCollection;
        }
        
        // ---------- Private function ----------
        /// <summary>
        /// 查詢最新主表資料
        /// </summary>
        /// <param name="data">主表資料</param>
        /// <param name="query">查詢條件</param>
        /// <param name="parameters"></param>
        /// <param name="paraValues"></param>
        private IResult SearchMainInfo(out List<List<object>> data, string query, List<string> whereParas, List<object> whereParaValues)
        {
            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                data = new List<List<object>>();
                IResult result = new Result.Result(false);
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand(query, con);

                using (TransactionScope scope = new TransactionScope())
                {
                    cmd.CommandType = CommandType.Text;
                    for (int i = 0; i < whereParas.Count; i++)
                        cmd.Parameters.AddWithValue(whereParas[i], whereParaValues[i]);

                    try
                    {
                        if (con.State == System.Data.ConnectionState.Closed)
                            con.Open();

                        dr = cmd.ExecuteReader();
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

                        if (con.State == System.Data.ConnectionState.Open)
                            con.Close();

                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Exception = ex;
                        result.Message = "Failed.";
                    }
                    
                    scope.Complete();
                }

                cmd.Dispose();
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
                SqlDataReader dr;
                SqlCommand cmdSelect = new SqlCommand(querySelect, con);
                SqlCommand cmdUpdate = new SqlCommand(queryUpdate, con);
                int executedSuccess = 0;

                using (TransactionScope scope = new TransactionScope())
                {
                    cmdSelect.CommandType = CommandType.Text;
                    for (int i = 0; i < selectParas.Count; i++)
                        cmdSelect.Parameters.AddWithValue(selectParas[i], selectParaValues[i]);

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
                            if ("1" == data[0][9].ToString())
                            {
                                cmdUpdate.CommandType = CommandType.Text;
                                for (int i = 0; i < updateParas.Count; i++)
                                    cmdUpdate.Parameters.AddWithValue(updateParas[i], updateParaValues[i]);

                                for (int i = 0; i < whereParas.Count; i++)
                                    cmdUpdate.Parameters.AddWithValue(whereParas[i], util.CheckString(data[0][0]));

                                executedSuccess = cmdUpdate.ExecuteNonQuery();

                                result.Success = true;
                            }
                            else
                            {
                                result.Success = false;
                                result.Message = "Status: 0.\n" + data[0][10].ToString();
                            }
                        }

                        if (con.State == System.Data.ConnectionState.Open)
                            con.Close();

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

        /// <summary>
        /// To connect db and get table data by query command.
        /// </summary>
        /// <param name="data">Output table data</param>
        /// <param name="query">sql command query</param>
        private void GetTableData(out List<List<object>> data, string query)
        {
            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader dr;
                    data = new List<List<object>>();

                    con.Open();

                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        List<object> row = new List<object>();
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            row.Add(dr.GetValue(i));
                        }
                        data.Add(row);
                    }

                    dr.Close();
                    cmd.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
        }

        private IResult GetTableDataWithParameter(out List<List<object>> data,
            string query,List<string> whereParas,List<object> whereParaValues)
        {
            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                data = new List<List<object>>();
                IResult result = new Result.Result(false);
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand(query, con);

                using (TransactionScope scope = new TransactionScope())
                {
                    cmd.CommandType = CommandType.Text;
                    for (int i = 0; i < whereParas.Count; i++)
                        cmd.Parameters.AddWithValue(whereParas[i], whereParaValues[i]);

                    try
                    {
                        if (con.State == System.Data.ConnectionState.Closed)
                            con.Open();

                        dr = cmd.ExecuteReader();
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

                        if (con.State == System.Data.ConnectionState.Open)
                            con.Close();

                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Exception = ex;
                        result.Message = "Failed.";
                    }

                    scope.Complete();
                }

                cmd.Dispose();
                con.Close();
                con.Dispose();

                return result;
            }
        }

        /// <summary>
        /// To connect db and get table data by query command. ok
        /// </summary>
        /// <param name="data">Output table data</param>
        /// <param name="query">sql command query</param>
        /// <param name="parameters">sql parameters</param>
        /// <param name="paraValues">values for sql parameters</param>
        /// <param name="cmdType">sql command type</param>
        private void GetTableCollectionWithParameter(
            out List<List<dynamic>> data,
            string query,
            List<string> parameters,
            List<object> paraValues,
            CommandType cmdType = CommandType.Text)
        {
            DataTableCollection temp = null;
            data = null;

            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();

                    try
                    {
                        cmd.CommandType = cmdType;
                        for (int i = 0; i < parameters.Count; i++)
                        {
                            cmd.Parameters.AddWithValue(parameters[i], paraValues[i]);
                        }

                        da.SelectCommand = cmd;

                        da.Fill(ds);
                        temp = ds.Tables;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    da.Dispose();

                    cmd.Dispose();
                    con.Close();
                    con.Dispose();
                }
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
        }
    }
}
