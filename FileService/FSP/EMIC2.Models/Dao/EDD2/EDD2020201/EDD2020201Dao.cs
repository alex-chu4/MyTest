
namespace EMIC2.Models.Dao.EDD2
{
    using EMIC2.Models.Helper;
    using EMIC2.Models.Interface.EDD;
    using EMIC2.Result;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Dynamic;
    using System.Linq;
    using System.Text;
    using System.Transactions;

    public class EDD2020201Dao : EDD2020Dao, IEDD2020201Dao
    {
        public IResult ViewLoadingInfo(out List<List<object>> data, int unitID)
        {
            List<string> inputParas_info = new List<string>() {
                "@P_UNIT_ID",
                "@P_RESOURCE_NAME",
                "@P_CITY_NAME",
                "@P_TOWN_NAME"
            };
            List<object> inputParaVals_info = new List<object>() { unitID, string.Empty, -1, -1 };
            string queryInfo = ConcatSelectQuery("[dbo].[EDD2_020201_M]", inputParas_info, useEnd: true);


            IResult result = GetViewLoadingInfo(
                out data,
                queryInfo, inputParas_info, inputParaVals_info);

            return result;
        }

        public IResult EDD2_020201_M(out List<List<dynamic>> data, int p_UNIT_ID, string p_RESOURCE_NAME, string p_CITY_NAME, string p_TOWN_NAME)
        {
            List<string> inputParas = new List<string>() {
                "@P_UNIT_ID",
                "@P_RESOURCE_NAME",
                "@P_CITY_NAME",
                "@P_TOWN_NAME"
            };
            List<object> inputParaVals = new List<object>() {
                p_UNIT_ID,
                p_RESOURCE_NAME,
                p_CITY_NAME,
                p_TOWN_NAME
            };

            string query = ConcatSelectQuery("[dbo].[EDD2_020201_M]", inputParas, useEnd: true);

            IResult result = GetTableCollectionWithParameter(out data, query, inputParas, inputParaVals);
            return result;
        }

        public IResult UpdateTableRows(short p_FUNCTION, string p_ACTION, int? p_AUDITING_RECORD_ID, int p_UNIT_ID, string p_MODIFIED_USER, DataTable p_DATA_TBL)
        {
            List<string> inputParas = new List<string>()
            {
                "@P_FUNCTION",
                "@P_ACTION",
                "@P_AUDITING_RECORD_ID",
                "@P_UNIT_ID",
                "@P_MODIFIED_USER",
                "@P_DATA_TBL"
            };
            List<object> inputParaValues = new List<object>()
            {
                p_FUNCTION,
                p_ACTION,
                p_AUDITING_RECORD_ID,
                p_UNIT_ID,
                p_MODIFIED_USER,
                p_DATA_TBL
            };

            List<string> outputParas = new List<string>()
            {
                "@O_IsSuccessful",
                "@O_Msg"
            };
            List<SqlDbType> outputTypes = new List<SqlDbType>()
            {
                SqlDbType.Int,
                SqlDbType.NVarChar
            };
            List<int?> outputSizes = new List<int?>()
            {
                null,
                4000
            };

            IResult result = UpdateStock(
                "dbo.[EDD2_UPDATE_STOCK]",
                inputParas, inputParaValues,
                outputParas, outputTypes, outputSizes,
                CommandType.StoredProcedure);

            return result;
        }

        // ---------- Private function ----------
        private IResult GetViewLoadingInfo(
            out List<List<dynamic>> data,
            string queryDataInfo,
            List<string> inputParasInfo,
            List<object> inputParaValsInfo)
        {
            List<List<object>> ids = new List<List<object>>();
            data = null;
            IResult result = new Result(false);
            DataTableCollection temp = null;

            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                // Select var
                SqlCommand cmdSelectInfo = new SqlCommand(queryDataInfo, con);

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                try
                {
                    if (con.State == System.Data.ConnectionState.Closed)
                        con.Open();
                    
                        cmdSelectInfo.CommandType = CommandType.Text;
                        for (int i = 0; i < inputParasInfo.Count; i++)
                            cmdSelectInfo.Parameters.AddWithValue(inputParasInfo[i], inputParaValsInfo[i]);

                        da.SelectCommand = cmdSelectInfo;
                        da.Fill(ds);
                        temp = ds.Tables;
                    
                        result.Success = true;

                        da.Dispose();
                        cmdSelectInfo.Dispose();

                    con.Close();
                    con.Dispose();
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Exception = ex;
                    result.Message = "Failed.";
                }

            }

