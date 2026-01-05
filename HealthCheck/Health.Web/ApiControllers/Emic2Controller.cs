using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using NLog;
using Health.Web.Models;

namespace Health.Web.ApiControllers
{
    public class Emic2Controller : BaseApiController
    {
        public Emic2Controller()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }

        [Route("UserToken/GetCurrentUsers")]
        public async Task<IHttpActionResult> GetCurrentView()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.GetAsync(WebApiUrls.UserTokenGetCurrentUsers))
                    {
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            string result = await responseMessage.Content.ReadAsStringAsync();
                            return Ok(int.Parse(result));
                        }
                        else
                        {
                            Logger.Info(await responseMessage.Content.ReadAsStringAsync());
                            return StatusCode(responseMessage.StatusCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(LogException(ex));
            }
        }
    }
}
