///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA20501Dao.cs
//  程式名稱：
//  ERA20501Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-21       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA20501Dao
///////////////////////////////////////////////////////////////////////////////////////
using Dapper;
using EMIC2.Models.Dao.Dto.ERA.ERA20501;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA2.ERA20501;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EMIC2.Models.Dao.ERA.ERA20501
{
    public class ERA20501Dao : IERA20501Dao
    {
        /// <summary>
        /// 查詢未完成的處置報告項目
        /// </summary>
        /// <returns> ERA20501Dto</returns>
        public List<ERA20501Dto> ERA2_0501_M(ERA20501SearchModelDto data)
        {
            List<ERA20501Dto> result = new List<ERA20501Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql =
                    @"select Case when count(*) > 0 then 0 else 1 end rpt_is_finish, 
                             count(*) rpt_cnt
                      from ERA2_0501_M (@P_EOC_ID, @P_PRJ_NO, @ORG_ID)
                      where IS_WRITE_FINISH = 0";

                var parameters = new
                {
                    P_EOC_ID = data.eoc_id,
                    P_PRJ_NO = data.prj_no,
                    ORG_ID = data.org_id,
                };

                result = conn.Query<ERA20501Dto>(sql, parameters).ToList();

                return result;
            }
        }
    }
}
