using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSP.SoapService.Services.Emic
{
    public class EmicSoapClient : BaseSoapClient
    {
        public override string Boundary { get; set; } = "MSP_BOUNDARY";

        public override string StartContentID { get; set; } = "<nec@nec.com.tw>";

        public override string Send(string url)
        {
            if (!Headers.Keys.Contains("SOAPAction"))
                Headers["SOAPAction"] = "urn:MSPMsgRequest";
            if (!Headers.Keys.Contains("keep-alive"))
                Headers["keep-alive"] = "True";
            if (!Headers.Keys.Contains("accept-encoding"))
                Headers["accept-encoding"] = "gzip, deflate";

            return base.Send(url);
        }

        public override Task<string> SendAsync(string url)
        {
            if (!Headers.Keys.Contains("SOAPAction"))
                Headers["SOAPAction"] = "urn:MSPMsgRequest";
            if (!Headers.Keys.Contains("keep-alive"))
                Headers["keep-alive"] = "True";
            if (!Headers.Keys.Contains("accept-encoding"))
                Headers["accept-encoding"] = "gzip, deflate";

            return base.SendAsync(url);
        }
    }
}
