using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using NLog;
using Health.Service.Interfaces;

namespace Health.API.Controllers
{
    public class UserTokenController : BaseController
    {
        private readonly IHealthService _HealthService;

        public UserTokenController(IHealthService healthService)
        {
            _HealthService = healthService;
            Logger = LogManager.GetCurrentClassLogger();
        }

        [Route("UserToken/GetCurrentUsers")]
        public async Task<IHttpActionResult> GetCurrentUsers()
        {
            try
            {
                return Ok(await _HealthService.GetCurrentUsersAsync());
            }
            catch (Exception ex)
            {
                return InternalServerError(LogException(ex));
            }
        }
    }
}
