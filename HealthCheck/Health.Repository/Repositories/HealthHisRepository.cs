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
    public class HealthHisRepository : BaseRepository, IHealthHisRepository
    {
        public async Task Insert(HealthHisDto healthHis)
        {
            string sql = "INSERT HEALTH_HIS(SYSTEM_ID,VM_ID,VM_IPv4,VM_IPv6,ISVIP,STATUS,TOTAL_TIME,SYSTEM_TIME,DB_TIME,CREATE_TIME) " +
                         "VALUES(@SYSTEM_ID,@VM_ID,@VM_IPv4,@VM_IPv6,@ISVIP,@STATUS,@TOTAL_TIME,@SYSTEM_TIME,@DB_TIME,@CREATE_TIME) ";
            //設定參數
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SYSTEM_ID", healthHis.SYSTEM_ID);
            parameters.Add("@VM_ID", healthHis.VM_ID);
            parameters.Add("@VM_IPv4", healthHis.VM_IPv4);
            parameters.Add("@VM_IPv6", healthHis.VM_IPv6);
            parameters.Add("@ISVIP", healthHis.ISVIP);
            parameters.Add("@STATUS", healthHis.STATUS);
            parameters.Add("@TOTAL_TIME", healthHis.TOTAL_TIME);
            parameters.Add("@SYSTEM_TIME", healthHis.SYSTEM_TIME);
            parameters.Add("@DB_TIME", healthHis.DB_TIME);
            parameters.Add("@CREATE_TIME", DateTime.Now);

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<IEnumerable<HealthHisDto>> Search(HealthHisSearchFilter filter)
        {
            StringBuilder sql =
                new StringBuilder(
                    "SELECT SYSTEM_ID," +
                    "VM_ID," +
                    "VM_IPv4," +
                    "VM_IPv6," +
                    "ISVIP," +
                    "STATUS," +
                    "TOTAL_TIME," +
                    "SYSTEM_TIME," +
                    "DB_TIME," +
                    "CREATE_TIME " +
                    "FROM HEALTH_HIS " +
                    "WHERE SYSTEM_ID=@SYSTEM_ID " +
                    "AND VM_ID=@VM_ID ");
            //設定參數
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SYSTEM_ID", filter.SYSTEM_ID);
            parameters.Add("@VM_ID", filter.VM_ID);

            if (filter.START_TIME.HasValue)
            {
                sql.Append("AND CREATE_TIME>=@START_TIME ");
                parameters.Add("@START_TIME", filter.START_TIME.Value);
            }

            if (filter.END_TIME.HasValue)
            {
                sql.Append("AND CREATE_TIME<=@END_TIME");
                parameters.Add("@END_TIME", filter.END_TIME.Value);
            }

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                IEnumerable<HealthHisDto> result = await connection.QueryAsync<HealthHisDto>(sql.ToString(), parameters);
                return result;
            }
        }
    }
}
