using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

using NLog;

namespace FSP2.API.Controllers
{
    public class ConvertController : BaseController
    {
        public ConvertController()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        [Route("ODT")]
        public HttpResponseMessage ConvertToOdt(HttpRequestMessage httpRequestMessage)
        {
            HttpResponseMessage httpResponseMessage = Request.CreateResponse();

            try
            {
                byte[] content = httpRequestMessage.Content.ReadAsByteArrayAsync().Result;
                byte[] newContent = null;

                using (MemoryStream inStream = new MemoryStream(content))
                using (MemoryStream outStream = (MemoryStream)ConvertTo(inStream))
                {
                    newContent = outStream.ToArray();
                }

                httpResponseMessage.StatusCode = HttpStatusCode.OK;
                httpResponseMessage.Content = new ByteArrayContent(newContent);
            }
            catch (Exception ex)
            {
                httpResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
                httpResponseMessage.Content = new StringContent(LogException(ex).Message, Encoding.UTF8);
            }

            return httpResponseMessage;
        }
    }
}
