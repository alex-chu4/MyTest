using System.Xml;
using System.Xml.Serialization;

namespace MSP.Service.Models
{
    public class ResultModel
    {
        [XmlElement(ElementName = "mspSerial")]
        public string Serial { get; set; }

        [XmlElement(ElementName = "sender")]
        public string Sender { get; set; }

        [XmlElement(ElementName = "sent")]
        public string Sent { get; set; }

        [XmlElement(ElementName = "ticketID")]
        public string TicketID { get; set; }

        [XmlElement(ElementName = "returnCode")]
        public string ReturnCode { get; set; }

        [XmlElement(ElementName = "returnDesc")]
        public string ReturnDesc { get; set; }
    }
}
