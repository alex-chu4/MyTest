using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using NLog;
using Newtonsoft.Json;
using Health.Repository.Dto;
using Health.Web.Models;

namespace Health.Web.ApiControllers
{
    public class HealthController : BaseApiController
    {
        public HealthController()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }

        [Route("HealthCurrent/GetCurrentView")]
        public async Task<IHttpActionResult> GetCurrentView()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.GetAsync(WebApiUrls.HealthCurrentGetCurrentView))
                    {
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            object result = JsonConvert.DeserializeObject(await responseMessage.Content.ReadAsStringAsync());
                            return Ok(result);
                        }
                        else
                        {
                            Logger.Info(await responseMessage.Content.ReadAsStringAsync());
                            return StatusCode(responseMessage.StatusCode);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(LogException(ex));
            }
        }

        [HttpPost]
        [Route("HealthHis/Search")]
        public async Task<IHttpActionResult> SearchHis(HealthHisSearchFilter filter)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.HealthHisSearch, filter))
                    {
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            object result = JsonConvert.DeserializeObject(await responseMessage.Content.ReadAsStringAsync());
                            return Ok(result);
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

        [HttpPost]
        [Route("HealthHis2/Search")]
        public async Task<IHttpActionResult> SearchHis2(HealthHis2SearchFilter filter)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.HealthHis2Search, filter))
                    {
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            object result = JsonConvert.DeserializeObject(await responseMessage.Content.ReadAsStringAsync());
                            return Ok(result);
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
