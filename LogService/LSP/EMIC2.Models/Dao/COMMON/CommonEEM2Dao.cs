///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  CommonEEM2Dao.cs
//  程式名稱：
//  執行SQL User Defined Function EEM2_QRY_EOC_PRJ
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期              版本              備註
//  Enosh           2019-08-26       1.0.0.0           初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  切換應變中心與專案時須使用到的功能
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Dao.COMMON
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    using Dapper;
    using EMIC2.Models.Dao.Dto.COMMON;
    using EMIC2.Models.Interface.COMMON;
    using EMIC2.Models.Helper;

    public class CommonEEM2Dao : ICommonEEM2Dao
    {
        public IEnumerable<EEM2_QRY_EOC_PRJ_Dto> Get_EEM2_QRY_EOC_PRJ(string orgId)
        {
            string sql = "SELECT EOC_ID,EOC_NAME,PRJ_NO,PRJ_NAME,ISNULL(OPEN_LV,'') AS OPEN_LV FROM EEM2_QRY_EOC_PRJ(@P_ORG_ID)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@P_ORG_ID", orgId);

            using (SqlConnection connection = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                return connection.Query<EEM2_QRY_EOC_PRJ_Dto>(sql, parameters);
            }
        }

        public int Get_EEM2_QRY_WORK_NOT_REPLY_COUNT(OrgEocPrjDto dto)
        {
            string sql = "SELECT dbo.EEM2_QRY_WORK_NOT_REPLY_COUNT(@P_PRJ_NO,@P_ORG_ID)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@P_PRJ_NO", dto.PRJ_NO);
            parameters.Add("@P_ORG_ID", dto.ORG_ID);

            using (SqlConnection connection = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                return Convert.ToInt32(connection.ExecuteScalar(sql, parameters));
            }
        }
    }
}

