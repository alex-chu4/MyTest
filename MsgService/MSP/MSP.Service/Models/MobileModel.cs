using System.Xml.Serialization;

namespace MSP.Service.Models
{
    public class MobileModel
    {
        [XmlElement(ElementName = "mobile")]
        public string Mobile { get; set; }
    }
}
