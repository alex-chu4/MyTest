using System;
using System.Data;
using System.Data.SqlClient;

namespace Utility.Helper
{
    public static class ConnectionHelper
    {
        public static R Connect<R>(string connString, Func<IDbConnection, R> exceFunc)
        {
            using (var conn = new SqlConnection(connString))
            {
                return exceFunc(conn);
            }
        }
    }
}
