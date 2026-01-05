using System.Xml.Serialization;

namespace MSP.Service.Models
{
    public class EmailModel
    {
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }
    }
}
