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
using Health.API.Models;

namespace Health.API.Controllers
{
    public class HealthHisController : BaseController
    {
        private readonly IHealthService _HealthService;

        public HealthHisController(IHealthService healthService)
        {
            _HealthService = healthService;
            Logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        [Route("HealthHis/Search")]
        public async Task<IHttpActionResult> Search(HealthHisSearchFilter filter)
        {
            try
            {
                IEnumerable<HealthHisDto> result = await _HealthService.SearchHealthHisAsync(filter);
                return Ok(result.OrderByDescending(h => h.CREATE_TIME));
            }
            catch(Exception ex)
            {
                return InternalServerError(LogException(ex));
            }
        }
    }
}
