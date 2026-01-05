using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using MSP.Repository.Interfaces.Emic;
using MSP.Repository.Models.Emic;

namespace MSP.Repository.Repositories.Emic
{
    public class CommonParameterRepository : EmicBaseRepository, ICommonParameterRepository
    {
        public SYS_COM_PARAM SearchByParamName(string paramName)
        {
            string sql = "SELECT PARAM_NAME,PARAM_VALUE,PARAM_DESC " +
                         "FROM SYS_COM_PARAM " +
                         "WHERE PARAM_NAME = @PARAM_NAME ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PARAM_NAME", paramName);

                SYS_COM_PARAM result = connection.Query<SYS_COM_PARAM>(sql, parameters).FirstOrDefault();
                return result;
            }
        }
    }
}
