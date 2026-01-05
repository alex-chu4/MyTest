using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.ERA.ERA20521
{
    public class ERA2_CHECK_EXT_TAG
    {
        public bool ISSUCCESSFUL { get; set; }

        public string MSG { get; set; }
        
        public string EOC_ID { get; set; }

        public string EOC_NAME { get; set; }

        public string PRJ_NO { get; set; }

        public string RPT_CODE { get; set; }

        public string CITY_NAME { get; set; }
    }
}
