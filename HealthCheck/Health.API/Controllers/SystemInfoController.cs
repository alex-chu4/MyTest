using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using NLog;
using Health.API.Models;
using Health.Repository.Dto;
using Health.Service.Interfaces;

namespace Health.API.Controllers
{
    public class SystemInfoController : BaseController
    {
        private readonly IHealthService _HealthService;

        public SystemInfoController(IHealthService healthService)
        {
            _HealthService = healthService;
            Logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        [Route("SystemInfo/Create")]
        public async Task<IHttpActionResult> Create(SystemInfoDto systemInfo)
        {
            try
            {
                await _HealthService.CreateSystemInfoAsync(systemInfo);
                return Ok();
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
                await _HealthService.DeleteSystemInfoAsync(systemInfo);
                return Ok();
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
                await _HealthService.UpdateSystemInfoAsync(systemInfo);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(LogException(ex));
            }
        }

        [HttpPost]
        [Route("SystemInfo/UpdateThreshold")]
        public async Task<IHttpActionResult> UpdateThreshold(HealthHisModel healthHisModel)
        {
            try
            {
                await _HealthService.UpdateThresholdAsync(healthHisModel.SYSTEM_ID, healthHisModel.THRESHOLD ?? DefaultThreshold);
                return Ok();
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
                IEnumerable<SystemInfoDto> systemInfoDtos = (await _HealthService.SearchSystemInfoAsync(filter)).OrderBy(p => p.ID).ToList();
                return Ok(systemInfoDtos);
            }
            catch (Exception ex)
            {
                return InternalServerError(LogException(ex));
            }
        }
    }
}
