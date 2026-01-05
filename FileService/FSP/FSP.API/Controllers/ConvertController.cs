using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;

using NLog;
using Spire.Xls;

namespace FSP.API.Controllers
{
    public class ConvertController : BaseController
    {
        public ConvertController()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        [Route("ODS")]
        public HttpResponseMessage ConvertToOds(HttpRequestMessage httpRequestMessage)
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

        [HttpPost]
        [Route("PDF")]
        public HttpResponseMessage ConvertToPdf(HttpRequestMessage httpRequestMessage)
        {
            HttpResponseMessage httpResponseMessage = Request.CreateResponse();

            try
            {
                byte[] content = httpRequestMessage.Content.ReadAsByteArrayAsync().Result;
                byte[] newContent = null;

                using (MemoryStream inStream = new MemoryStream(content))
                using (MemoryStream outStream = (MemoryStream)ConvertTo(inStream, FileFormat.PDF))
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

        [HttpPost]
        [Route("ODT")]
        public HttpResponseMessage ConvertToOdt(HttpRequestMessage httpRequestMessage)
        {
            HttpResponseMessage httpResponseMessage = Request.CreateResponse();

            try
            {
                byte[] content = httpRequestMessage.Content.ReadAsByteArrayAsync().Result;

                HttpWebRequest webRequest = WebRequest.Create(ConfigurationManager.AppSettings["OdtUrl"]) as HttpWebRequest;
                webRequest.Method = "post";
                webRequest.ContentType = "application/octet-stream";
                webRequest.GetRequestStream().Write(content, 0, content.Length);

                HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
                httpResponseMessage.StatusCode = webResponse.StatusCode;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    webResponse.GetResponseStream().CopyTo(memoryStream);
                    byte[] responseBytes = memoryStream.ToArray();

                    httpResponseMessage.Content = new ByteArrayContent(responseBytes);
                }
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
