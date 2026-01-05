///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA20502Dao.cs
//  程式名稱：
//  ERA20502Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-20       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA20502Dao
///////////////////////////////////////////////////////////////////////////////////////
using Dapper;
using EMIC2.Models.Dao.Dto.ERA.ERA20502;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA2.ERA20502;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EMIC2.Models.Dao.ERA.ERA20502
{
    public class ERA20502Dao : IERA20502Dao
    {
        /// <summary>
        /// 查詢未完成的處置報告項目
        /// </summary>
        /// <returns> ERA20502Dto</returns>
        public List<ERA20502Dto> ERA2_0502_M(ERA20502SearchModelDto data)
        {
            List<ERA20502Dto> result = new List<ERA20502Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql =
                    @"select Case when count(*) > 0 then 0 else 1 end disp_is_finish, 
                             count(*) disp_cnt
                      from ERA2_0502_M(@P_EOC_ID, @P_PRJ_NO, @ORG_ID)
                      where IS_WRITE_FINISH = 0";

                var parameters = new
                {
                    P_EOC_ID = data.eoc_id,
                    P_PRJ_NO = data.prj_no,
                    ORG_ID = data.org_id,
                };

                result = conn.Query<ERA20502Dto>(sql, parameters).ToList();

                return result;
            }
        }
    }
}
