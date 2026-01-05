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
    public class SystemInfoRepository : BaseRepository, ISystemInfoRepository
    {
        public async Task<IEnumerable<SystemInfoDto>> Search(SystemInfoSearchFilter filter)
        {
            DynamicParameters parameters = new DynamicParameters();
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT OID,ID,NAME,[DESC],ISNULL(THRESHOLD,5.0) AS THRESHOLD,ISNULL(KEY_FUNCTION,CONVERT(BIT,0)) AS KEY_FUNCTION,CREATE_TIME,UPDATE_TIME " +
                "FROM SYSTEM_INFO " +
                "WHERE 1=1 ");
            if (!string.IsNullOrEmpty(filter.ID))
            {
                sql.Append("AND ID=@ID ");
                parameters.Add("@ID", filter.ID);
            }
            else
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.Append("AND NAME LIKE '%' + @NAME + '%' ");
                    parameters.Add("@NAME", filter.Name);
                }

                if ((filter.IDs != null) && (filter.IDs.Count() > 0))
                {
                    sql.AppendFormat("AND ID IN ('{0}') ", string.Join("','", filter.IDs));
                }

                if (filter.KEY_FUNCTION.HasValue)
                {
                    sql.Append("AND KEY_FUNCTION=@KEY_FUNCTION ");
                    parameters.Add("@KEY_FUNCTION", filter.KEY_FUNCTION.Value);
                }
            }

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                IEnumerable<SystemInfoDto> result = await connection.QueryAsync<SystemInfoDto>(sql.ToString(), parameters);
                return result;
            }
        }

        public async Task<int> Create(SystemInfoDto systemInfo)
        {
            string sql = "INSERT INTO SYSTEM_INFO(ID,NAME,[DESC],THRESHOLD,KEY_FUNCTION,CREATE_TIME) " +
                         "VALUES(@ID,@NAME,@DESC,@THRESHOLD,@KEY_FUNCTION,GETDATE())";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", systemInfo.ID);
            parameters.Add("@NAME", systemInfo.NAME);
            parameters.Add("@DESC", systemInfo.DESC);
            parameters.Add("@THRESHOLD", systemInfo.THRESHOLD);
            parameters.Add("@KEY_FUNCTION", systemInfo.KEY_FUNCTION);

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<int> Delete(SystemInfoDto systemInfo)
        {
            string sql = "DELETE SYSTEM_INFO WHERE OID=@OID ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OID", systemInfo.OID);

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<int> Update(SystemInfoDto systemInfo)
        {
            string sql = "UPDATE SYSTEM_INFO SET " +
                         "ID=@ID," +
                         "NAME=@NAME," +
                         "[DESC]=@DESC," +
                         "THRESHOLD=@THRESHOLD," +
                         "KEY_FUNCTION=@KEY_FUNCTION," +
                         "UPDATE_TIME=GETDATE() " +
                         "WHERE OID=@OID ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", systemInfo.ID);
            parameters.Add("@NAME", systemInfo.NAME);
            parameters.Add("@DESC", systemInfo.DESC);
            parameters.Add("@THRESHOLD", systemInfo.THRESHOLD);
            parameters.Add("@KEY_FUNCTION", systemInfo.KEY_FUNCTION);
            parameters.Add("@OID", systemInfo.OID);

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<int> UpdateThreshold(string SYSTEM_ID, float THRESHOLD)
        {
            string sql = "UPDATE SYSTEM_INFO SET THRESHOLD=@THRESHOLD WHERE ID=@ID";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@THRESHOLD", THRESHOLD);
            parameters.Add("@ID", SYSTEM_ID);

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
