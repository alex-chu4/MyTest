using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using NLog;
using Spire.Xls;

namespace FSP.API.Controllers
{
    public class BaseController : ApiController
    {
        protected ILogger Logger { get; set; }

        protected Exception LogException(Exception ex)
        {
            Logger.Error(ex);
            return ex.InnerException == null ? ex : ex.InnerException;
        }

        protected Stream ConvertTo(Stream inStream, FileFormat format = FileFormat.ODS)
        {
            MemoryStream outStream = new MemoryStream();
            using (Workbook workbook = new Workbook())
            {
                workbook.LoadFromStream(inStream);
                workbook.SaveToStream(outStream, format);
            }

            return outStream;
        }
    }
}
