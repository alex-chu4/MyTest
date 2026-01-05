using EMIC2.Models.Interface;
using EMIC2.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace Utility.Helper
{
    public class DBHelper
    {
        private static IUnitOfWork unitOfWork = new UnitOfWork();

        public static string GetEMIC2DBConnection()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["EMIC2DB"].ToString();
        }

        public static List<string> GetWhiteList()
        {
            List<string> rtn = unitOfWork.Context.Database.SqlQuery<string>("select ap_url from sso2_ap_menu where 1=1").ToList();
            return rtn;
        }

        public static string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }

            return output;
        }

        public static string GetRemoteIP()
        {
            try
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    // using proxy
                    return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); // Return real client IP.
                }
                else
                {
                    // not using proxy or can’t get the Client IP
                    return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can’t get the Client IP, it will return proxy IP.
                }
            }
            catch (Exception) { }

            return "LocalHost";
        }
    }
}
