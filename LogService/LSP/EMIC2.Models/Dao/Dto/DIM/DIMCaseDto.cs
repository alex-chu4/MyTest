using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.DIM
{
    public class DIMCaseDto
    {
        public string CASE_NO { get; set; }

        public string CASE_LOCATION { get; set; }

        public DateTime? CASE_TIME { get; set; }

        public decimal? Distance { get; set; }

        public string CASE_WGS84_PTS { get; set; }

        public string CITY_ID { get; set; }

        public string TOWN_ID { get; set; }

        public decimal TheDays { get; set; }

        /// <summary> Point 的 X 座標 </summary>
        public double Place_X { get; set; }

        /// <summary>Point 的 Y 座標</summary>
        public double Place_Y { get; set; }

    }
}
