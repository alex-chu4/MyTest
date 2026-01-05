using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EEM2.EEM2010303
{
    public class EEM2010303Dto
    {
        public EEM2010303Dto()
        {
            this.IS_Checked = false;
            this.Suggest = false;
        }

        public bool IS_Checked { get; set; }

        public bool Suggest { get; set; }

        public decimal PRJ_GROUP_UID { get; set; }

        public string CASE_NAME { get; set; }

        public string EOC_ID { get; set; }

        public string EOC_NAME { get; set; }

        public DateTime PRJ_STIME { get; set; }

        public string PRJ_STIME_Txt { get; set; }

        public int EstablishedDays { get; set; }

        public string EOC_PARENT { get; set; }

        public DateTime? ParentRemoveTime { get; set; }

        public string ParentRemoveTimeText { get; set; }

        public string CONTACT_NAME { get; set; }

        public string CONTACT_EMAIL { get; set; }

        public decimal PRJ_NO { get; set; }
    }
}
