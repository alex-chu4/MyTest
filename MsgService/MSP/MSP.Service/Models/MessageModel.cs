using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace MSP.Service.Models
{
    public class MessageModel
    {
        [XmlElement(ElementName = "oid")]
        public string Oid { get; set; }

        [XmlElement(ElementName = "apId")]
        public string ApId { get; set; }

        [XmlElement(ElementName = "funcName")]
        public string FuncName { get; set; }

        [XmlElement(ElementName = "subject")]
        public string Subject { get; set; }

        [XmlElement(ElementName = "content")]
        public string Content { get; set; }

        [XmlElement(ElementName = "faxList")]
        public IEnumerable<FaxModel> FaxList { get; set; }

        [XmlElement(ElementName = "emailList")]
        public IEnumerable<EmailModel> EmailList { get; set; }

        [XmlElement(ElementName = "mobileList")]
        public IEnumerable<MobileModel> MobileList { get; set; }

        [XmlElement(ElementName = "fileBase")]
        public string FileBase { get; set; }

        [XmlElement(ElementName = "mineType")]
        public string MimeType { get; set; }

        [XmlElement(ElementName = "userType")]
        public string UserType { get; set; }
    }
}
