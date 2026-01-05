using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Repository.Dto
{
    public class NotificationInfoDto
    {
        public int OID { get; set; }

        public int TYPE { get; set; }

        public string EMAIL { get; set; }

        public string SMS { get; set; }

        public string FAX { get; set; }

        public bool ACTIVE { get; set; }

        public string MEMO { get; set; }

        public DateTime CREATE_TIME { get; set; }

        public string CREATE_USER { get; set; }

        public Nullable<DateTime> UPDATE_TIME { get; set; }

        public string UPDATE_USER { get; set; }
    }
}
