using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using MSP.SoapService.Interfaces;

namespace MSP.SoapService.Services.Emic
{
    public class EmicSoapMessage : ISoapMessage
    {
        public IDictionary<string, string> Decode(string xml)
        {
            IDictionary<string, string> pairs = new Dictionary<string, string>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ns", "msp.emic.gov.tw:1.0");

            string result = xmlDoc.SelectSingleNode("//soapenv:Envelope/soapenv:Body/ns:MSPMsgRequestResponse/ns:return", nsmgr)?.InnerText;
            pairs["return"] = WebUtility.HtmlDecode(result);

            return pairs;
        }

        public string Encode(IDictionary<string, string> pairs)
        {
            string xml = string.Format("<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                "<s:Body xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
                "<MSPMsgRequest xmlns=\"msp.emic.gov.tw:1.0\">" +
                "<Authorization>{0}</Authorization>" +
                "<Message>{1}</Message>" +
                "<Attachments xsi:nil=\"true\" />" +
                "</MSPMsgRequest>" +
                "</s:Body>" +
                "</s:Envelope>", pairs["Authorization"], WebUtility.HtmlEncode(pairs["Message"]));

            return xml;
        }
    }
}
