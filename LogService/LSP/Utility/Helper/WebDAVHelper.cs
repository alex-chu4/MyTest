using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Helper
{
    public class WebDAVHelper
    {
        public static string WebDAVPath
        {
            get
            {
                return ConfigurationManager.AppSettings["WebDAVPath"];
            }
        }

        public static string WebDAVAccount
        {
            get
            {
                return ConfigurationManager.AppSettings["WebDAVAccount"];
            }
        }

        public static string WebDAVSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["WebDAVSecret"];
            }
        }
    }
}
