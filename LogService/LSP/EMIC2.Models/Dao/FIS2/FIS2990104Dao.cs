///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  FIS2990104Dao.cs
//  程式名稱：
//  排程
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本         備註
//  Chrissy          2019-09-20       1.0.0.0      初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  至3大資料抓資料
///////////////////////////////////////////////////////////////////////////////////////
using Dapper;
using EMIC2.Models.Dao.Dto.FIS2;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.FIS2;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EMIC2.Models.Dao.FIS2
{
    public class FIS2990104Dao: IFIS2990104Dao
    {
        /// <summary>
        /// 撤離名冊
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>FIS2990104Dto</returns>
        public FIS2990104Dto Get_QryEvaReport(FIS2990104Dto model)
        {
            FIS2990104Dto dto = new FIS2990104Dto();
            dto.QryEvaRows = ConnectionHelper.Connect(DBHelper.GetEMIC2DBConnection(),
                c => c.Query<QryEvaRows>(@"select * from EEA2_QRY_EVA_REPORT(@Start_Time, @End_Time)", model).ToList());
            return dto;
        }

        /// <summary>
        /// 收容名冊
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>FIS2990104Dto</returns>
        public FIS2990104Dto Get_QryShReport(FIS2990104Dto model)
        {
            FIS2990104Dto dto = new FIS2990104Dto();
            dto.QryShRows = ConnectionHelper.Connect(DBHelper.GetEMIC2DBConnection(),
                c => c.Query<QryShRows>(@"select * from EEA2_QRY_SH_REPORT(@Start_Time, @End_Time)", model).ToList());
            return dto;
        }

        /// <summary>
        /// 處置報告
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>FIS2990104Dto</returns>
        public FIS2990104Dto Get_QryMaxPeopleMissing(List<FIS2990104Dto> model)
        {
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                FIS2990104Dto dto = new FIS2990104Dto();
                StringBuilder sql = new StringBuilder();
                DynamicParameters paras = new DynamicParameters();

                sql.Append(" select * from ERA2_QRY_MAX_PEOPLE_MISSING(@Eoc_ID, @Prj_No, null) ");
                paras.Add("Eoc_ID", model[0].Eoc_ID);
                paras.Add("Prj_No", model[0].Prj_No);

                for (int i = 1; i < model.Count(); i++)
                {
                    sql.Append(" union all ");
                    sql.Append($" ( select * from ERA2_QRY_MAX_PEOPLE_MISSING(@Eoc_ID{i}, @Prj_No{i}, null) ) ");
                    paras.Add("Eoc_ID"+i, model[i].Eoc_ID);
                    paras.Add("Prj_No"+i, model[i].Prj_No);
                }

                dto.QryMaxPeopleMissingRows = conn.Query<QryMaxPeopleMissingRows>(sql.ToString(), paras).ToList();
                return dto;
            }
        }
    }
}
