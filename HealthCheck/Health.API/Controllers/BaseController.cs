using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using NLog;

namespace Health.API.Controllers
{
    public class BaseController : ApiController
    {
        protected ILogger Logger { get; set; }

        protected float DefaultThreshold
        {
            get
            {
                float threshold;

                if (!float.TryParse(ConfigurationManager.AppSettings["DefaultThreshold"], out threshold))
                {
                    threshold = 5.0f;
                }

                return threshold;
            }
        }

        protected int EventTimeInterval
        {
            get
            {
                int interval;

                if (!int.TryParse(ConfigurationManager.AppSettings["EventTimeInterval"], out interval))
                {
                    interval = 60;
                }

                return interval;
            }
        }

        protected Exception LogException(Exception ex)
        {
            Logger.Error(ex);
            return ex.InnerException == null ? ex : ex.InnerException;
        }
    }
}
