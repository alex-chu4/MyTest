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
    public class HealthTargetController : BaseController
    {
        private readonly IHealthService _HealthService;

        public HealthTargetController(IHealthService healthService)
        {
            _HealthService = healthService;
            Logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        [Route("HealthTarget/Create")]
        public async Task<IHttpActionResult> Create(HealthTargetDto healthTarget)
        {
            try
            {
                await _HealthService.CreateHealthTargetAsync(healthTarget);
                return Ok();
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
                await _HealthService.DeleteHealthTargetAsync(healthTarget);
                return Ok();
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
                await _HealthService.UpdateHealthTargetAsync(healthTarget);
                return Ok();
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
                return Ok((await _HealthService.SearchHealthTargetAsync(filter)).OrderBy(p => p.SYSTEM_ID).ThenBy(p => p.VM_ID).ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError(LogException(ex));
            }
        }
    }
}
