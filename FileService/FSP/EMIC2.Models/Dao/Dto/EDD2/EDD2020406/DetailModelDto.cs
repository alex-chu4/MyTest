using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020406
{
    public class DetailModelDto
    {
        public int UNIT_LOCATION_ID { get; set; }

        public int UNIT_ID { get; set; }

        public string UNIT_NAME { get; set; }

        public string CONTACT_NAME { get; set; }

        public string CONTACT_TEL { get; set; }

        public string LOCATION_NAME { get; set; }

        public string CITY_NAME { get; set; }

        public string TOWN_NAME { get; set; }

        public string LOCATION_ADDRESS { get; set; }

        public DateTime? MODIFIED_TIME { get; set; }

        public string MODIFIED_TIME_TXT { get; set; }

        public string MASTER_TYPE_NAME { get; set; }

        public string SECONDARY_TYPE_NAME { get; set; }

        public string DETAIL_TYPE_NAME { get; set; }

        public string RESOURCE_NAME { get; set; }

        public int AVAILABLE_QTY { get; set; }

        public string STD_UOM { get; set; }

        public int RESOURCE_STATUS { get; set; }
    }
}
