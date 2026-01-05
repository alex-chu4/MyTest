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
    public class NotificationInfoController : BaseApiController
    {
        public NotificationInfoController()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        [Route("NotificationInfo/Create")]
        public async Task<IHttpActionResult> Create(NotificationInfoDto notificationInfo)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.NotificationInfoCreate, notificationInfo))
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
        [Route("NotificationInfo/Delete")]
        public async Task<IHttpActionResult> Delete(NotificationInfoDto notificationInfo)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.NotificationInfoDelete, notificationInfo))
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
        [Route("NotificationInfo/Update")]
        public async Task<IHttpActionResult> Update(NotificationInfoDto notificationInfo)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.NotificationInfoUpdate, notificationInfo))
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
        [Route("NotificationInfo/Search")]
        public async Task<IHttpActionResult> Search(NotificationInfoSearchFilter filter)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.NotificationInfoSearch, filter))
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
