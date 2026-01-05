using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSP.Repository.Repositories
{
    public class BaseRepository
    {
        public string GetConnectionString(string connectionName)
        {
            if ((ConfigurationManager.ConnectionStrings[connectionName] != null) &&
                !string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString))
                return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            else
                return string.Empty;
        }
    }
}
