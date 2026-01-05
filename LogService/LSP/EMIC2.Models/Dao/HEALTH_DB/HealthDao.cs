using Dapper;
using EMIC2.Models.Dao.Dto.HEALTH_DB;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.HEALTH_DB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.HEALTH_DB
{
    public class HealthDao : IHealthDao
    {
        public void Insert(HEALTH_HIS2Dto model)
        {
            //Func<string> GetLocalPv4 = () => {
            //    string _rtn = string.Empty;
            //    try
            //    {
            //        IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            //        _rtn = hostEntry.AddressList
            //        .Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            //        .Select(y => y.ToString())
            //        .FirstOrDefault();
            //    }
            //    catch
            //    {
            //    }
            //    return _rtn;
            //};

            //model.IPv4 = GetLocalPv4();
            model.IPv4 = GetLocalIPv4(NetworkInterfaceType.Ethernet);
            //string aa = GetLocalIPv4(NetworkInterfaceType.Wireless80211);

            var ConnString = System.Configuration.ConfigurationManager.ConnectionStrings["HEALTH_DB"].ToString();
            ConnString = Helper.CryptographyEx.DecryptString(ConnString, "EMIC_SSO2");

            ConnectionHelper.Connect<int>(ConnString,
                c => c.Execute(@"INSERT HEALTH_HIS2(SYSTEM_ID, FUNCTION_CODE, IPv4, TOTAL_TIME, SYSTEM_TIME, DB_TIME, CREATE_TIME) 
            VALUES(@SYSTEM_ID, @FUNCTION_CODE, @IPv4, @TOTAL_TIME, @SYSTEM_TIME, @DB_TIME, @CREATE_TIME)", model));
        }

        private string GetLocalIPv4(NetworkInterfaceType _type)
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
    }
}
