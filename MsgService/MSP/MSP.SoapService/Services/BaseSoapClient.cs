using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using MSP.SoapService.Interfaces;
using MSP.SoapService.Models;

namespace MSP.SoapService.Services
{
    public class BaseSoapClient : ISoapClient
    {
        public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        public IList<ContentPart> Parts { get; } = new List<ContentPart>();

        public virtual string Boundary { get; set; }

        public virtual string StartContentID { get; set; }

        public virtual string Send(string url)
        {
#if false
            StringBuilder content = new StringBuilder();
            string contentID = StartContentID;

            if (Parts.Count == 1)
            {
                // 只有一筆ContentPart，就不使用MIME的方式
                // 處理Header
                foreach (string key in Parts.ElementAt(0).Headers.Keys)
                {
                    if (!Headers.Keys.Contains(key))
                    {
                        Headers[key] = Parts.ElementAt(0).Headers[key];
                    }
                }

                // 處理Content
                content.Append(Parts.ElementAt(0).Content);
            }
            else if (Parts.Count > 1)
            {
                // 處理Header的MIME
                if (Parts.ElementAt(0).Headers.Keys.Contains("content-id"))
                    contentID = Parts.ElementAt(0).Headers["content-id"];
                if (!Headers.Keys.Contains("content-type"))
                    Headers["content-type"] = string.Format("multipart/related; type=\"text/xml\"; start=\"{0}\"; boundary=\"{1}\" ", contentID, Boundary);
                foreach (ContentPart part in Parts)
                {
                    // 加Boundary
                    content.AppendFormat("--{0}",Boundary);
                    content.AppendLine();

                    // 加Header
                    foreach (string key in part.Headers.Keys)
                    {
                        content.AppendFormat("{0}: {1}", key, part.Headers[key]);
                        content.AppendLine();
                    }
                    content.AppendLine();

                    // 加Content
                    content.Append(part.Content);
                    content.AppendLine();
                    content.AppendLine();
                }

                // 加Boundary結尾
                content.AppendFormat("--{0}--", Boundary);
            }

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            foreach (string key in Headers.Keys)
            {
                switch (key)
                {
                    case "content-type":
                        request.ContentType = Headers[key];
                        break;
                    case "keep-alive":
                        request.KeepAlive = bool.Parse(Headers[key]);
                        break;
                    default:
                        request.Headers.Add(key, Headers[key]);
                        break;
                }
            }

            Stream stream = request.GetRequestStream();
            byte[] bytes = UTF8Encoding.UTF8.GetBytes(content.ToString());
            stream.Write(bytes, 0, bytes.Length);
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadLine();
            }
#else
            return SendAsync(url).Result;
#endif
        }

        public async virtual Task<string> SendAsync(string url)
        {
            StringBuilder content = new StringBuilder();
            string contentID = StartContentID;

            if (Parts.Count == 1)
            {
                // 只有一筆ContentPart，就不使用MIME的方式
                // 處理Header
                foreach (string key in Parts.ElementAt(0).Headers.Keys)
                {
                    if (!Headers.Keys.Contains(key))
                    {
                        Headers[key] = Parts.ElementAt(0).Headers[key];
                    }
                }

                // 處理Content
                content.Append(Parts.ElementAt(0).Content);
            }
            else if (Parts.Count > 1)
            {
                // 處理Header的MIME
                if (Parts.ElementAt(0).Headers.Keys.Contains("content-id"))
                    contentID = Parts.ElementAt(0).Headers["content-id"];
                if (!Headers.Keys.Contains("content-type"))
                    Headers["content-type"] = string.Format("multipart/related; type=\"text/xml\"; start=\"{0}\"; boundary=\"{1}\" ", contentID, Boundary);
                foreach (ContentPart part in Parts)
                {
                    // 加Boundary
                    content.AppendFormat("--{0}", Boundary);
                    content.AppendLine();

                    // 加Header
                    foreach (string key in part.Headers.Keys)
                    {
                        content.AppendFormat("{0}: {1}", key, part.Headers[key]);
                        content.AppendLine();
                    }
                    content.AppendLine();

                    // 加Content
                    content.Append(part.Content);
                    content.AppendLine();
                    content.AppendLine();
                }

                // 加Boundary結尾
                content.AppendFormat("--{0}--", Boundary);
            }

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            foreach (string key in Headers.Keys)
            {
                switch (key)
                {
                    case "content-type":
                        request.ContentType = Headers[key];
                        break;
                    case "keep-alive":
                        request.KeepAlive = bool.Parse(Headers[key]);
                        break;
                    default:
                        request.Headers.Add(key, Headers[key]);
                        break;
                }
            }

            Stream stream = request.GetRequestStream();
            byte[] bytes = UTF8Encoding.UTF8.GetBytes(content.ToString());
            stream.Write(bytes, 0, bytes.Length);
            HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadLine();
            }
        }
    }
}
