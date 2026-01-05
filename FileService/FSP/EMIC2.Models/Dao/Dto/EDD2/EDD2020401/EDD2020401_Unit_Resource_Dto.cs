using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020401
{
    public class EDD2020401_Unit_Resource_Dto
    {
        public int UNIT_ID { get; set; }

        public int RESOURCE_STOCK_ID { get; set; }

        public int RESOURCE_ID { get; set; }

        public string RESOURCE_NAME { get; set; }

        public int LOCATION_ID { get; set; }

        public string LOCATION_NAME { get; set; }

        public int CURRENT_QTY { get; set; }

        public int AVAILABLE_QTY { get; set; }

        public int INPUT_CURRENT_QTY { get; set; }

        public int INPUT_AVAILABLE_QTY { get; set; }

        public string REMARK { get; set; }

        public DateTime? MODIFIED_TIME { get; set; }
    }
}
