using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSP.SoapService
{
    public class SoapException : Exception
    {
        public SoapException(string message) : base(message)
        {
        }
    }
}
