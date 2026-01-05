///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  EDD2020403Dao.cs
//  程式名稱：
//  救災資源查詢與統計
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-10-02       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  救災資源查詢與統計
///////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using EMIC2.Models.Dao.Dto.EDD2.EDD2020403;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.EDD2.EDD2020403;

namespace EMIC2.Models.Dao.EDD2.EDD2020403
{
    public class EDD2020403Dao : IEDD2020403Dao
    {
        /// <summary>
        ///  救災資源查詢與統計取得資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns>List<EDD2020403Dto></returns>
        public List<EDD2020403Dto> EDD2_020403_M(EDD2020403Dto data)
        {
            List<EDD2020403Dto> result = new List<EDD2020403Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql = string.Empty;
                StringBuilder sql_str = new StringBuilder();

                // 主體sql
                sql_str.Append(@"select *  from EDD2_020403_M 
                                               (@P_VIEW_TYPE, @P_UNIT_ID, @P_MASTER_TYPE_ID, 
                                                @P_SECONDARY_TYPE_ID, @P_DETAIL_TYPE_ID, @P_RESOURCE_ID, 
                                                @P_CITY_NAME, @P_TOWN_NAME, @P_UNIT_NAME)");

                var parameters = new
                {
                    P_VIEW_TYPE = data.P_VIEW_TYPE,
                    P_UNIT_ID = data.P_UNIT_ID = data.unit_level_1 == "0" ? "-1" : data.unit_level_4 ?? data.unit_level_3 ?? data.unit_level_2 ?? data.unit_level_1,
                    P_MASTER_TYPE_ID = data.P_MASTER_TYPE_ID = data.P_MASTER_TYPE_ID == "0" ? "-1" : data.P_MASTER_TYPE_ID,
                    P_SECONDARY_TYPE_ID = data.P_SECONDARY_TYPE_ID = data.P_SECONDARY_TYPE_ID ?? "-1",
                    P_DETAIL_TYPE_ID = data.P_DETAIL_TYPE_ID = data.P_DETAIL_TYPE_ID ?? "-1",
                    P_RESOURCE_ID = data.P_RESOURCE_ID,
                    P_CITY_NAME = data.P_CITY_NAME == "0" ? null : data.P_CITY_NAME,
                    P_TOWN_NAME = data.P_TOWN_NAME == "0" ? null : data.P_TOWN_NAME,
                    P_UNIT_NAME = data.P_UNIT_NAME,
                };

                // 排序種類
                StringBuilder orderby_1 = new StringBuilder();
                orderby_1.Append("order by UNIT_LEVEL, UNIT_ID, LOCATION_NAME, RESOURCE_ID, SORT_SEQ");
                StringBuilder orderby_2 = new StringBuilder();
                orderby_2.Append("order by RESOURCE_ID, SORT_SEQ, UNIT_LEVEL, UNIT_ID, LOCATION_NAME");

                // 選取地圖點位查詢
                if (data.LOCATION_ID_ITEMS != null)
                {
                    StringBuilder location_str = new StringBuilder();
                    string _id = "";
                    location_str.Append(@"where LOCATION_ID in (");
                    foreach (var item in data.LOCATION_ID_ITEMS.TrimEnd(',').Split(','))
                    {
                        _id += item + ",";
                    }
                    location_str.Append(_id.TrimEnd(','));
                    location_str.Append(")");

                    sql_str.Append(location_str);
                   
                }

                //依填報單位
                if (data.P_VIEW_TYPE == "O")
                {

                    sql_str.Append(orderby_1);

                }
                else
                {
                    sql_str.Append(orderby_2);
                }

                var query = conn.Query<EDD2020403Dto>(sql_str.ToString(), parameters);
                result = query.ToList();

                foreach (var item in result)
                {
                    item.MODIFIED_TIME_TEXT = item.MODIFIED_TIME?.ToString("yyyy-MM-dd");
                    item.SORT_SEQ_TEXT = item.SORT_SEQ == 1 ? "明細" : "小計";
                }

                return result;
            }
        }

