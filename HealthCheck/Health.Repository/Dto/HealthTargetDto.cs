using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Repository.Dto
{
    public class HealthTargetDto
    {
        public int OID { get; set; }

        public string SYSTEM_ID { get; set; }

        public string VM_ID { get; set; }

        public string VM_IPv4 { get; set; }

        public string VM_IPv6 { get; set; }

        public bool ISVIP { get; set; }

        public string URL { get; set; }

        public bool ACTIVE { get; set; }

        public DateTime CREATE_TIME { get; set; }

        public DateTime? UPDATE_TIME { get; set; }
    }
}
