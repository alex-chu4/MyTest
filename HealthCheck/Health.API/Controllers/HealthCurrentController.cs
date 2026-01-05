using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

using NLog;
using Health.Repository.Dto;
using Health.Service.Interfaces;

namespace Health.API.Controllers
{
    public class HealthCurrentController : BaseController
    {
        private readonly IHealthService _HealthService;

        public HealthCurrentController(IHealthService healthService)
        {
            _HealthService = healthService;
            Logger = LogManager.GetCurrentClassLogger();
        }

        [Route("HealthCurrent/GetCurrentView")]
        public async Task<IHttpActionResult> GetCurrentView()
        {
            try
            {
                IEnumerable<HealthCurrentViewDto> healthCurrentViews = await _HealthService.GetCurrentViewAsync();
                string[] systems = (from h in healthCurrentViews
                                    select h.SYSTEM_ID).Distinct().ToArray();
                IEnumerable<SystemInfoDto> systemInfos = (await _HealthService.SearchSystemInfoAsync(new SystemInfoSearchFilter { IDs = systems })).OrderBy(p => p.ID);
                IEnumerable<EventDto> events = await _HealthService.GetHealthHis2OverThresholdAsync(EventTimeInterval, DefaultThreshold);

                return Ok(new Tuple<IEnumerable<SystemInfoDto>, IEnumerable<HealthCurrentViewDto>, IEnumerable<EventDto>>(systemInfos, healthCurrentViews, events));
            }
            catch (Exception ex)
            {
                return InternalServerError(LogException(ex));
            }
        }
    }
}
