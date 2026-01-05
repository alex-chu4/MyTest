using System.Xml.Serialization;

namespace MSP.Service.Models
{
    public class FaxModel
    {
        [XmlElement(ElementName = "fax")]
        public string Fax { get; set; }
    }
}
