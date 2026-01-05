///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  EDD2020302.cs
//  程式名稱：
//  EDD2020301Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-09-24       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//
///////////////////////////////////////////////////////////////////////////////////////
using Dapper;
using EMIC2.Models.Dao.Dto.EDD2.EDD2020302;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.EDD2.EDD2020302;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.EDD2.EDD2020302
{
    public class EDD2020302Dao : IEDD2020302Dao
    {
        /// <summary>
        /// 稽催填報進度查詢
        /// </summary>
        /// <returns>List<EDD2_020302_M></returns>
        public List<EDD2_020302_MDto> EDD2_020302_M(Dto.EDD2.EDD2020302.EDD2_020302_M_SearchModelDto data)
        {
            List<EDD2_020302_MDto> result = new List<EDD2_020302_MDto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT * FROM EDD2_020302_M (@P_UNIT_ID, @P_TIME_S, @P_TIME_E, @P_AUDITING_ID) ");

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("P_UNIT_ID", data.UNIT_ID);
                parameters.Add("P_TIME_S", data.TIME_S.Replace("-", string.Empty));
                parameters.Add("P_TIME_E", data.TIME_E.Replace("-", string.Empty));

                if (!string.IsNullOrEmpty(data.AUDITING_ID))
                {
                    parameters.Add("P_AUDITING_ID", data.AUDITING_ID);
                }
                else
                {
                    parameters.Add("P_AUDITING_ID", "-1");
                }

                if (data.status == "1")
                {
                    sql.Append(" where RECEIVE_STATUS = '1' ");
                }
                if (data.status == "0")
                {
                    sql.Append(" where RECEIVE_STATUS = '0' ");
                }

                sql.Append(" order by SEND_DATE ");

                result = conn.Query<EDD2_020302_MDto>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 稽催填報進度查詢
        /// </summary>
        /// <returns>List<EDD2_020302_M></returns>
        public List<EDD2_020302_DDto> EDD2_020302_M_Detail(Dto.EDD2.EDD2020302.EDD2_020302_M_SearchModelDto data)
        {
            List<EDD2_020302_DDto> result = new List<EDD2_020302_DDto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select * from EDD2_020302_D (@P_AUDITING_ID, @P_SEND_DATE) where 1 = 1 ");

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("P_AUDITING_ID", data.AUDITING_ID);
                parameters.Add("P_SEND_DATE", data.SEND_DATE.Replace("-", string.Empty));

                if (!string.IsNullOrEmpty(data.INPUT_TIME_S) && !string.IsNullOrEmpty(data.INPUT_TIME_E))
                {
                    sql.Append(" and CHECK_DATE >= @INPUT_TIME_S and CHECK_DATE <= @INPUT_TIME_E ");
                    parameters.Add("INPUT_TIME_S", data.INPUT_TIME_S);
                    parameters.Add("INPUT_TIME_E", data.INPUT_TIME_E);
                }
                if (!string.IsNullOrEmpty(data.UNIT_NAME))
                {
                    sql.Append(" and UNIT_NAME like @P_UNIT_NAME");
                    parameters.Add("P_UNIT_NAME", '%' + data.UNIT_NAME + '%');
                }
                if (data.delay != "-1")
                {
                    sql.Append(" and DELAY_STATUS = @P_DELAY_STATUS");
                    parameters.Add("P_DELAY_STATUS", data.delay);
                }
                if (data.status != "-1")
                {
                    sql.Append(" and CHECK_STATUS like @P_CHECK_STATUS");
                    parameters.Add("P_CHECK_STATUS", data.status);
                }

                result = conn.Query<EDD2_020302_DDto>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 稽催填報進度，稽催作業下拉選單
        /// </summary>
        /// <returns>List<EDD2_020302_M></returns>
        public List<EDD2_AUDITING_OPERATION> EDD2_AUDITING_OPERATION(Dto.EDD2.EDD2020302.EDD2_020302_M_SearchModelDto data)
        {
            List<EDD2_AUDITING_OPERATION> result = new List<EDD2_AUDITING_OPERATION>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM EDD2_AUDITING_OPERATION WHERE 1 = 1 ");

                DynamicParameters parameters = new DynamicParameters();

                if (!string.IsNullOrEmpty(data.TIME_S) && !string.IsNullOrEmpty(data.TIME_E))
                {
                    sql.Append(" AND START_DATE >= @TIME_S AND START_DATE <= @TIME_E ");
                    parameters.Add("TIME_S", data.TIME_S);
                    parameters.Add("TIME_E", data.TIME_E);
                }
                sql.Append(" ORDER BY START_DATE ");

                result = conn.Query<EDD2_AUDITING_OPERATION>(sql.ToString(), parameters).ToList();

                return result;
            }
        }
    }
}
