using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using MSP.SoapService.Interfaces;
using MSP.SoapService.Models;
using MSP.SoapService.Services.Emic;
using MSP.Repository.Interfaces.Emic;
using MSP.Repository.Models.Emic;
using MSP.Service.Models;

namespace MSP.Service.Services.Emic
{
    public abstract class BaseService
    {
        protected readonly ICommonParameterRepository commonParameterRepository;
        protected readonly IMspLicenseRepository mspLicenseRepository;

        public BaseService(ICommonParameterRepository commonParameterRepository, IMspLicenseRepository mspLicenseRepository)
        {
            this.commonParameterRepository = commonParameterRepository;
            this.mspLicenseRepository = mspLicenseRepository;
        }

        protected string DevelopSite
        {
            get
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DevelopSite"]))
                    return ConfigurationManager.AppSettings["DevelopSite"];
                else
                    return "/EMP2_Develop/";
            }
        }

        protected string ProductionSite
        {
            get
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ProductionSite"]))
                    return ConfigurationManager.AppSettings["ProductionSite"];
                else
                    return "/EMP2/";
            }
        }

        protected ResultModel ErrorResultModel
        {
            get
            {
                return CreateErrorResultModel("E100", "Unauthorized(註冊碼錯誤或過期)");
            }
        }

        protected ResultModel MspErrorResultModel
        {
            get
            {
                return CreateErrorResultModel("E101", "訊息服務平台API無法連線");
            }
        }

        protected ResultModel InvalidUserTypeResultModel
        {
            get
            {
                return CreateErrorResultModel("E102", "使用機關帳號登入者，無法發送訊息");
            }
        }

        protected ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> toList, string fileBase, string fileNameExtension)
        {
#if false
            //一開始檢查授權,有問題就擋掉
            //開始使用訊息服務平台共用Service
            SYS_MSP_LICENSE sysMspLicense = mspLicenseRepository.SearchByOid(oid);

            if (sysMspLicense == null)
            {
                return ErrorResultModel;
            }
            else
            {
                //url直接抓SYS_COM_PARAM
                string mailContextRoot = commonParameterRepository.SearchByParamName(SysComParamConstant.MAIL_URL).PARAM_VALUE;
                string mspmsgContextRoot = commonParameterRepository.SearchByParamName(SysComParamConstant.MSPMSG).PARAM_VALUE;
                string mspWsdlContextRoot = commonParameterRepository.SearchByParamName(SysComParamConstant.MSP_WSDL).PARAM_VALUE;

                //檢查msp訊息是否正確
                if (!CheckMspWsdl(mailContextRoot + mspWsdlContextRoot))
                {
                    return MspErrorResultModel;
                }
                else
                {
                    string xml = ComposeMessageXml(subject, content, toList, oid);

                    IDictionary<string, string> pairs = new Dictionary<string, string>
                    {
                        {"Authorization", sysMspLicense.LICENSE },
                        {"Message", xml }
                    };

                    ISoapMessage soapMessage = new EmicSoapMessage();
                    string soapContent = soapMessage.Encode(pairs);

                    ISoapClient soapClient = new EmicSoapClient();
                    ContentPart part = new ContentPart();
                    part.Headers["content-type"] = "text/xml; charset=utf-8;";
                    part.Headers["content-transfer-encoding"] = "binary";
                    part.Headers["content-id"] = soapClient.StartContentID;
                    part.Content = soapContent;
                    soapClient.Parts.Add(part);

                    if (!string.IsNullOrEmpty(fileBase))
                    {
                        part = new ContentPart();
                        part.Headers["content-type"] = GetMimeType(fileNameExtension);
                        part.Headers["content-transfer-encoding"] = "base64";
                        part.Headers["content-id"] = string.Format("<{0}>", Guid.NewGuid().ToString());
                        part.Content = fileBase;
                        soapClient.Parts.Add(part);
                    }

                    pairs = soapMessage.Decode(soapClient.Send(mailContextRoot + mspmsgContextRoot));
                    string responseXml = pairs["return"];
                    ResultModel resultModel = XmlToResultModel(responseXml);

                    return resultModel;
                }
            }
#else
            return SendAsync(apId, funcName, oid, subject, content, toList, fileBase, fileNameExtension).Result;
#endif
        }

        public ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> toList, string attFileName)
        {
            FileInfo fileInfo = new FileInfo(attFileName);

            using (FileStream fileStream = fileInfo.OpenRead())
            {
                return Send(apId, funcName, oid, subject, content, toList, fileStream, fileInfo.Extension);
            }
        }

        protected ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> toList, Stream stream, string fileNameExtension)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                string fileBase = Convert.ToBase64String(bytes);
                return Send(apId, funcName, oid, subject, content, toList, fileBase, fileNameExtension);
            }
        }

        protected async Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> toList, string fileBase, string fileNameExtension)
        {
            //一開始檢查授權,有問題就擋掉
            //開始使用訊息服務平台共用Service
            SYS_MSP_LICENSE sysMspLicense = mspLicenseRepository.SearchByOid(oid);

            if (sysMspLicense == null)
            {
                return ErrorResultModel;
            }
            else
            {
                //url直接抓SYS_COM_PARAM
                string mailContextRoot = commonParameterRepository.SearchByParamName(SysComParamConstant.MAIL_URL).PARAM_VALUE;
                string mspmsgContextRoot = commonParameterRepository.SearchByParamName(SysComParamConstant.MSPMSG).PARAM_VALUE;
                string mspWsdlContextRoot = commonParameterRepository.SearchByParamName(SysComParamConstant.MSP_WSDL).PARAM_VALUE;

                //檢查msp訊息是否正確
                if (!CheckMspWsdl(mailContextRoot + mspWsdlContextRoot))
                {
                    return MspErrorResultModel;
                }
                else
                {
                    string xml = ComposeMessageXml(subject, content, toList, oid);

                    IDictionary<string, string> pairs = new Dictionary<string, string>
                    {
                        {"Authorization", sysMspLicense.LICENSE },
                        {"Message", xml }
                    };

                    ISoapMessage soapMessage = new EmicSoapMessage();
                    string soapContent = soapMessage.Encode(pairs);

                    ISoapClient soapClient = new EmicSoapClient();
                    ContentPart part = new ContentPart();
                    part.Headers["content-type"] = "text/xml; charset=utf-8;";
                    part.Headers["content-transfer-encoding"] = "binary";
                    part.Headers["content-id"] = soapClient.StartContentID;
                    part.Content = soapContent;
                    soapClient.Parts.Add(part);

                    if (!string.IsNullOrEmpty(fileBase))
                    {
                        part = new ContentPart();
                        part.Headers["content-type"] = GetMimeType(fileNameExtension);
                        part.Headers["content-transfer-encoding"] = "base64";
                        part.Headers["content-id"] = string.Format("<{0}>", Guid.NewGuid().ToString());
                        part.Content = fileBase;
                        soapClient.Parts.Add(part);
                    }

                    pairs = soapMessage.Decode(await soapClient.SendAsync(mailContextRoot + mspmsgContextRoot));
                    string responseXml = pairs["return"];
                    ResultModel resultModel = XmlToResultModel(responseXml);

                    return resultModel;
                }
            }
        }

        public async Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> toList, string attFileName)
        {
            FileInfo fileInfo = new FileInfo(attFileName);

            using (FileStream fileStream = fileInfo.OpenRead())
            {
                return await SendAsync(apId, funcName, oid, subject, content, toList, fileStream, fileInfo.Extension);
            }
        }

        protected async Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> toList, Stream stream, string fileNameExtension)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                string fileBase = Convert.ToBase64String(bytes);
                return await SendAsync(apId, funcName, oid, subject, content, toList, fileBase, fileNameExtension);
            }
        }

        protected bool CheckMspWsdl(string url)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, url);
            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        protected ResultModel XmlToResultModel(string xml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("msp", "msp.emic.gov.tw:1.0");
            var root = xmlDoc.DocumentElement;

            return new ResultModel
            {
                Serial = root.SelectSingleNode("msp:mspSerial", nsmgr)?.InnerText,
                Sender = root.SelectSingleNode("msp:sender", nsmgr)?.InnerText,
                Sent = root.SelectSingleNode("msp:sent", nsmgr)?.InnerText,
                TicketID = root.SelectSingleNode("msp:ticketID", nsmgr)?.InnerText,
                ReturnCode = root.SelectSingleNode("msp:returnCode", nsmgr)?.InnerText,
                ReturnDesc = root.SelectSingleNode("msp:returnDesc", nsmgr)?.InnerText
            };
        }

        protected ResultModel CreateErrorResultModel(string returnCode, string returnDesc)
        {
            DateTime now = DateTime.Now;

            return new ResultModel
            {
                Serial = now.ToString("yyyyMMddHHmmssfff"),
                Sender = "msp.emic.gov.tw",
                Sent = now.ToString("yyyy-MM-ddTHH:mm:ss.fff"),
                TicketID = "0",
                ReturnCode = returnCode,
                ReturnDesc = returnDesc
            };
        }

        protected string GetMimeType(string fileNameExtension)
        {
            IDictionary<string, string> extToMime = new Dictionary<string, string>
            {
                { "jpg", "image/jpg" },
                { "jpeg", "image/jpeg" },
                { "pdf", "application/pdf" },
                { "doc", "application/doc" },
                { "docx", "application/docx" },
                { "xls", "application/xls" },
                { "xlsx", "application/xlsx" },
                { "tif", "image/tif" },
                { "tiff", "image/tiff" }
            };

            if (extToMime.Keys.Contains(fileNameExtension))
                return extToMime[fileNameExtension];
            else
                return fileNameExtension;
        }

        protected string ComposeMessageXml(string headline, string description, IEnumerable<string> toList, string oid)
        {
            DateTime now = DateTime.Now;
            string mspSerial = now.ToString("yyyyMMddHHmmssfff");
            string timeFormat = now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
            string empContextRoot = commonParameterRepository.SearchByParamName(SysComParamConstant.EMP_CONTEXT_ROOT).PARAM_VALUE;
            string sender = "nec@nec.com.tw";

            StringBuilder xml = new StringBuilder();

            xml.Append("<mspSend>");
            xml.Append("<mspSerial>" + mspSerial + "</mspSerial>");
            xml.Append("<sender>" + sender + "</sender>");
            xml.Append("<sent>" + timeFormat + "</sent>");
            xml.Append("<channelInfo>");
            xml.Append("<chCategory>");
            xml.Append("<chCategoryID>" + CategoryID + "</chCategoryID>");
            xml.Append("<channel>");
            xml.Append("<chID>" + ChannelID + "</chID>");
            xml.Append("</channel>");
            xml.Append("</chCategory>");
            xml.Append("</channelInfo>");

            if (oid.Equals("OP"))
                xml.Append("<msgType>2</msgType>");
            else
            {
                if (empContextRoot.Equals(DevelopSite))
                    xml.Append("<msgType>0</msgType>");
                else if (empContextRoot.Equals(ProductionSite))
                    xml.Append("<msgType>1</msgType>");
            }

            xml.Append("<info>");
            xml.Append(ComposeAudience(toList));
            xml.Append("<expires>" + timeFormat + "</expires>");
            xml.Append("<headline>" + headline + "</headline>");
            xml.Append("<description>" + description + "</description>");
            xml.Append("<intervalOfSend>5</intervalOfSend>");
            xml.Append("<maxOfSend>5</maxOfSend>");
            xml.Append("</info>");
            xml.Append("</mspSend>");

            return xml.ToString();
        }

        protected abstract string CategoryID { get; }

        protected abstract string ChannelID { get; }

        protected abstract string ComposeAudience(IEnumerable<string> toList);
    }
}
