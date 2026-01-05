using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Repository.Dto
{
    public class HealthHis2Dto
    {
        public string SYSTEM_ID { get; set; }

        public string FUNCTION_CODE { get; set; }

        public string IPv4 { get; set; }

        public float? THRESHOLD { get; set; }

        public float? TOTAL_TIME { get; set; }

        public float? SYSTEM_TIME { get; set; }

        public float? DB_TIME { get; set; }

        public float? CLIENT_TIME
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
