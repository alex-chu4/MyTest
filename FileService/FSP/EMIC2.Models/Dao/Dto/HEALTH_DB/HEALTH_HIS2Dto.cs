using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.HEALTH_DB
{
    public class HEALTH_HIS2Dto
    {
        public string SYSTEM_ID { get; set; }

        public string FUNCTION_CODE { get; set; }

        public string IPv4 { get; set; }

        public float TOTAL_TIME { get; set; }

        public float SYSTEM_TIME { get; set; }

        public float DB_TIME { get; set; }

        public DateTime? CREATE_TIME { get; set; }
    }
}