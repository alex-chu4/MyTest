using System;

namespace EMIC2.Models.Dao.Dto.DIM.DIM2050101
{
    public class SearchProjectDto
    {
        public string EOC_ID { get; set; }

        public DateTime RPT_TIME_S { get; set; }

        public DateTime RPT_TIME_E { get; set; }

        public string DIS_DATA_UID { get; set; }

        public string CASE_NAME { get; set; }
    }
}
