using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA;
using EMIC2.Result;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Transactions;

namespace EMIC2.Models.Dao.ERA
{
    public class ERA203Dao
    {
        // ---------- Common ----------
        /// <summary>
        /// To add property to dynamic object.
        /// </summary>
        /// <param name="expando">dynamic object</param>
        /// <param name="propertyName">property name</param>
        /// <param name="propertyValue">property value</param>
        protected static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            // FORTIFY: Null Dereference
            var expandoDict = (IDictionary<string, object>)expando;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

        /// <summary>
        /// To concatenate two lists to a new list
        /// </summary>
        /// <typeparam name="T">list type</typeparam>
        /// <param name="concat1">first list</param>
        /// <param name="concat2">second list</param>
        /// <returns>new list</returns>
        //protected List<T> ConcatList<T>(List<T> concat1,List<T> concat2)
        //{
        //    List<T> newConcat = new List<T>();
        //    foreach (var item in concat1)
        //        newConcat.Add(item);
        //    foreach (var item in concat2)
        //        newConcat.Add(item);
        //    return newConcat;
        //}

        /// <summary>
        /// To concatenate multiple lists to a new list
        /// </summary>
        /// <typeparam name="T">list type</typeparam>
        /// <param name="item">list collection</param>
        /// <returns>new list</returns>
        protected List<T> ConcatList<T>(params List<T>[] item)
        {
            List<T> newConcat = new List<T>();
            for (int i = 0; i < item.Length; i++)
            {
                foreach (var it in item[i])
                    newConcat.Add(it);
            }
            return newConcat;
        }

        /// <summary>
        /// To concatenate where query to select/Insert/update/delete query
        /// </summary>
        /// <param name="query">select/Insert/update/delete query</param>
        /// <param name="useEnd">use ';'</param>
        /// <param name="whereParas">Where parameters</param>
        /// <param name="useNull">null/not null for where parameters</param>
        /// <returns>command query</returns>
        protected string ConcatWhereQuery(string query, bool useEnd = false, List<string> whereParas = null, List<string> useNull = null)
        {
            if (null != whereParas)
            {
                query += " WHERE ";
                for (int i = 0; i < whereParas.Count; i++)
                {
                    query += whereParas[i].Replace("@", "");

                    string temp = "=";
                    temp += whereParas[i];
                    if (null != useNull)
                    {
                        if (null != useNull[i])
                        {
                            temp = " is " + useNull[i];
                        }
                    }

                    query += temp;
                    query += i != whereParas.Count - 1 ? " AND " : "";
                }
            }

            if (useEnd)
                query += ";";

            return query;
        }

        /// <summary>
        /// To concatenate where query to select/Insert/update/delete query
        /// </summary>
        /// <param name="query">select/Insert/update/delete query</param>
        /// <param name="useEnd">use ';'</param>
        /// <param name="whereAndParas">where and parameters</param>
        /// <param name="whereAndOperaters">operater to and parameters. e.g: =/<>/</></param>
        /// <param name="useAndNull">null/not null for and parameters</param>
        /// <param name="whereOrParas">Where or parameters</param>
        /// <param name="whereOrOperaters">operater to or parameters. e.g: =/<>/</></param>
        /// <param name="useOrNull">null/not null for or parameters</param>
        /// <returns>command query</returns>
        protected string ConcatWhereQuery(
            string query, bool useEnd = false,
            List<string> whereAndParas = null, List<string> whereAndOperaters = null, List<string> useAndNull = null,
            List<string> whereOrParas = null, List<string> whereOrOperaters = null, List<string> useOrNull = null)
        {
            query += " WHERE 1=1";
            if (null != whereAndParas)
            {
                for (int i = 0; i < whereAndParas.Count; i++)
                {
                    query += " AND ";
                    query += whereAndParas[i].Replace("@", "");

                    string temp = null != whereAndOperaters ? whereAndOperaters[i] : "=";
                    temp += whereAndParas[i];
                    if (null != useAndNull)
                    {
                        if (null != useAndNull[i])
                        {
                            temp = " is " + useAndNull[i];
                        }
                    }

                    query += temp;
                }
            }

            if (null != whereOrParas)
            {
                for (int i = 0; i < whereOrParas.Count; i++)
                {
                    query += " OR ";
                    query += whereOrParas[i].Replace("@", "");

                    string temp = null != whereOrOperaters ? whereOrOperaters[i] : "=";
                    temp += whereOrParas[i];
                    if (null != useOrNull)
                    {
                        if (null != useOrNull[i])
                        {
                            temp = " is " + useOrNull[i];
                        }
                    }

                    query += temp;
                }
            }

            if (useEnd)
                query += ";";

            return query;
        }

