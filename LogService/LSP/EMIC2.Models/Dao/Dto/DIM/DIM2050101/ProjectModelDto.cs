using System;

namespace EMIC2.Models.Dao.Dto.DIM.DIM2050101
{
    public class ProjectModelDto
    {
        public string PRJ_NO { get; set; }

        public string EOC_ID { get; set; }

        public string CASE_NAME { get; set; }

        public DateTime PRJ_STIME { get; set; }

        public DateTime PRJ_ETIME { get; set; }

        public int OPEN_LV { get; set; }

        public string OPEN_STATUS { get; set; }

        public string DIS_DATA_UID { get; set; }

        public string DIS_NAME { get; set; }
    }
}
