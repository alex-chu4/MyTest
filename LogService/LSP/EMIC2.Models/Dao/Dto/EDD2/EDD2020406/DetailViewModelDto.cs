using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020406
{
    public class DetailViewModelDto
    {
        public int UNIT_ID { get; set; }

        public int LOCATION_ID { get; set; }

        public string UNIT_NAME { get; set; }

        public string LOCATION_NAME { get; set; }

        public string CONTACT_NAME { get; set; }

        public DateTime? MODIFIED_TIME { get; set; }

        public string MODIFIED_TIME_TXT { get; set; }

        public string CONTACT_TEL { get; set; }

        public string CITY_NAME { get; set; }

        public string TOWN_NAME { get; set; }

        public string LOCATION_ADDRESS { get; set; }

        public string LOCATION_COORDINATE_X { get; set; }

        public string LOCATION_COORDINATE_Y { get; set; }

        public string LOCATION_LONGITUDE { get; set; }

        public string LOCATION_LATITUDE { get; set; }

        public string L_CONTACT_NAME { get; set; }

        public string L_CONTACT_TEL { get; set; }

        public string CONTACT_EXT { get; set; }

        public string CONTACT_MOBILE { get; set; }

        public string CONTACT_EMAIL { get; set; }

        public string CONTACT_FAX { get; set; }
    }
}
