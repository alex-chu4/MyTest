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
    public class HealthHis2Controller : BaseController
    {
        private readonly IHealthService _HealthService;

        public HealthHis2Controller(IHealthService healthService)
        {
            _HealthService = healthService;
            Logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        [Route("HealthHis2/Search")]
        public async Task<IHttpActionResult> Search(HealthHis2SearchFilter filter)
        {
            try
            {
                IEnumerable<HealthHis2Dto> healthHis2 = await _HealthService.SearchHealthHis2Async(filter);
                string[] functionCodes = healthHis2.Select(p => p.FUNCTION_CODE).Distinct().ToArray();
                IEnumerable<FunctionDto> functions;

                if (functionCodes.Length > 0)
                {
                    functions = await _HealthService.SearchFunctionAsync(new FunctionSearchFilter { FUNCTION_CODES = functionCodes });
                }
                else
                {
                    functions = new List<FunctionDto>();
                }

                return Ok(new Tuple<IEnumerable<FunctionDto>, IEnumerable<HealthHis2Dto>>(functions, healthHis2.OrderByDescending(h => h.CREATE_TIME)));
            }
            catch (Exception ex)
            {
                return InternalServerError(LogException(ex));
            }
        }
    }
}
