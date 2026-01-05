///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  IERA20405Dao.cs
//  程式名稱：
//  道路通阻案件
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-07-23       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  道路通阻案件
///////////////////////////////////////////////////////////////////////////////////////
using Dapper;
using EMIC2.Models.Dao.Dto;
using EMIC2.Models.Dao.Dto.ERA.ERA20405;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA2;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EMIC2.Models.Dao.ERA
{
    public class ERA20405Dao : IERA20405Dao
    {

        /// <summary>
        /// 專案查詢 道路通阻案件查詢
        /// </summary>
        /// <returns>List<ERA2Dto></returns>
        public List<ERA20405> ERA2_0405_M(ERA2Dto data)
        {
            List<ERA20405> result = new List<ERA20405>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql =
                "select * " +
                "from [dbo].[ERA2_0405_M] (@P_CITY_ID, @P_TOWN_ID, @P_RPT_TIME_S, @P_RPT_TIME_E, @P_LINECODE )";

                if (data.ORDER_TYPE == "1")
                {
                    sql += "order by TRFSTATUS, ROADTYPE_ORDER, CLOSE_DATETIME, SHOW_ORDER";
                }
                else
                {
                    sql += "order by TRFSTATUS, CLOSE_DATETIME, ROADTYPE_ORDER, SHOW_ORDER";
                }

                var parameters = new
                {
                    P_CITY_ID = data.CITY_ID,
                    P_TOWN_ID = data.TOWN_ID,
                    P_RPT_TIME_S = this.GetTimePara(data.P_RPT_TIME_S.Value),
                    P_RPT_TIME_E = this.GetTimePara(data.P_RPT_TIME_E.Value),
                    P_LINECODE = data.P_LINECODE == null ? "" : data.P_LINECODE,
                };

                var query = conn.Query<ERA20405>(sql, parameters);

                //foreach (var item in query)
                //{
                //    // D0 (二、（一）阻斷未搶通部分)

                //    // 阻斷日期：CLOSE_DATETIME		(UI顯示至時分)
                //    item.CLOSE_DATETIME_TEXT = item.CLOSE_DATETIMEValue.ToString("yyyy-MM-dd HH:ss");

                //    // 原因：(CLOSE_TYPE-CLOSE_TSUB)
                //    item.CLOSE_TYPE = item.CLOSE_TYPE + "-" + item.CLOSE_TSUB;


                //}

                result = query.ToList();

                return result;
            }
        }

        private int GetTimePara(DateTime dateTime)
        {
            string str = string.Format("{0,4:0000}", dateTime.Year);
            str += string.Format("{0,2:00}", dateTime.Month);
            str += string.Format("{0,2:00}", dateTime.Day);

            return Convert.ToInt32(str);
        }
    }
}
