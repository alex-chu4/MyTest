using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020406
{
    public class DetailSearchModelDto
    {
        public int unitLocationID { get; set; }

        public string resourceName { get; set; }

        public int MASTER_TYPE_ID { get; set; }

        public int SECONDARY_TYPE_ID { get; set; }

        public int DETAIL_TYPE_ID { get; set; }

        public int RESOURCE_ID { get; set; }
    }
}
