using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Dao
{
    public class MessageModelDao
    {
        public string Oid { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public List<EMAIL> EmailList { get; set; }

        public string UserType { get; set; }
    }

    public class EMAIL
    {
        public string Email { get; set; }
    }

    public class ResponseMessage
    {
        public string Serial { get; set; }

        public string Sender { get; set; }

        public string Sent { get; set; }

        public string TicketID { get; set; }

        public string ReturnCode { get; set; }

        public string ReturnDesc { get; set; }
    }
}