        /// <summary>
        /// To concatenate order by query to select/Insert/update/delete query
        /// </summary>
        /// <param name="query">select/Insert/update/delete query</param>
        /// <param name="useEnd">use ';'</param>
        /// <param name="orderParas">order by parameters</param>
        /// <returns>command query</returns>
        protected string ConcatOrderByQuery(
            string query, bool useEnd = false, List<string> orderParas = null)
        {
            if (null != orderParas)
            {
                query += " ORDER BY ";
                for (int i = 0; i < orderParas.Count; i++)
                {
                    query += orderParas[i].Replace("@", "");
                    query += (orderParas.Count - 1) != i ? "," : "";
                }
            }

            if (useEnd)
                query += ";";

            return query;
        }

        /// <summary>
        /// To concatenate select query
        /// </summary>
        /// <param name="tableName">Table name. e.g:[dbo].[ERA2_0301_M]</param>
        /// <param name="useEnd">use ';'</param>
        /// <param name="selectParas">select parameters</param>
        /// <returns>command query</returns>
        protected string ConcatSelectQuery(string tableName, bool useEnd = false, List<string> selectParas = null)
        {
            string query = "SELECT ";
            if (null != selectParas)
            {
                for (int i = 0; i < selectParas.Count; i++)
                {
                    query += selectParas[i].Replace("@", "");
                    query += i != selectParas.Count - 1 ? "," : "";
                }
            }
            else
                query += "*";

            query += " FROM ";
            query += tableName;

            if (useEnd)
                query += ";";

            return query;
        }

        /// <summary>
        /// To concatenate select db function query
        /// </summary>
        /// <param name="tableName">Table name. e.g:[dbo].[ERA2_0301_M]</param>
        /// <param name="useEnd">use ';'</param>
        /// <param name="selectParas">select parameters</param>
        /// <returns>command query</returns>
        protected string ConcatSelectQuery(string tableName, List<string> inputParas, bool useEnd = false, List<string> selectParas = null)
        {
            string query = "SELECT ";
            if (null != selectParas)
            {
                for (int i = 0; i < selectParas.Count; i++)
                {
                    query += selectParas[i].Replace("@", "");
                    query += i != selectParas.Count - 1 ? "," : "";
                }
            }
            else
                query += "*";

            query += " FROM ";
            query += tableName + "(";
            for (int i = 0; i < inputParas.Count; i++)
            {
                query += inputParas[i];
                query += i != inputParas.Count - 1 ? "," : "";
            }
            query += ")";

            if (useEnd)
                query += ";";

            return query;
        }

        /// <summary>
        /// To concatenate insert query
        /// </summary>
        /// <param name="tableName">Table name. e.g:[dbo].[ERA2_0301_M]</param>
        /// <param name="parameters">insert parameters</param>
        /// <returns>command query</returns>
        protected string ConcatInsertQuery(string tableName, List<string> parameters)
        {
            string query = "INSERT INTO ";
            query += tableName;

            string para = " (";
            for (int i = 0; i < parameters.Count; i++)
            {
                para += parameters[i];
                para += i != parameters.Count - 1 ? "," : "";
            }
            para += ") ";

            query += para.Replace("@", "");
            query += "values" + para + "; ";
            query += "Select cast(scope_identity() as int);";

            return query;
        }

