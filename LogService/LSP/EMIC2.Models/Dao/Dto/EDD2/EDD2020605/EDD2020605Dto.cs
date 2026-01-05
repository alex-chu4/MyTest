using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020605
{
    public class EDD2020605Dto
    {
        public string LOCATION_NAME { get; set; }

        public string CITY_NAME { get; set; }

        public string TOWN_NAME { get; set; }

        public string LOCATION_ADDRESS { get; set; }

        public string LAT { get; set; }

        public string LON { get; set; }

        public string RESOURCE_NAME { get; set; }

        public int CURRENT_QTY { get; set; }

        public string UNIT_NAME { get; set; }

        public DateTime? MODIFIED_TIME { get; set; }
    }
}
