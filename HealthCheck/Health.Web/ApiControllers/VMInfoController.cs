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
    public class VMInfoController : BaseApiController
    {
        public VMInfoController()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        [Route("VMInfo/Create")]
        public async Task<IHttpActionResult> Create(VMInfoDto vmInfo)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.VMInfoCreate, vmInfo))
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
        [Route("VMInfo/Delete")]
        public async Task<IHttpActionResult> Delete(VMInfoDto vmInfo)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.VMInfoDelete, vmInfo))
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
        [Route("VMInfo/Update")]
        public async Task<IHttpActionResult> Update(VMInfoDto vmInfo)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.VMInfoUpdate, vmInfo))
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
        [Route("VMInfo/Search")]
        public async Task<IHttpActionResult> Search(VMInfoSearchFilter filter)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    using (HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(WebApiUrls.VMInfoSearch, filter))
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
