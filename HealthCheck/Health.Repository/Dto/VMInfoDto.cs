using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Repository.Dto
{
    public class VMInfoDto
    {
        public int OID { get; set; }

        public string ID { get; set; }

        public string NAME { get; set; }

        public string IPv4 { get; set; }

        public string IPv6 { get; set; }

        public bool ISVIP { get; set; }

        public string MEMO { get; set; }

        public DateTime CREATE_TIME { get; set; }

        public Nullable<DateTime> UPDATE_TIME { get; set; }
    }
}
