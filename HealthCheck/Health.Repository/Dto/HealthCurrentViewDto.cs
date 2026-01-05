using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Repository.Dto
{
    public class HealthCurrentViewDto
    {
        public string SYSTEM_ID { get; set; }

        public string SYSTEM_NAME { get; set; }

        public string VM_ID { get; set; }

        public string VM_IPv4 { get; set; }

        public string VM_IPv6 { get; set; }

        public bool ISVIP { get; set; }

        public bool STATUS { get; set; }
    }
}
