
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
    public class ERA20203Dao : IERA20203Dao
    {
        /// <summary>
        /// 依通報日期查詢資料
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_RPT_TIME_S">時間(起)</param>
        /// <param name="p_RPT_TIME_E">時間(迄)</param>
        /// <param name="p_DIS_DATA_UID">災害類別</param>
        /// <param name="p_RPT_NO_S">報別(起)</param>
        /// <param name="p_RPT_NO_E">報別(迄)</param>
        /// <param name="P_RPT_DEF_ID">報表定義ID</param>
        /// <returns>資料集</returns>
        public List<List<object>> ERA2_0203_QD(string p_EOC_ID, DateTime p_RPT_TIME_S, DateTime p_RPT_TIME_E, int p_DIS_DATA_UID, int p_RPT_NO_S, int p_RPT_NO_E, int P_RPT_DEF_ID)
        {
            
            string query =
                "Select * from " + "[dbo].[ERA2_0203_QD]" +
                "('" + p_EOC_ID + "','" +
                this.GetTimePara(p_RPT_TIME_S) + "','" +
                this.GetTimePara(p_RPT_TIME_E) + "','" +
                p_DIS_DATA_UID + "','" +
                p_RPT_NO_S + "','" +
                p_RPT_NO_E + "','" +
                P_RPT_DEF_ID + "') order by RPT_CODE, RPT_NO desc;";

            GetTableData(out List<List<object>> tbData, query);

            return tbData;
        }

        /// <summary>
        /// 依專案查詢資料
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_RPT_NO_S">報別(起)</param>
        /// <param name="p_RPT_NO_E">報別(迄)</param>
        /// <returns>資料集</returns>
        public List<List<object>> ERA2_0203_QP(string p_EOC_ID, long p_PRJ_NO, int p_RPT_NO_S, int p_RPT_NO_E)
        {
            string query =
                "Select * from " + "[dbo].[ERA2_0203_QP]" +
                "('" + p_EOC_ID + "','" +
                p_PRJ_NO + "','" +
                p_RPT_NO_S + "','" +
                p_RPT_NO_E + "') order by RPT_CODE, RPT_NO desc;";

            GetTableData(out List<List<object>> tbData, query);

            return tbData;
        }

        /// <summary>
        /// 層級1之報表集合
        /// </summary>
        /// <returns>資料集</returns>
        public List<List<object>> ERA2_RPT_DEF_L1()
        {
            string query =
                "Select * from " + "[dbo].[ERA2_RPT_DEF_L1] order by RPT_CODE";

            GetTableData(out List<List<object>> tbData, query);

            return tbData;
        }

        /// <summary>
        /// 層級2之報表集合
        /// </summary>
        /// <returns>資料集</returns>
        public List<List<object>> ERA2_RPT_DEF_L2()
        {
            string query =
                "Select * from " + "[dbo].[ERA2_RPT_DEF_L2]";

            GetTableData(out List<List<object>> tbData, query);

            return tbData;
        }

        /// <summary>
        /// 層級3之報表集合
        /// </summary>
        /// <returns>資料集</returns>
        public List<List<object>> ERA2_RPT_DEF_L3()
        {
            string query =
                "Select * from " + "[dbo].[ERA2_RPT_DEF_L3]";

            GetTableData(out List<List<object>> tbData, query);

            return tbData;
        }

        /// <summary>
        /// 專案查詢取專案資料集
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_RPT_TIME_S"></param>
        /// <param name="p_RPT_TIME_E"></param>
        /// <param name="p_DIS_DATA_UID"></param>
        /// <param name="p_CASE_NAME"></param>
        /// <returns>資料集</returns>
        public List<List<object>> ERA2_PROJECT_SEARCH(string p_EOC_ID, DateTime p_RPT_TIME_S, DateTime p_RPT_TIME_E, int p_DIS_DATA_UID, string p_CASE_NAME)
        {
            string query =
                "select PRJ_NO, EOC_ID, CASE_NAME, PRJ_STIME, PRJ_ETIME, OPEN_LV, " +
                "case when PRJ_ETIME is NULL then '開設中' else '已撤除' end as OPEN_STATUS " +
                "from [dbo].[EEM2_EOC_PRJ] " +
                "where " +
                "EOC_ID = " + p_EOC_ID +
                " and " + "PRJ_STIME >= " + "convert(datetime, '" + this.GetTimePara(p_RPT_TIME_S) + "')" +
                " and " + "ISNULL(PRJ_ETIME,convert(datetime, '" + this.GetTimePara(p_RPT_TIME_E) + "')) <= convert(datetime, '" + this.GetTimePara(p_RPT_TIME_E) + "')" +
                " and " + "DIS_DATA_UID = " + p_DIS_DATA_UID +
                "";
            if (""!= p_CASE_NAME)
            {
                query += " and " + "CASE_NAME like '%" + p_CASE_NAME + "%'";
            }

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

        private int GetTimePara(DateTime dateTime)
        {
            string str = string.Format("{0,4:0000}", dateTime.Year);
            str += string.Format("{0,2:00}", dateTime.Month);
            str+= string.Format("{0,2:00}", dateTime.Day);

            return Convert.ToInt32(str);
        }
    }
}
