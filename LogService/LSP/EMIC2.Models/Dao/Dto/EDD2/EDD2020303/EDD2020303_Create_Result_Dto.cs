using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020303
{
    public class EDD2020303_Create_Result_Dto
    {
        public int RESOURCE_STOCK_ID { get; set; }

        public int UNIT_ID { get; set; }

        public string UNIT_NAME { get; set; }

        public int RESOURCE_ID { get; set; }

        public string RESOURCE_NAME { get; set; }

        public int LOCATION_ID { get; set; }

        public string LOCATION_NAME { get; set; }

        public int CURRENT_QTY { get; set; }

        public int AVAILABLE_QTY { get; set; }

        public string CONTACT_NAME { get; set; }

        public string CONTACT_EMAIL { get; set; }

        public string CONTACT_TEL { get; set; }

        public string CONTACT_NAME_1 { get; set; }

        public string CONTACT_EMAIL_1 { get; set; }
    }
}
