using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSP.SoapService.Interfaces
{
    public interface ISoapMessage
    {
        string Encode(IDictionary<string, string> pairs);

        IDictionary<string, string> Decode(string xml);
    }
}
