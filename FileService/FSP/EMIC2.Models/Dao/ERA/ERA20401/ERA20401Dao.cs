
///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  IERA20401Dao.cs
//  程式名稱：
//  各機關最新填報狀況
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-04-26       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  各機關最新填報狀況，時所使用Dao
///////////////////////////////////////////////////////////////////////////////////////
using EMIC2.Models.Dao.Dto.ERA;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.ERA
{
    public class ERA20401Dao : IERA20401Dao
    {
        /// <summary>
        /// 各機關最新填報狀況資訊，呼叫 Stored Procedure 回傳資料
        /// </summary>
        /// <returns> IEnumerable<ERA20401Dto></returns>
        public IEnumerable<ERA20401Dto> GetData(ERA20401Dto data)
        {
            IEnumerable<ERA20401Dto> resultDtos = new List<ERA20401Dto>();
            string query =
               "Select * from " + "[dbo].[ERA2_0401_M]" +
               "('" + data.EOC_ID + "','" +
               data.P_PRJ_NO + "','" +
               data.P_HOUR + "') Order by SHOW_ORDER, ORG_ID";

            resultDtos = this.GetTableData(query);

            return resultDtos;
        }


        /// <summary>
        /// To connect db and get table data by query command.
        /// </summary>
        /// <param name="data">Output table data</param>
        /// <param name="query">sql command query</param>
        private IEnumerable<ERA20401Dto> GetTableData(string query)
        {
            using (SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    SqlDataReader dr;


                    con.Open();

                    dr = cmd.ExecuteReader();

                    List<ERA20401Dto> list = new List<ERA20401Dto>();
                    while (dr.Read())
                    {

                        list.Add(new ERA20401Dto
                        {
                            ORG_NAME = dr.GetValue(0).ToString(),
                            NOT_RPT_CNT = (int)dr.GetValue(1),
                            NOT_RPT_CODE = dr.GetValue(2).ToString()
                        });

                    }

                    // FORTIFY: Unreleased Resource: Database
                    dr.Close();
                    con.Close();

                    return list;
                }
            }
        }
    }
}
