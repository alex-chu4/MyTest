///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  CommonNTT2Dao.cs
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

    public class CommonNTT2Dao : ICommonNTT2Dao
    {
        public int Get_NTT2_QRY_CASE_COUNT(OrgEocPrjDto dto)
        {
            string sql = "SELECT dbo.NTT2_QRY_CASE_COUNT(@P_EOC_ID)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@P_EOC_ID", dto.EOC_ID);

            using (SqlConnection connection = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                return Convert.ToInt32(connection.ExecuteScalar(sql, parameters));
            }
        }
    }
}
