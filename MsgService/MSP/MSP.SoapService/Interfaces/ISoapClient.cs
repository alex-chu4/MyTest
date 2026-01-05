using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MSP.SoapService.Models;

namespace MSP.SoapService.Interfaces
{
    public interface ISoapClient
    {
        IDictionary<string, string> Headers { get; }

        IList<ContentPart> Parts { get; }

        string Boundary { get; set; }

        string StartContentID { get; set; }

        string Send(string url);

        Task<string> SendAsync(string url);
    }
}
