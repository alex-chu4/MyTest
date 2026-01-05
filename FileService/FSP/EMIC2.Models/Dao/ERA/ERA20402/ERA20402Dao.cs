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
using EMIC2.Models.Interface.ERA2.ERA;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace EMIC2.Models.Dao.ERA.ERA20402
{
    public class ERA20402Dao : IERA20402Dao
    {
        public object DateTimeExtensions { get; private set; }

        /// <summary>
        /// 下層應變中心最新填報狀況，呼叫 Stored Procedure 回傳資料
        /// </summary>
        /// <returns> IEnumerable<ERA20402Dto></returns>
        public IEnumerable<ERA20402Dto> GetData(ERA20402Dto data)
        {
            IEnumerable<ERA20402Dto> resultDtos = new List<ERA20402Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                // 此日期參數進 db前需要轉為 int            
                string RPT_TIME = data.P_RPT_TIME?.ToString("yyyyMMddHHmmss");

                string sql =
                   "Select * from " + "[dbo].[ERA2_0402_M](@EOC_ID, @P_EOC_ID, @PRJ_NO, @RPT_TIME, @P_HOUR )";

                var parameters = new
                {
                    EOC_ID = data.EOC_ID,
                    P_EOC_ID = data.P_EOC_ID,
                    PRJ_NO = data.PRJ_NO,
                    RPT_TIME = RPT_TIME,
                    P_HOUR = data.P_HOUR,
                };

                var query = conn.Query<ERA20402Dto>(sql, parameters);

                return query;
            }

        }
    }
}
