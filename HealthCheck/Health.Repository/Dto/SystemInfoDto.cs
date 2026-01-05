using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Repository.Dto
{
    public class SystemInfoDto
    {
        public int OID { get; set; }

        public string ID { get; set; }

        public string NAME { get; set; }

        public string DESC { get; set; }

        public Nullable<float> THRESHOLD { get; set; }

        public Nullable<bool> KEY_FUNCTION { get; set; }

        public DateTime CREATE_TIME { get; set; }

        public Nullable<DateTime> UPDATE_TIME { get; set; }
    }
}
