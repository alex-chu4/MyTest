using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using Health.Repository.Dto;
using Health.Repository.Interfaces;

namespace Health.Repository.Repositories
{
    public class HealthCurrentRepository : BaseRepository, IHealthCurrentRepository
    {
        public async Task<IEnumerable<HealthCurrentViewDto>> GetCurrentView()
        {
            string sql = "SELECT HT.SYSTEM_ID," +
                         "(SELECT TOP 1 [NAME] FROM SYSTEM_INFO WHERE ID = HT.SYSTEM_ID) AS SYSTEM_NAME," +
                         "HT.VM_ID," +
                         "HT.VM_IPv4," +
                         "HT.VM_IPv6," +
                         "HT.ISVIP," +
                         "ISNULL(HC.[STATUS],CONVERT(BIT, 0)) AS [STATUS] " +
                         "FROM HEALTH_TARGET AS HT " +
                         "LEFT JOIN HEALTH_CURRENT AS HC ON HC.SYSTEM_ID = HT.SYSTEM_ID AND HC.VM_ID = HT.VM_ID " +
                         "WHERE ACTIVE = 1 " +
                         "ORDER BY HT.SYSTEM_ID,HT.VM_ID ";

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                IEnumerable<HealthCurrentViewDto> result = await connection.QueryAsync<HealthCurrentViewDto>(sql);
                return result;
            }
        }

        public async Task Insert(HealthCurrentDto healthCurrent)
        {
            string sql = "INSERT HEALTH_CURRENT(SYSTEM_ID,VM_ID,VM_IPv4,VM_IPv6,ISVIP,STATUS,CREATE_TIME) " +
                         "VALUES(@SYSTEM_ID,@VM_ID,@VM_IPv4,@VM_IPv6,@ISVIP,@STATUS,@CREATE_TIME) ";

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@SYSTEM_ID", healthCurrent.SYSTEM_ID);
                parameters.Add("@VM_ID", healthCurrent.VM_ID);
                parameters.Add("@VM_IPv4", healthCurrent.VM_IPv4);
                parameters.Add("@VM_IPv6", healthCurrent.VM_IPv6);
                parameters.Add("@ISVIP", healthCurrent.ISVIP);
                parameters.Add("@STATUS", healthCurrent.STATUS);
                parameters.Add("@CREATE_TIME", DateTime.Now);

                await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<bool> IsExist(HealthCurrentDto healthCurrent)
        {
            string sql = "SELECT COUNT(*) " +
                         "FROM HEALTH_CURRENT " +
                         "WHERE SYSTEM_ID=@SYSTEM_ID " +
                         "AND VM_ID=@VM_ID ";

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@SYSTEM_ID", healthCurrent.SYSTEM_ID);
                parameters.Add("@VM_ID", healthCurrent.VM_ID);

                return await connection.ExecuteScalarAsync<int>(sql, parameters) > 0;
            }
        }

        public async Task Update(HealthCurrentDto healthCurrent)
        {
            string sql = "UPDATE HEALTH_CURRENT SET " +
                    "VM_IPv4=@VM_IPv4," +
                    "VM_IPv6=@VM_IPv6," +
                    "ISVIP=@ISVIP," +
                    "STATUS=@STATUS," +
                    "UPDATE_TIME=@UPDATE_TIME " +
                    "WHERE SYSTEM_ID=@SYSTEM_ID " +
                    "AND VM_ID=@VM_ID ";

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@VM_IPv4", healthCurrent.VM_IPv4);
                parameters.Add("@VM_IPv6", healthCurrent.VM_IPv6);
                parameters.Add("@ISVIP", healthCurrent.ISVIP);
                parameters.Add("@STATUS", healthCurrent.STATUS);
                parameters.Add("@UPDATE_TIME", DateTime.Now);
                parameters.Add("@SYSTEM_ID", healthCurrent.SYSTEM_ID);
                parameters.Add("@VM_ID", healthCurrent.VM_ID);

                await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
