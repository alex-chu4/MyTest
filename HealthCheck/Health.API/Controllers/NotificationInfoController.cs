using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using NLog;
using Health.Repository.Dto;
using Health.Service.Interfaces;

namespace Health.API.Controllers
{
    public class NotificationInfoController : BaseController
    {
        private readonly IHealthService _HealthService;

        public NotificationInfoController(IHealthService healthService)
        {
            _HealthService = healthService;
            Logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        [Route("NotificationInfo/Create")]
        public async Task<IHttpActionResult> Create(NotificationInfoDto notificationInfo)
        {
            try
            {
                await _HealthService.CreateNotificationInfoAsync(notificationInfo);
                return Ok();
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
                await _HealthService.DeleteNotificationInfoAsync(notificationInfo);
                return Ok();
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
                await _HealthService.UpdateNotificationInfoAsync(notificationInfo);
                return Ok();
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
                return Ok(await _HealthService.SearchNotificationInfoAsync(filter));
            }
            catch (Exception ex)
            {
                return InternalServerError(LogException(ex));
            }
        }
    }
}
