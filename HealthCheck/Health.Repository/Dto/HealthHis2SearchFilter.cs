using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Repository.Dto
{
    public class HealthHis2SearchFilter
    {
        public string SYSTEM_ID { get; set; }

        public string FUNCTION_CODE { get; set; }

        public DateTime? START_TIME { get; set; }

        public DateTime? END_TIME { get; set; }

        public Nullable<bool> OVER_THRESHOLD { get; set; }
    }
}
