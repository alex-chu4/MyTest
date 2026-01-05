using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using Health.Repository.Interfaces;

namespace Health.Repository.Repositories
{
    public class UserTokenRepository : BaseRepository, IUserTokenRepository
    {
        public async Task<int> GetCurrentUsers()
        {
            string sql = "SELECT COUNT(*) FROM SSO2_USER_TOKEN WHERE EXP_TIME >= GETDATE()";

            using (SqlConnection connection = new SqlConnection(Emic2ConnectionString))
            {
                return Convert.ToInt32(await connection.ExecuteScalarAsync(sql));
            }
        }
    }
}
