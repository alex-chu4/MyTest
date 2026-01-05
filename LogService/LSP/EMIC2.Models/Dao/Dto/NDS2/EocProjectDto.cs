namespace EMIC2.Models.Dao.Dto.NDS2
{
    using System;

    public class EocProjectDto
    {
        public string PRJ_NO { get; set; }

        public string EOC_ID { get; set; }

        public string PRJ_TYPE_NAME { get; set; }

        public decimal PRJ_GROUP_UID { get; set; }

        public string CASE_NAME { get; set; }

        public DateTime PRJ_STIME { get; set; }

        public DateTime? PRJ_ETIME { get; set; }

        public string OPEN_LV { get; set; }

        public string OPEN_STATUS { get; set; }
    }
}
