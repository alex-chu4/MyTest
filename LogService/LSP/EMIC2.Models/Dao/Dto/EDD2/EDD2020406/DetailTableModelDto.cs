using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020406
{
    public class DetailTableModelDto
    {
        public int RESOURCE_ID { get; set; }

        public string RESOURCE_NAME { get; set; }

        public string CURRENT_QTY { get; set; }

        public string AVAILABLE_QTY { get; set; }

        public string SUPPORT_QTY { get; set; }

        public string USEFUL_QTY { get; set; }

        public DateTime? MODIFIED_TIME { get; set; }

        public string MODIFIED_TIME_TXT { get; set; }
    }
}
