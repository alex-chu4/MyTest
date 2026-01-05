///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  EDD2020301Dao.cs
//  程式名稱：
//  EDD2020301Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-09-24       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  介面
///////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using EMIC2.Models.Dao.Dto.EDD2.EDD2020301;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.EDD2.EDD2020301;

namespace EMIC2.Models.Dao.EDD2
{
    public class EDD2020301Dao : IEDD2020301Dao
    {
        public List<EDD2020301Dto> GetEDD2_QRY_UNIT_OWN(int unitId, int qryType, int limit, int offset)
        {
            List<EDD2020301Dto> result = new List<EDD2020301Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                var orderby = string.Empty;
                if (qryType == 1 || qryType == 2)
                {
                    orderby = " order by UNIT_LEVEL, OID";
                }
                else
                {
                    orderby = " order by CITY_NAME, UNIT_LEVEL, OID";
                }

                var sql = "select * from EDD2_QRY_UNIT_OWN(@P_UNIT_ID, @P_QRY_TYPE)" + orderby;




                //unitId = 14;
                //qryType = 1;
                var parameters = new
                {
                    P_UNIT_ID = unitId,
                    P_QRY_TYPE = qryType,
                };

                var query = conn.Query<EDD2020301Dto>(sql, parameters);
                result = query.ToList();

                if (qryType == 4)
                {
                    result = result.Skip(offset).Take(limit).ToList();
                }


                return result;
            }
        }
    }
}
