using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSP.SoapService.Models
{
    public class ContentPart
    {
        public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        public string Content { get; set; }
    }
}