            if (null != temp && result.Success)
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
                    result.Message = "Dynamic Failed.";
                }
            }

            return result;
        }

        private IResult UpdateStock(
            string query,
            List<string> inputParas,
            List<object> inputParaValues,
            List<string> outputParas,
            List<SqlDbType> outputTypes,
            List<int?> outputSizes,
            CommandType cmdType = CommandType.Text)
        {
            IResult result = new Result(false);

            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        cmd.CommandType = cmdType;
                        for (int i = 0; i < inputParas.Count; i++)
                        {
                            cmd.Parameters.AddWithValue(inputParas[i], inputParaValues[i] ?? DBNull.Value);
                        }

                        List<SqlParameter> returnParas = new List<SqlParameter>();
                        for (int i = 0; i < outputParas.Count; i++)
                        {
                            SqlParameter para;
                            para = null != outputSizes[i] ?
                                cmd.Parameters.Add(outputParas[i], outputTypes[i], (int)(outputSizes[i])) :
                                cmd.Parameters.Add(outputParas[i], outputTypes[i]);
                            para.Direction = ParameterDirection.Output;
                            returnParas.Add(para);
                        }

                        if (con.State == System.Data.ConnectionState.Closed)
                            con.Open();

                        cmd.ExecuteNonQuery();

                        result.Message = returnParas.Find(p => p.ParameterName.Contains("Msg")).SqlValue.ToString();
                        result.ReturnValue = returnParas.Find(p => p.ParameterName.Contains("Success")).SqlValue.ToString();

                        result.Success = true;
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Exception = ex;
                        result.Message = "Failed.";
                    }

                    cmd.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }

            return result;
        }

        public IResult ExportRsc(out List<List<dynamic>> data, int p_UNIT_ID)
        {
            List<string> selectParas = new List<string>();
            selectParas.Add("@A.RESOURCE_NAME");
            selectParas.Add("@A.LOCATION_NAME");
            selectParas.Add("CAST(ISNULL(@B.LENGTH, 0) as nvarchar)" + " + '*' + " +
                " CAST(ISNULL(@B.WIDTH, 0) as nvarchar)" + " + '*' + " +
                "CAST(ISNULL(@B.HEIGHT, 0) as nvarchar)" + " + " +
                "ISNULL(@B.SIZE_UOM_NAME, '')");
            selectParas.Add("CAST(ISNULL(@B.WEIGHT, 0) as nvarchar)" + " + " +
                "ISNULL(@B.WEIGHT_UOM_NAME, '')");
            selectParas.Add("CAST(ISNULL(@B.ENERGY_VOLUMN, 0) as nvarchar)" + " + " +
                "ISNULL(@B.ENERGY_UOM_NAME, '')");
            selectParas.Add("CAST(ISNULL(@B.EFFICACY, 0) as nvarchar)" + " + " +
                "ISNULL(@B.EFFICACY_UOM_NAME, '')");
            selectParas.Add("'承載' + " + "CAST(ISNULL(@B.BEARING_NO, 0) as nvarchar)" + " + '人 ' + " +
                "'操作' + " + "CAST(ISNULL(@B.OPERATOR_NO, 0) as nvarchar)" + " + '人'");
            selectParas.Add("CAST(ISNULL(@A.CURRENT_QTY, 0) as nvarchar)" + " + " +
                "ISNULL(@B.STANDARD_UOM_NAME, '')");
            selectParas.Add("CAST(ISNULL(@A.CURRENT_QTY, 0) as nvarchar)" + " + " +
                "ISNULL(@B.STANDARD_UOM_NAME, '')");
            selectParas.Add("CAST(ISNULL(@A.AVAILABLE_QTY, 0) as nvarchar)" + " + " +
                "ISNULL(@B.STANDARD_UOM_NAME, '')");
            selectParas.Add("CONVERT(varchar, @A.MODIFIED_TIME, 120)");

            List<string> inputParas = new List<string>() {
                "@P_UNIT_ID",
                "@P_RESOURCE_NAME",
                "@P_CITY_NAME",
                "@P_TOWN_NAME"
            };
            List<object> inputParaVals = new List<object>() {
                p_UNIT_ID,
                "",
                -1,
                -1
            };

            string query = ConcatSelectQuery("[dbo].[EDD2_020201_M]", inputParas, useEnd: false, selectParas: selectParas);
            query += " A JOIN  EDD2_RESOURCE_ITEM_ALL B on A.RESOURCE_ID = B.RESOURCE_ID;";

            IResult result = GetTableCollectionWithParameter(out data, query, inputParas, inputParaVals);
            return result;
        }


        // ----- 0502
        public IResult SearchUnitLocation(
            out List<List<dynamic>> data, string p_QRY_TYPE, int p_UNIT_ID, int? p_RESOURCE_ID,
            string w_CITY_NAME = "", string w_TOWN_NAME = "", string w_LOCATION_NAME = "", string w_CONTACT_NAME = "")
        {
            List<string> inputParas = new List<string>()
            {
                "@P_QRY_TYPE",
                "@P_UNIT_ID",
                "@P_RESOURCE_ID"
            };
            List<object> inputVals = new List<object>();
            inputVals.Add(p_QRY_TYPE);
            inputVals.Add(p_UNIT_ID);
            if (null != p_RESOURCE_ID)
                inputVals.Add(p_RESOURCE_ID);
            else
                inputVals.Add(DBNull.Value);


            List<string> whereParas = new List<string>();
            List<object> whereVals = new List<object>();
            List<string> whereOperaters = new List<string>();
            if (string.Empty != w_CITY_NAME && null != w_CITY_NAME && "-1" != w_CITY_NAME)
            {
                whereParas.Add("@CITY_NAME");
                whereVals.Add("%" + w_CITY_NAME + "%");
                whereOperaters.Add(" like @paraVal");
            }
            if (string.Empty != w_TOWN_NAME && null != w_TOWN_NAME && "-1" != w_TOWN_NAME)
            {
                whereParas.Add("@TOWN_NAME");
                whereVals.Add("%" + w_TOWN_NAME + "%");
                whereOperaters.Add(" like @paraVal");
            }
            if (string.Empty != w_LOCATION_NAME && null != w_LOCATION_NAME)
            {
                whereParas.Add("@LOCATION_NAME");
                whereVals.Add("%" + w_LOCATION_NAME + "%");
                whereOperaters.Add(" like @paraVal");
            }
            if (string.Empty != w_CONTACT_NAME && null != w_CONTACT_NAME)
            {
                whereParas.Add("@CONTACT_NAME");
                whereVals.Add("%" + w_CONTACT_NAME + "%");
                whereOperaters.Add(" like @paraVal");
            }

            string query = ConcatSelectQuery("EDD2_QRY_LOCATION_BYUNIT", inputParas);
            query = ConcatWhereQuery(query, true, whereParas, whereOperaters, null);

            IResult result = GetTableCollectionWithParameter(out data, query, ConcatList(inputParas, whereParas), ConcatList(inputVals, whereVals));
            return result;
        }

        public IResult UpdateUnitLocation(List<string> insertDetailParas, List<object> insertDetailVals, List<string> insertMasterParas = null, List<object> insertMasterVals = null)
        {
            insertDetailParas.RemoveAt(0);
            insertDetailVals.RemoveAt(0);
            string queryDatail = ConcatInsertQuery("[dbo].[EDD2_UNIT_LOCATION]", insertDetailParas);
            string queryMaster = string.Empty;
            if (null != insertMasterParas && null != insertMasterVals)
            {
                insertMasterParas.RemoveAt(0);
                insertMasterVals.RemoveAt(0);
                queryMaster = ConcatInsertQuery("[dbo].[EDD2_LOCATION_MASTER]", insertMasterParas);
            }

            IResult result = this.UpdateUnitLoca(queryDatail, insertDetailParas, insertDetailVals, queryMaster, insertMasterParas, insertMasterVals);
            return result;
        }

        // ========== Private
        private IResult UpdateUnitLoca(
            string queryDetail, List<string> parametersDetail, List<object> paraValuesDetail,
            string queryMaster = "", List<string> parametersMaster = null, List<object> paraValuesMaster = null)
        {
            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                IResult result = new Result(false);
                int pkLocationId = Convert.ToInt32(paraValuesDetail[1]);
                int unitLocationId = 0;
                SqlCommand cmdDetail = new SqlCommand(queryDetail, con);

                using (TransactionScope scope = new TransactionScope())
                {
                    cmdDetail.CommandType = CommandType.Text;
                    for (int i = 0; i < parametersDetail.Count; i++)
                        cmdDetail.Parameters.AddWithValue(parametersDetail[i], paraValuesDetail[i]);

                    try
                    {
                        if (con.State == System.Data.ConnectionState.Closed)
                            con.Open();

                        if ("" != queryMaster)
                        {
                            SqlCommand cmdMaster = new SqlCommand(queryMaster, con);

                            cmdMaster.CommandType = CommandType.Text;
                            for (int i = 0; i < parametersMaster.Count; i++)
                                cmdMaster.Parameters.AddWithValue(parametersMaster[i], paraValuesMaster[i]);
                            pkLocationId = (int)cmdMaster.ExecuteScalar();
                            cmdDetail.Parameters["@LOCATION_ID"].Value = pkLocationId;
                            cmdMaster.Dispose();
                        }

                        unitLocationId = (int)cmdDetail.ExecuteScalar();

                        result.Success = true;
                        result.ReturnValue = unitLocationId + "," + pkLocationId;
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Exception = ex;
                        result.Message = ex.Message;
                    }

                    scope.Complete();
                }

                cmdDetail.Dispose();
                con.Close();
                con.Dispose();

                return result;
            }
        }




        // --------------------other
        /// <summary>
        /// 匯入外部資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        public IResult EDD2_IMP_XLS_TO_RESOURCE_STOCK_DATA(int MTMP_REC_ID, int UNIT_ID)
        {
            IResult result = new Result(false);

            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("EDD2_IMP_XLS_TO_RESOURCE_STOCK_DATA", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@P_MTMP_REC_ID", MTMP_REC_ID);
                    cmd.Parameters.AddWithValue("@P_UNIT_ID", UNIT_ID);

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
