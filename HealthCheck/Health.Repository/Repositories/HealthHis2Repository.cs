using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using Health.Repository.Dto;
using Health.Repository.Interfaces;

namespace Health.Repository.Repositories
{
    public class HealthHis2Repository : BaseRepository, IHealthHis2Repository
    {
        public async Task<IEnumerable<HealthHis2Dto>> Search(HealthHis2SearchFilter filter)
        {
            StringBuilder sql =
                new StringBuilder(
                    "SELECT SYSTEM_ID," +
                    "FUNCTION_CODE," +
                    "IPv4," +
                    "THRESHOLD," +
                    "TOTAL_TIME," +
                    "SYSTEM_TIME," +
                    "DB_TIME," +
                    "CREATE_TIME " +
                    "FROM HEALTH_HIS2 " +
                    "WHERE SYSTEM_ID=@SYSTEM_ID ");
            //設定參數
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SYSTEM_ID", filter.SYSTEM_ID);

            if (!string.IsNullOrEmpty(filter.FUNCTION_CODE))
            {
                sql.Append("AND FUNCTION_CODE=@FUNCTION_CODE ");
                parameters.Add("@FUNCTION_CODE", filter.FUNCTION_CODE);
            }

            if (filter.START_TIME.HasValue)
            {
                sql.Append("AND CREATE_TIME>=@START_TIME ");
                parameters.Add("@START_TIME", filter.START_TIME.Value);
            }

            if (filter.END_TIME.HasValue)
            {
                sql.Append("AND CREATE_TIME<=@END_TIME ");
                parameters.Add("@END_TIME", filter.END_TIME.Value);
            }

            if (true.Equals(filter.OVER_THRESHOLD))
            {
                sql.Append("AND TOTAL_TIME>THRESHOLD ");
            }

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                IEnumerable<HealthHis2Dto> result = await connection.QueryAsync<HealthHis2Dto>(sql.ToString(), parameters);
                return result;
            }
        }

        public async Task<int> UpdateThreshold(HealthHis2Dto healthHis2)
        {
            DynamicParameters parameters = new DynamicParameters();
            string sql = "UPDATE HEALTH_HIS2 SET THRESHOLD=@THRESHOLD WHERE SYSTEM_ID=@SYSTEM_ID AND THRESHOLD IS NULL ";

            parameters.Add("@THRESHOLD", healthHis2.THRESHOLD.Value);
            parameters.Add("@SYSTEM_ID", healthHis2.SYSTEM_ID);

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
