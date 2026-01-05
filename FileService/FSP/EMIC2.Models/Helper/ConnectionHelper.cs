///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ConnectionHelper.cs
//  程式名稱：
//  提供資料庫連線並且執行SQL敘述Helper
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Jeter             2019/08/28       1.0.0.0      無
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供資料庫連線並且執行SQL敘述。
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Data;
using System.Data.SqlClient;

namespace EMIC2.Models.Helper
{
    public static class ConnectionHelper
    {
        private static string connString;
        static ConnectionHelper()
        {
            connString = DBHelper.GetEMIC2DBConnection();
        }

        public static R Connect<R>(string connString, Func<IDbConnection, R> execFunc)
        {
            using(var conn = new SqlConnection(connString))
            {
                return execFunc(conn);
            }
        }

        public static R Connect<R>(Func<IDbConnection, R> execFunc)
        {
            using (var conn = new SqlConnection(connString))
            {
                return execFunc(conn);
            }
        }
    }
}