        /// <summary>
        /// To concatenate update query
        /// </summary>
        /// <param name="tableName">Table name. e.g:[dbo].[ERA2_0301_M]</param>
        /// <param name="parameters">Table parameters</param>
        /// <param name="whereParas">Where parameters</param>
        /// <returns>command query</returns>
        protected string ConcatUpdateQuery(string tableName, List<string> parameters, List<string> whereParas = null)
        {
            string query = "update ";
            query += tableName;
            query += " set ";

            string para = "";
            for (int i = 0; i < parameters.Count; i++)
            {
                para += parameters[i].Replace("@", "") + "=";
                para += parameters[i];
                para += i != parameters.Count - 1 ? "," : "";
            }
            query += para;

            if (null != whereParas)
            {
                query += " where ";
                for (int i = 0; i < whereParas.Count; i++)
                {
                    query += whereParas[i].Replace("@", "") + "=";
                    query += whereParas[i];
                    query += i != whereParas.Count - 1 ? "," : "";
                }
            }
            query += ";";

            return query;
        }

        /// <summary>
        /// To concatenate delete query
        /// </summary>
        /// <param name="tableName">Table name. e.g:[dbo].[ERA2_0301_M]</param>
        /// <param name="whereParas">Where parameters</param>
        /// <returns>command query</returns>
        protected string ConcatDeleteQuery(string tableName, List<string> whereParas)
        {
            string query = "delete from ";
            query += tableName + " where ";

            for (int i = 0; i < whereParas.Count; i++)
            {
                query += whereParas[i].Replace("@", "") + "=";
                query += whereParas[i];
                query += i != whereParas.Count - 1 ? " and " : "";
            }
            query += ";";

            return query;
        }

        // ---------- protected function ----------
        /// <summary>
        /// To connect db and get table data by query command.
        /// </summary>
        /// <param name="data">Output table data</param>
        /// <param name="query">sql command query</param>
        protected void GetTableData(out List<List<object>> data, string query)
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

        /// <summary>
        /// To connect db and get table data by query command. ok
        /// </summary>
        /// <param name="data"></param>
        /// <param name="query"></param>
        /// <param name="whereParas"></param>
        /// <param name="whereParaValues"></param>
        /// <returns>connection status</returns>
        protected IResult GetTableDataWithParameter(
            out List<List<object>> data,
            string query,
            List<string> whereParas = null,
            List<object> whereParaValues = null)
        {
            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                data = new List<List<object>>();
                IResult result = new Result.Result(false);
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand(query, con);

                //using (TransactionScope scope = new TransactionScope())
                //{
                cmd.CommandType = CommandType.Text;
                if (null != whereParas)
                {
                    for (int i = 0; i < whereParas.Count; i++)
                        cmd.Parameters.AddWithValue(whereParas[i], whereParaValues[i]);
                }

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
                        result.ReturnValue = "-1";
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
                    result.Message = ex.Message;
                }

                //scope.Complete();
                //}

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
        /// <returns>connection status</returns>
        protected IResult GetTableCollectionWithParameter(
            out List<List<dynamic>> data,
            string query,
            List<string> parameters,
            List<object> paraValues,
            CommandType cmdType = CommandType.Text)
        {
            IResult result = new Result.Result(false);
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

                        result.Success = true;
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Exception = ex;
                        result.Message = ex.Message;
                    }

                    da.Dispose();

                    cmd.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }

            if (temp != null && result.Success)
            {
                try
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

                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Exception = ex;
                    result.Message = ex.Message;
                }
            }

            return result;
        }

        /// <summary>
        /// To connect db and get table data by query command. ok
        /// </summary>
        /// <param name="data">Output table data</param>
        /// <param name="query">sql command query</param>
        /// <param name="parameters">sql parameters</param>
        /// <param name="paraValues">values for sql parameters</param>
        /// <param name="cmdType">sql command type</param>
        /// <returns>connection status</returns>
        protected IResult GetDataSetWithParameter(
            out DataSet DS,
            string query,
            List<string> parameters,
            List<object> paraValues,
            CommandType cmdType = CommandType.Text)
        {
            IResult result = new Result.Result(false);
            DataTableCollection temp = null;
            DS = null;

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

                        DS = ds;

                        result.Success = true;
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Exception = ex;
                        result.Message = ex.Message;
                    }

                    da.Dispose();

                    cmd.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }

            return result;
        }
    }
}