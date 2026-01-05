using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using NLog;
using Spire.Doc;

namespace FSP2.API.Controllers
{
    public class BaseController : ApiController
    {
        protected ILogger Logger { get; set; }

        protected Exception LogException(Exception ex)
        {
            Logger.Error(ex);
            return ex.InnerException == null ? ex : ex.InnerException;
        }

        protected Stream ConvertTo(Stream inStream, FileFormat format = FileFormat.Odt)
        {
            MemoryStream outStream = new MemoryStream();
            using (Document document = new Document())
            {
                document.LoadFromStream(inStream, FileFormat.Auto);
                document.SaveToStream(outStream, format);
            }

            return outStream;
        }
    }
}
