using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Health.Checker.Checkers.NetworkChecker
{
    public class HttpChecker : BaseNetworkChecker<HttpResponseMessage>
    {
        protected override async Task<HttpResponseMessage> CheckFuncAsync(string requestUri, int timeout)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    if (timeout > 0)
                    {
                        httpClient.Timeout = TimeSpan.FromMilliseconds(timeout);
                    }

                    return await httpClient.GetAsync(requestUri);
                }
            }
            catch
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable);
            }
        }

        protected override HttpResponseMessage CheckFunc(string requestUri, int timeout)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    if (timeout > 0)
                    {
                        httpClient.Timeout = TimeSpan.FromMilliseconds(timeout);
                    }

                    return httpClient.GetAsync(requestUri).Result;
                }
            }
            catch
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable);
            }
        }
    }
}
