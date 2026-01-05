
///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA20403Dao.cs
//  程式名稱：
//  處置報告最新填報狀況SPEC
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-04-26       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  各機關處置報告填報狀況，時所使用Dao
///////////////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using EMIC2.Models.Dao.Dto.ERA.ERA20403;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA2.ERA20403;
using EMIC2.Result;

namespace EMIC2.Models.Dao.ERA.ERA20403
{
    public class ERA20403Dao : IERA20403Dao
    {
        /// <summary>
        /// 處置報告最新填報狀況SPEC資訊，呼叫 Stored Procedure 回傳資料
        /// </summary>
        /// <returns> IEnumerable<ERA20401Dto></returns>
        public IEnumerable<ERA20403Dto> GetData(ERA20403Dto data)
        {
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@P_EOC_ID", data.EOC_ID, dbType: DbType.String, direction: ParameterDirection.Input);
                parameters.Add("@PRJ_NO", data.P_PRJ_NO, dbType: DbType.Int64, direction: ParameterDirection.Input);
                parameters.Add("@P_HOUR", data.P_HOUR, dbType: DbType.Int32, direction: ParameterDirection.Input);

                string strSql = @"SELECT ORG_NAME, NOT_DISP_CNT, NOT_DISP_ITEM, ORG_ID, SHOW_ORDER 
                                    FROM [dbo].[ERA2_0403_M] ( @P_EOC_ID, @PRJ_NO, @P_HOUR) ORDER BY SHOW_ORDER";
                var results = conn.Query<ERA20403Dto>(strSql, parameters);

                return results;
            }
        }
    }
}
