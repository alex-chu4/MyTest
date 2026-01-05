using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Repository.Dto
{
    public class EventDto
    {
        public string SYSTEM_ID { get; set; }

        public string SYSTEM_NAME { get; set; }

        public string SYSTEM_DESC { get; set; }

        public Nullable<float> THRESHOLD { get; set; }

        public Nullable<bool> KEY_FUNCTION { get; set; }

        public string FUNCTION_CODE { get; set; }

        public string FUNCTION_NAME { get; set; }

        public string IPv4 { get; set; }
    }
}
