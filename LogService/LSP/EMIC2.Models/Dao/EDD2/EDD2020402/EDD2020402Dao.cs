///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  EDD2020402.cs
//  程式名稱：
//  EDD2020402Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-10-07       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//
///////////////////////////////////////////////////////////////////////////////////////
using Dapper;
using EMIC2.Models.Dao.Dto.EDD2.EDD2020402;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.EDD2.EDD2020402;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.EDD2.EDD2020402
{
    public class EDD2020402Dao : IEDD2020402Dao
    {
        /// <summary>
        /// 稽催填報進度查詢
        /// </summary>
        /// <returns>List<EDD2_020402_MDto></returns>
        public List<EDD2_020402_MDto> EDD2_020402_M(EDD2020402SearchModelDto data)
        {
            List<EDD2_020402_MDto> result = new List<EDD2_020402_MDto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select * from EDD2_020402_M (@P_UNIT_ID, @P_MASTER_TYPE_ID, @P_SECONDARY_TYPE_ID, @P_DETAIL_TYPE_ID, @P_RESOURCE_ID, @P_TIME_S, @P_TIME_E, @P_UNIT_NAME) order by UNIT_LEVEL, UNIT_NAME, CHECK_DATE ");

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("P_UNIT_ID", data.UNIT_ID); 
                parameters.Add("P_MASTER_TYPE_ID", data.MASTER_TYPE_ID); 
                parameters.Add("P_SECONDARY_TYPE_ID", data.SECONDARY_TYPE_ID); 
                parameters.Add("P_DETAIL_TYPE_ID", data.DETAIL_TYPE_ID); 
                parameters.Add("P_RESOURCE_ID", data.RESOURCE_ID);
                parameters.Add("P_TIME_S", data.datetimes.Replace("-", string.Empty));
                parameters.Add("P_TIME_E", data.datetimee.Replace("-", string.Empty));
                parameters.Add("P_UNIT_NAME", data.UNIT_NAME);

                result = conn.Query<EDD2_020402_MDto>(sql.ToString(), parameters).ToList();

                return result;
            }
        }
    }
}
