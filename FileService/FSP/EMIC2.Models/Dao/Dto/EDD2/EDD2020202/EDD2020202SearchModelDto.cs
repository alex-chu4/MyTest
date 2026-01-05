using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020202
{
    public class EDD2020202SearchModelDto
    {
        public int? UNIT_ID { get; set; }

        public int FUNCTION { get; set; }

        public string ACTION { get; set; }

        public DataTable DATA_TBL { get; set; }
    }
}
