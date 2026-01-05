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
    public class HealthTargetRepository : BaseRepository, IHealthTargetRepository
    {
        public async Task<IEnumerable<HealthTargetDto>> GetActiveTarget()
        {
            string sql = "SELECT OID," +
                         "SYSTEM_ID," +
                         "VM_ID," +
                         "VM_IPv4," +
                         "VM_IPv6," +
                         "ISVIP," +
                         "URL," +
                         "ACTIVE," +
                         "CREATE_TIME," +
                         "UPDATE_TIME " +
                         "FROM HEALTH_TARGET " +
                         "WHERE ACTIVE = 1 ";

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                IEnumerable<HealthTargetDto> result = await connection.QueryAsync<HealthTargetDto>(sql);
                return result;
            }
        }

        public async Task<int> Create(HealthTargetDto healthTarget)
        {
            string sql = "INSERT INTO HEALTH_TARGET(SYSTEM_ID,VM_ID,VM_IPv4,VM_IPv6,ISVIP,URL,ACTIVE,CREATE_TIME) " +
                         "VALUES(@SYSTEM_ID,@VM_ID,@VM_IPv4,@VM_IPv6,@ISVIP,@URL,@ACTIVE,GETDATE()) ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SYSTEM_ID", healthTarget.SYSTEM_ID);
            parameters.Add("@VM_ID", healthTarget.VM_ID);
            parameters.Add("@VM_IPv4", healthTarget.VM_IPv4);
            parameters.Add("@VM_IPv6", healthTarget.VM_IPv6);
            parameters.Add("@ISVIP", healthTarget.ISVIP);
            parameters.Add("@URL", healthTarget.URL);
            parameters.Add("@ACTIVE", healthTarget.ACTIVE);

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<int> Delete(HealthTargetDto healthTarget)
        {
            string sql = "DELETE HEALTH_TARGET WHERE OID=@OID ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OID", healthTarget.OID);

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<int> Update(HealthTargetDto healthTarget)
        {
            string sql = "UPDATE HEALTH_TARGET SET " +
                         "SYSTEM_ID=@SYSTEM_ID," +
                         "VM_ID=@VM_ID," +
                         "VM_IPv4=@VM_IPv4," +
                         "VM_IPv6=@VM_IPv6," +
                         "ISVIP=@ISVIP," +
                         "URL=@URL," +
                         "ACTIVE=@ACTIVE," +
                         "UPDATE_TIME=GETDATE() " +
                         "WHERE OID=@OID ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SYSTEM_ID", healthTarget.SYSTEM_ID);
            parameters.Add("@VM_ID", healthTarget.VM_ID);
            parameters.Add("@VM_IPv4", healthTarget.VM_IPv4);
            parameters.Add("@VM_IPv6", healthTarget.VM_IPv6);
            parameters.Add("@ISVIP", healthTarget.ISVIP);
            parameters.Add("@URL", healthTarget.URL);
            parameters.Add("@ACTIVE", healthTarget.ACTIVE);
            parameters.Add("@OID", healthTarget.OID);

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<IEnumerable<HealthTargetDto>> Search(HealthTargetSearchFilter filter)
        {
            DynamicParameters parameters = new DynamicParameters();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT OID,SYSTEM_ID,VM_ID,VM_IPv4,VM_IPv6,ISVIP,URL,ACTIVE,CREATE_TIME,UPDATE_TIME " +
                       "FROM HEALTH_TARGET " +
                       "WHERE 1=1 ");

            if (!string.IsNullOrEmpty(filter.SYSTEM_ID))
            {
                sql.Append("AND SYSTEM_ID=@SYSTEM_ID ");
                parameters.Add("@SYSTEM_ID", filter.SYSTEM_ID);
            }

            if (!string.IsNullOrEmpty(filter.VM_IPv4))
            {
                sql.Append("AND VM_IPv4=@VM_IPv4 ");
                parameters.Add("@VM_IPv4", filter.VM_IPv4);
            }

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return await connection.QueryAsync<HealthTargetDto>(sql.ToString(), parameters);
            }
        }
    }
}
