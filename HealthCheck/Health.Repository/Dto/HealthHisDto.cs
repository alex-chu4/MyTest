using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Repository.Dto
{
    public class HealthHisDto
    {
        public string SYSTEM_ID { get; set; }

        public string VM_ID { get; set; }

        public string VM_IPv4 { get; set; }

        public string VM_IPv6 { get; set; }

        public bool ISVIP { get; set; }

        public bool? STATUS { get; set; }

        public float? TOTAL_TIME { get; set; }

        public float? SYSTEM_TIME { get; set; }

        public float? DB_TIME { get; set; }

        public float? PROCESS_TIME
        {
            get
            {
                if (TOTAL_TIME.HasValue && DB_TIME.HasValue)
                    return TOTAL_TIME.Value - DB_TIME.Value;
                else
                    return null;
            }
        }

        public DateTime CREATE_TIME { get; set; }
    }
}
