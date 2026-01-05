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
    public class VMInfoRepository : BaseRepository, IVMInfoRepository
    {
        public async Task<int> Create(VMInfoDto vmInfo)
        {
            string sql = "INSERT INTO VM_INFO(ID,NAME,IPv4,IPv6,ISVIP,MEMO,CREATE_TIME) " +
                         "VALUES(@ID,@NAME,@IPv4,@IPv6,@ISVIP,@MEMO,GETDATE()) ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", vmInfo.ID);
            parameters.Add("@NAME", vmInfo.NAME);
            parameters.Add("@IPv4", vmInfo.IPv4);
            parameters.Add("@IPv6", vmInfo.IPv6);
            parameters.Add("@ISVIP", vmInfo.ISVIP);
            parameters.Add("@MEMO", vmInfo.MEMO);

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<int> Delete(VMInfoDto vmInfo)
        {
            string sql = "DELETE VM_INFO WHERE OID=@OID ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OID", vmInfo.OID);

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<int> Update(VMInfoDto vmInfo)
        {
            string sql = "UPDATE VM_INFO SET " +
                         "ID=@ID," +
                         "NAME=@NAME," +
                         "IPv4=@IPv4," +
                         "IPv6=@IPv6," +
                         "ISVIP=@ISVIP," +
                         "MEMO=@MEMO," +
                         "UPDATE_TIME=GETDATE() " +
                         "WHERE OID=@OID ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", vmInfo.ID);
            parameters.Add("@NAME", vmInfo.NAME);
            parameters.Add("@IPv4", vmInfo.IPv4);
            parameters.Add("@IPv6", vmInfo.IPv6);
            parameters.Add("@ISVIP", vmInfo.ISVIP);
            parameters.Add("@MEMO", vmInfo.MEMO);
            parameters.Add("@OID", vmInfo.OID);

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<IEnumerable<VMInfoDto>> Search(VMInfoSearchFilter filter)
        {
            DynamicParameters parameters = new DynamicParameters();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT OID,ID,NAME,IPv4,IPv6,ISVIP,MEMO,CREATE_TIME,UPDATE_TIME " +
                       "FROM VM_INFO " +
                       "WHERE 1=1 ");

            if (!string.IsNullOrEmpty(filter.ID))
            {
                sql.Append("AND ID=@ID ");
                parameters.Add("@ID", filter.ID);
            }

            if (!string.IsNullOrEmpty(filter.NAME))
            {
                sql.Append("AND NAME LIKE '%@NAME%' ");
                parameters.Add("@NAME", filter.NAME);
            }

            if (!string.IsNullOrEmpty(filter.IPv4))
            {
                sql.Append("AND IPv4=@IPv4 ");
                parameters.Add("@IPv4", filter.IPv4);
            }

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return await connection.QueryAsync<VMInfoDto>(sql.ToString(), parameters);
            }
        }
    }
}
