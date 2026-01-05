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
    public class MspLicenseRepository : EmicBaseRepository, IMspLicenseRepository
    {
        public SYS_MSP_LICENSE SearchByOid(string oid)
        {
            string sql = "SELECT OID,UNIT_NAME,LICENSE,STATUS " +
                         "FROM SYS_MSP_LICENSE " +
                         "WHERE OID = @OID ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@OID", oid);

                SYS_MSP_LICENSE result = connection.Query<SYS_MSP_LICENSE>(sql, parameters).FirstOrDefault();
                return result;
            }
        }
    }
}
