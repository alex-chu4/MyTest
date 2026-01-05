///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  CommonDIM2Dao.cs
//  程式名稱：
//  搜尋
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期              版本              備註
//  Enosh           2019-09-04       1.0.0.0           初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  待辦事項使用
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Dao.COMMON
{
    using System;
    using System.Data.SqlClient;

    using Dapper;
    using EMIC2.Models.Dao.Dto.COMMON;
    using EMIC2.Models.Helper;
    using EMIC2.Models.Interface.COMMON;

    public class CommonDIM2Dao : ICommonDIM2Dao
    {
        public int Get_DIM2_QRY_CASE_NOT_ASSIGN_COUNT(OrgEocPrjDto dto)
        {
            string sql = "SELECT dbo.DIM2_QRY_CASE_NOT_ASSIGN_COUNT(@P_EOC_ID,@P_PRJ_NO)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@P_EOC_ID", dto.EOC_ID);
            parameters.Add("@P_PRJ_NO", dto.PRJ_NO);

            using (SqlConnection connection = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                return Convert.ToInt32(connection.ExecuteScalar(sql, parameters));
            }
        }

        public int Get_DIM2_QRY_CASE_NOT_REPLY_COUNT(OrgEocPrjDto dto)
        {
            string sql = "SELECT dbo.DIM2_QRY_CASE_NOT_REPLY_COUNT(@P_PRJ_NO,@P_ORG_ID)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@P_PRJ_NO", dto.PRJ_NO);
            parameters.Add("@P_ORG_ID", dto.ORG_ID);

            using (SqlConnection connection = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                return Convert.ToInt32(connection.ExecuteScalar(sql, parameters));
            }
        }

        public int Get_DIM2_QRY_TASK_NOT_REPLY_COUNT(OrgEocPrjDto dto)
        {
            string sql = "SELECT dbo.DIM2_QRY_TASK_NOT_REPLY_COUNT(@P_PRJ_NO,@P_ORG_ID)";
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
