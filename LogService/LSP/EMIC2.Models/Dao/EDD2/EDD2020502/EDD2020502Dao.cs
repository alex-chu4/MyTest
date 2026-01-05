
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

    public class EDD2020502Dao : EDD2020Dao, IEDD2020502Dao
    {
        public IResult SearchUnitLocation(
            out List<List<dynamic>> data,string p_QRY_TYPE, int p_UNIT_ID, int? p_RESOURCE_ID,
            string w_CITY_NAME="", string w_TOWN_NAME="", string w_LOCATION_NAME = "", string w_CONTACT_NAME = "")
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

    }
}
