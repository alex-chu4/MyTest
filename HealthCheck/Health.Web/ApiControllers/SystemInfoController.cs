using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using Newtonsoft.Json;
using NLog;
using Health.API.Models;
using Health.Repository.Dto;
using Health.Web.Models;

namespace Health.Web.ApiControllers
{
    public class SystemInfoController : BaseApiController
    {
        public SystemInfoController()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        [Route("SystemInfo/UpdateThreshold")]
        public async Task<IHttpActionResult> UpdateThreshold(HealthHisModel healthHisModel)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.SystemInfoUpdateThreshold, healthHisModel))
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
        [Route("SystemInfo/Create")]
        public async Task<IHttpActionResult> Create(SystemInfoDto systemInfo)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.SystemInfoCreate, systemInfo))
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
        [Route("SystemInfo/Delete")]
        public async Task<IHttpActionResult> Delete(SystemInfoDto systemInfo)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.SystemInfoDelete, systemInfo))
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
        [Route("SystemInfo/Update")]
        public async Task<IHttpActionResult> Update(SystemInfoDto systemInfo)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.SystemInfoUpdate, systemInfo))
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
        [Route("SystemInfo/Search")]
        public async Task<IHttpActionResult> Search(SystemInfoSearchFilter filter)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.SystemInfoSearch, filter))
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
