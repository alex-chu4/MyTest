using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.DIM.DIM2010301
{
    public class ResultResourceDto
    {
        public string UNIT_NAME { get; set; }
        public string RESOURCE_NAME { get; set; }
        public string LOCATION_NAME { get; set; }
        public string LOCATION_CITY_TOWN { get; set; }
        public int CURRENT_QTY { get; set; }
        public DateTime? MODIFIED_TIME { get; set; }
        public string WGS84_X { get; set; }

        public string WGS84_Y { get; set; }
        public string DISTANCE { get; set; }
    }
}
