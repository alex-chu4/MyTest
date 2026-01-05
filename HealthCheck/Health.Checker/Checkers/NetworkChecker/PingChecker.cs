using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Health.Checker.Checkers.NetworkChecker
{
    public class PingChecker : BaseNetworkChecker<PingReply>
    {
        protected override async Task<PingReply> CheckFuncAsync(string requestUri, int timeout)
        {
            using (Ping ping = new Ping())
            {
                return await ping.SendPingAsync(requestUri, timeout);
            }
        }

        protected override PingReply CheckFunc(string requestUri, int timeout)
        {
            using (Ping ping = new Ping())
            {
                return ping.SendPingAsync(requestUri, timeout).Result;
            }
        }
    }
}
