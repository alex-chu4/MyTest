using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.API.Models
{
    public class HealthHisModel
    {
        public string SYSTEM_ID { get; set; }

        public string SYSTEM_NAME { get; set; }

        public string VM_ID { get; set; }

        public Nullable<float> THRESHOLD { get; set; }
    }
}