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
    public class VMInfoController : BaseController
    {
        private readonly IHealthService _HealthService;

        public VMInfoController(IHealthService healthService)
        {
            _HealthService = healthService;
            Logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        [Route("VMInfo/Create")]
        public async Task<IHttpActionResult> Create(VMInfoDto vmInfo)
        {
            try
            {
                await _HealthService.CreateVMInfoAsync(vmInfo);
                return Ok();
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
                await _HealthService.DeleteVMInfoAsync(vmInfo);
                return Ok();
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
                await _HealthService.UpdateVMInfoAsync(vmInfo);
                return Ok();
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
                return Ok((await _HealthService.SearchVMInfoAsync(filter)).OrderBy(p => p.ID).ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError(LogException(ex));
            }
        }
    }
}