        /// <summary>
        /// 功能說明：取得保管場所地點資料
        /// </summary>
        /// <param name="data"> SearchModelDto: 查詢使用Dto (預設)
        /// </param>
        /// <returns>
        /// ResultModelDto
        /// </returns>
        /// 開發人員            日期           異動內容                    解決的問題
        /// Joe             2019-10-09        新增此功能                  保管場所地點資料
        public List<EDD2020403Dto> EDD2_020403_LOCATION(EDD2020403Dto data)
        {
            List<EDD2020403Dto> result = new List<EDD2020403Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql = string.Empty;
                StringBuilder sql_str = new StringBuilder();

                // 主體sql
                sql_str.Append(@"select *  from EDD2_020403_LOCATION 
                                               (@P_VIEW_TYPE, @P_UNIT_ID, @P_MASTER_TYPE_ID, 
                                                @P_SECONDARY_TYPE_ID, @P_DETAIL_TYPE_ID, @P_RESOURCE_ID, 
                                                @P_CITY_NAME, @P_TOWN_NAME, @P_UNIT_NAME)");

                var parameters = new
                {
                    P_VIEW_TYPE = data.P_VIEW_TYPE,
                    P_UNIT_ID = data.P_UNIT_ID = data.unit_level_1 == "0" ? "-1" : data.unit_level_4 ?? data.unit_level_3 ?? data.unit_level_2 ?? data.unit_level_1,
                    P_MASTER_TYPE_ID = data.P_MASTER_TYPE_ID = data.P_MASTER_TYPE_ID == "0" ? "-1" : data.P_MASTER_TYPE_ID,
                    P_SECONDARY_TYPE_ID = data.P_SECONDARY_TYPE_ID = data.P_SECONDARY_TYPE_ID ?? "-1",
                    P_DETAIL_TYPE_ID = data.P_DETAIL_TYPE_ID = data.P_DETAIL_TYPE_ID ?? "-1",
                    P_RESOURCE_ID = data.P_RESOURCE_ID,
                    P_CITY_NAME = data.P_CITY_NAME == "0" ? null : data.P_CITY_NAME,
                    P_TOWN_NAME = data.P_TOWN_NAME == "0" ? null : data.P_TOWN_NAME,
                    P_UNIT_NAME = data.P_UNIT_NAME,
                };

                var query = conn.Query<EDD2020403Dto>(sql_str.ToString(), parameters);
                result = query.ToList();

                foreach (var item in result)
                {
                    item.MODIFIED_TIME_TEXT = item.MODIFIED_TIME?.ToString("yyyy-MM-dd");
                    item.SORT_SEQ_TEXT = item.SORT_SEQ == 1 ? "明細" : "小計";
                }

                return result;
            }
        }

        /// <summary>
        ///  詳細資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns>EDD2020403Dto</returns>
        public EDD2020403Dto GetDeatilData(EDD2020403Dto data)
        {
            EDD2020403Dto result = new EDD2020403Dto();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql = string.Empty;

                sql = @"select *,
                         (cast(A.CURRENT_QTY as varchar) + E.STANDARD_UOM_NAME) as CURRENT_QTY,
                         (cast(A.AVAILABLE_QTY as varchar) + E.STANDARD_UOM_NAME) as AVAILABLE_QTY,
                         (cast(A.SUPPORT_QTY as varchar) + E.STANDARD_UOM_NAME) as SUPPORT_QTY,
                         cast((case when A.AVAILABLE_QTY - A.SUPPORT_QTY > 0 
                         then A.AVAILABLE_QTY - A.SUPPORT_QTY else 0 end) as varchar) + E.STANDARD_UOM_NAME as INVENTORY_QTY,
                         A.MODIFIED_TIME 
                        from EDD2_RESOURCE_STOCK_DATA A
                        join EDD2_UNIT_LOCATION B on A.UNIT_LOCATION_ID = B.UNIT_LOCATION_ID
                        join EDD2_UNIT_MASTER C on B.UNIT_ID = C.UNIT_ID
                        join EDD2_LOCATION_MASTER D on B.LOCATION_ID = D.LOCATION_ID
                        join EDD2_RESOURCE_ITEM_ALL E on A.RESOURCE_ID = E.RESOURCE_ID
                        where A.RESOURCE_STOCK_ID = @RESOURCE_STOCK_ID";


                var parameters = new
                {
                    RESOURCE_STOCK_ID = data.RESOURCE_STOCK_ID,
                };

                var query = conn.Query<EDD2020403Dto>(sql, parameters).FirstOrDefault();
                result = query;


                return result;
            }
        }
    }
}