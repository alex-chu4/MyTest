///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  EDD2Dao.cs
//  程式名稱：
//  EDD2 共用 Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-09-20       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  EDD2 共用 Dao
///////////////////////////////////////////////////////////////////////////////////////
using EMIC2.Models.Dao.Dto;
using EMIC2.Models.Interface.EDD2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMIC2.Models.Dao.Dto.EDD2;
using System.Data.SqlClient;
using EMIC2.Models.Helper;
using Dapper;
using System.Transactions;

namespace EMIC2.Models.Dao.EDD2
{
    public class EDD2Dao : IEDD2Dao
    {
        /// <summary>
        /// 資源項目
        /// </summary>
        /// <returns>List<RESOURCE_ITEM_ALLDto></returns>
        public RESOURCE_ITEM_ALLDto ERA2_RESOURCE_ITEM_ALL(ResourceItemModelDto data)
        {
            RESOURCE_ITEM_ALLDto result = new RESOURCE_ITEM_ALLDto();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                var sql = "select * from EDD2_RESOURCE_ITEM_ALL where RESOURCE_ID = @RESOURCE_ID";

                var parameters = new
                {
                    RESOURCE_ID = data.item_id,
                };

                result = conn.Query<RESOURCE_ITEM_ALLDto>(sql, parameters).FirstOrDefault();

                return result;
            }
        }

        /// <summary>
        /// 資源項目列表
        /// </summary>
        /// <returns>List<RESOURCE_ITEM_ALLDto></returns>
        public List<RESOURCE_ITEM_ALLDto> ERA2_RESOURCE_ITEM_ALL_LIST(ResourceItemModelDto data)
        {
            List<RESOURCE_ITEM_ALLDto> result = new List<RESOURCE_ITEM_ALLDto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                var sql = "select * from EDD2_RESOURCE_ITEM_ALL";

                result = conn.Query<RESOURCE_ITEM_ALLDto>(sql).ToList();

                return result;
            }
        }

        /// <summary>
        /// Dapper大量資料寫入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="list"></param>
        public void DapperToBulkInsert<T>(string sql, List<T> list)
        {
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                var execute = conn.Execute(sql, list);
            }
        }
    }
}
