///////////////////////////////////////////////////////////////////////////////////////
//  程式名稱：EDD2020504Dao.cs
//  資源項目維護Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員      日期       方法名稱          版本       功能說明
//  Joe        2019-09-21  資源項目維護Dao    1.0.0     資源項目維護
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
///////////////////////////////////////////////////////////////////////////////////////


using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using EMIC2.Models.Dao.Dto.EDD2;
using EMIC2.Models.Dao.Dto.EDD2.EDD2020504;
using EMIC2.Models.Helper;

namespace EMIC2.Models.Dao.EDD2.EDD2020504
{
    using EMIC2.Models.Interface;

    public class EDD2020504Dao : IEDD2020504Dao
    {
        /// <summary>
        /// 功能說明：查詢資料
        /// </summary>
        /// <param name="data">查詢條件</param>
        /// <returns>List<RESOURCE_ITEM_ALLDto></returns>
        /// 開發人員            日期           異動內容                    解決的問題
        /// Joe             2019-10-21       新增此功能                     查詢資料
        public (int Total, List<RESOURCE_ITEM_ALLDto>) EDD2_RESOURCE_ITEM_ALL(EDD2020504Dto data)
        {
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT * FROM EDD2_RESOURCE_ITEM_ALL WHERE 1 = 1 ");

                DynamicParameters parameters = new DynamicParameters();

                if (!string.IsNullOrEmpty(data.MASTER_TYPE_ID) && data.MASTER_TYPE_ID != "0")
                {
                    sql.Append("AND MASTER_TYPE_ID = @MASTER_TYPE_ID ");
                    parameters.Add("MASTER_TYPE_ID", data.MASTER_TYPE_ID);
                }

                if (!string.IsNullOrEmpty(data.SECONDARY_TYPE_ID))
                {
                    sql.Append("AND SECONDARY_TYPE_ID = @SECONDARY_TYPE_ID ");
                    parameters.Add("SECONDARY_TYPE_ID", data.SECONDARY_TYPE_ID);
                }

                if (!string.IsNullOrEmpty(data.DETAIL_TYPE_ID))
                {
                    sql.Append("AND DETAIL_TYPE_ID = @DETAIL_TYPE_ID ");
                    parameters.Add("DETAIL_TYPE_ID", data.DETAIL_TYPE_ID);
                }

                if (!string.IsNullOrEmpty(data.RESOURCE_ID))
                {
                    sql.Append("AND RESOURCE_ID = @RESOURCE_ID ");
                    parameters.Add("RESOURCE_ID", data.RESOURCE_ID);
                }

                if (!string.IsNullOrEmpty(data.RESOURCE_NAME))
                {
                    sql.Append("AND @RESOURCE_NAME ");
                    parameters.Add("RESOURCE_NAME", data.RESOURCE_NAME);
                }

                // 總數計算
                var total = conn.Query<RESOURCE_ITEM_ALLDto>(sql.ToString(), parameters).Count();

                if (data.SortName == null)
                {
                    sql.Append("ORDER BY MASTER_TYPE_ID " + data.SortOrder ?? " ");
                }
                else
                {
                    sql.Append("ORDER BY " + (data.SortName ?? " ") + " " + (data.SortOrder ?? " ") + " ");
                }


                if (data.Offset != 0 || data.Limit != 0)
                {
                    sql.Append("OFFSET " + data.Offset + " ROWS FETCH NEXT " + data.Limit + " ROWS ONLY ");
                }

                // 資料撈取
                var result = conn.Query<RESOURCE_ITEM_ALLDto>(sql.ToString(), parameters).ToList();


                return (total, result);
            }
        }

        /// <summary>
        /// 功能說明：查詢資料單筆
        /// </summary>
        /// <param name="data">查詢條件</param>
        /// <returns>RESOURCE_ITEM_ALLDto</returns>
        /// 開發人員            日期           異動內容                    解決的問題
        /// Joe             2019-10-22       新增此功能                     查詢資料
        public RESOURCE_ITEM_ALLDto EDD2_RESOURCE_ITEM(EDD2020504Dto data)
        {
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT * FROM EDD2_RESOURCE_ITEM_ALL WHERE 1 = 1 ");

                DynamicParameters parameters = new DynamicParameters();

                if (!string.IsNullOrEmpty(data.RESOURCE_ID))
                {
                    sql.Append("AND RESOURCE_ID = @RESOURCE_ID ");
                    parameters.Add("RESOURCE_ID", data.RESOURCE_ID);
                }

                // 資料撈取
                var result = conn.Query<RESOURCE_ITEM_ALLDto>(sql.ToString(), parameters).FirstOrDefault();

                return result;
            }
        }
    }

}
