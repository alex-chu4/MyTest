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
    public class HealthTargetController : BaseApiController
    {
        public HealthTargetController()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        [Route("HealthTarget/Create")]
        public async Task<IHttpActionResult> Create(HealthTargetDto healthTarget)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.HealthTargetCreate, healthTarget))
                    {
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            return Ok("儲存成功");
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
        [Route("HealthTarget/Delete")]
        public async Task<IHttpActionResult> Delete(HealthTargetDto healthTarget)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.HealthTargetDelete, healthTarget))
                    {
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            return Ok("刪除成功");
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
        [Route("HealthTarget/Update")]
        public async Task<IHttpActionResult> Update(HealthTargetDto healthTarget)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.HealthTargetUpdate, healthTarget))
                    {
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            return Ok("異動成功");
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
        [Route("HealthTarget/Search")]
        public async Task<IHttpActionResult> Search(HealthTargetSearchFilter filter)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.HealthTargetSearch, filter))
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
