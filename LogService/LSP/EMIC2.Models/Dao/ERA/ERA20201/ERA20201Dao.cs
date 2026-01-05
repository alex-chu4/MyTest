
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.ERA
{
    public class ERA20201Dao : IERA20201Dao
    {
        /// <summary>
        /// 查詢最新報表資料
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <returns>資料集</returns>
        public List<List<object>> ERA2_0201_M(string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID)
        {
            string query =
                "Select * from " + "[dbo].[ERA2_0201_M]" +
                "('" + p_EOC_ID + "','" +
                p_PRJ_NO.ToString() + "','" +
                p_ORG_ID.ToString() + "') order by RPT_CODE";

            GetTableData(out List<List<object>> tbData, query);

            return tbData;
        }
        
        /// <summary>
        /// To connect db and get table data by query command.
        /// </summary>
        /// <param name="data">Output table data</param>
        /// <param name="query">sql command query</param>
        private void GetTableData(out List<List<object>> data,string query)
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
    }
}
