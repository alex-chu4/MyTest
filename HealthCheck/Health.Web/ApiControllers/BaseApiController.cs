using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using NLog;

namespace Health.Web.ApiControllers
{
    public class BaseApiController : ApiController
    {
        protected ILogger Logger { get; set; }

        protected Exception LogException(Exception ex)
        {
            Logger.Error(ex);
            return ex.InnerException == null ? ex : ex.InnerException;
        }
    }
}
