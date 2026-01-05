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
    public class FunctionRepository : BaseRepository, IFunctionRepository
    {
        public async Task<IEnumerable<FunctionDto>> Search(FunctionSearchFilter filter)
        {
            DynamicParameters parameters = new DynamicParameters();
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT FUNCTION_CODE,FUNCTION_NAME,SYS_CODE " +
                "FROM SSO2_FUNCTION " +
                "WHERE URL <> '' " +
                "AND IS_ON_OFF = 'Y' ");

            if (!string.IsNullOrEmpty(filter.SYS_CODE))
            {
                sql.Append("AND SYS_CODE=@SYS_CODE ");
                parameters.Add("@SYS_CODE", filter.SYS_CODE);
            }

            if (!string.IsNullOrEmpty(filter.FUNCTION_CODE))
            {
                sql.Append("AND FUNCTION_CODE=@FUNCTION_CODE ");
                parameters.Add("@FUNCTION_CODE", filter.FUNCTION_CODE);
            }

            if ((filter.FUNCTION_CODES != null) && (filter.FUNCTION_CODES.Length>0))
            {
                sql.AppendFormat("AND FUNCTION_CODE IN ('{0}') ", string.Join("','", filter.FUNCTION_CODES));
            }

            sql.Append("ORDER BY SYS_CODE,FUNCTION_CODE ");

            using (SqlConnection connection = new SqlConnection(Emic2ConnectionString))
            {
                IEnumerable<FunctionDto> result = await connection.QueryAsync<FunctionDto>(sql.ToString());
                return result;
            }
        }
    }
}
