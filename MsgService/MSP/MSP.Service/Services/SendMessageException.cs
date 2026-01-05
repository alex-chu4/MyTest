using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSP.Service.Services
{
    public class SendMessageException : Exception
    {
        public SendMessageException(string message) : base(message)
        {
        }
    }
}
