using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Repository.Repositories
{
    public class BaseRepository
    {
        protected string HealthConnectionString
        {
            get
            {
                if ((ConfigurationManager.ConnectionStrings["HEALTHDB"] != null) &&
                    !string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings["HEALTHDB"].ConnectionString))
                    return ConfigurationManager.ConnectionStrings["HEALTHDB"].ConnectionString;
                else
                    return "Data Source=10.1.2.165;initial Catalog=HEALTH_DB;Persist Security Info=False;Uid=empuser;Password=abcd1234;providerName=System.Data.SqlClient";
            }
        }

        protected string Emic2ConnectionString
        {
            get
            {
                if ((ConfigurationManager.ConnectionStrings["EMIC2DB"] != null) &&
                    !string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings["EMIC2DB"].ConnectionString))
                    return ConfigurationManager.ConnectionStrings["EMIC2DB"].ConnectionString;
                else
                    return "Data Source=10.1.2.165;initial Catalog=EMIC2_Public;Persist Security Info=False;Uid=empuser;Password=abcd1234;providerName=System.Data.SqlClient";
            }
        }
    }
}
