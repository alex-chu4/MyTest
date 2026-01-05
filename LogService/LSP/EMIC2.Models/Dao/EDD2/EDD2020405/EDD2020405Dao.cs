///////////////////////////////////////////////////////////////////////////////////////
//  程式名稱：
//  EDD2020405Dao.cs
//  程式描述：
//  EDD2020405Dao.cs
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員      日期           方法名稱          版本      功能說明
//  timan         2019-10-3      EDD2_020405_M     1.0.0     EDD2_020405_M查詢
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
///////////////////////////////////////////////////////////////////////////////////////
using Dapper;
using EMIC2.Models.Dao.Dto.EDD2.EDD2020405;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.EDD2.EDD2020405;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.EDD2.EDD2020405
{
    public class EDD2020405Dao : IEDD2020405Dao
    {
        /// <summary>
        /// 單位填報紀錄列表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<EDD2_020405_MDto> EDD2_020405_M(EDD2020405SearchModelDto data)
        {
            List<EDD2_020405_MDto> result = new List<EDD2_020405_MDto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select * from EDD2_020405_M (@P_UNIT_ID, @P_CHECK_DATE_S, @P_CHECK_DATE_E) where 1 = 1 ");

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("P_UNIT_ID", data.UNIT_ID);
                parameters.Add("P_CHECK_DATE_S", data.datetimes.Replace("-", string.Empty));
                parameters.Add("P_CHECK_DATE_E", data.datetimee.Replace("-", string.Empty));

                if (!string.IsNullOrEmpty(data.CITY_NAME))
                {
                    sql.Append(" and CITY_NAME = @P_CITY_NAME ");
                    parameters.Add("P_CITY_NAME", data.CITY_NAME);
                }
                if (!string.IsNullOrEmpty(data.TOWN_NAME))
                {
                    sql.Append(" and TOWN_NAME = @P_TOWN_NAME ");
                    parameters.Add("P_TOWN_NAME", data.TOWN_NAME);
                }
                if (!string.IsNullOrEmpty(data.UNIT_NAME))
                {
                    sql.Append(" and UNIT_NAME like @P_UNIT_NAME ");
                    parameters.Add("P_UNIT_NAME", '%' + data.UNIT_NAME + '%');
                }
                if (data.status != -1)
                {
                    sql.Append(" and CHECK_STATUS = @CHECK_STATUS ");
                    parameters.Add("CHECK_STATUS", data.status);
                }

                sql.Append(" order by UNIT_LEVEL, OID ");

                result = conn.Query<EDD2_020405_MDto>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 檢視異動狀態列表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<EDD2_020405_DDto> EDD2_020405_D(EDD2020405SearchModelDto data)
        {
            List<EDD2_020405_DDto> result = new List<EDD2_020405_DDto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select * from EDD2_020405_D (@UNIT_ID, @MAX_CHECK_DATE) where 1 = 1 ");

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("UNIT_ID", data.UNIT_ID);
                parameters.Add("MAX_CHECK_DATE", data.MAX_CHECK_DATE.Replace("-", string.Empty));

                if (!string.IsNullOrEmpty(data.UNIT_NAME))
                {
                    sql.Append("and RESOURCE_NAME like @RESOURCE_NAME");
                    parameters.Add("RESOURCE_NAME", '%' + data.RESOURCE_NAME + '%');
                }

                sql.Append(" order by RESOURCE_ID, CHECK_DATE ");

                result = conn.Query<EDD2_020405_DDto>(sql.ToString(), parameters).ToList();

                return result;
            }
        }
    }
}
