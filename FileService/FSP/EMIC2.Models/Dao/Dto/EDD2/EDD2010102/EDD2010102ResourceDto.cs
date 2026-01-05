using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2010102
{
    public class EDD2010102ResourceDto
    {
        public string EOC_PARENT { get; set; }

        public string EOC_ID { get; set; }

        public string EOC_NAME { get; set; }

        public int RESOURCE_ID { get; set; }

        public string RESOURCE_NAME { get; set; }

        public int AVAILABLE_QTY { get; set; }

        public int CURRENT_QTY { get; set; }

        public string LOCATION_NAME { get; set; }

        public string LOCATION_LONGITUDE { get; set; }

        public string LOCATION_LATITUDE { get; set; }
    }
}