using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020402
{
    public class EDD2_020402_MDto
    {
        /// <summary>
        /// Gets or sets 填報單位
        /// </summary>
        [DisplayName("填報單位")]
        public string UNIT_NAME { get; set; }

        /// <summary>
        /// Gets or sets 填報單位層級
        /// </summary>
        [DisplayName("填報單位層級")]
        public string UNIT_LEVEL { get; set; }

        /// <summary>
        /// Gets or sets 資源項目流水號
        /// </summary>
        [DisplayName("資源項目流水號")]
        public string RESOURCE_ID { get; set; }

        /// <summary>
        /// Gets or sets 資源項目
        /// </summary>
        [DisplayName("資源項目")]
        public string RESOURCE_NAME { get; set; }

        /// <summary>
        /// Gets or sets 保管場所
        /// </summary>
        [DisplayName("保管場所")]
        public string LOCATION_NAME { get; set; }

        /// <summary>
        /// Gets or sets 異動時間
        /// </summary>
        [DisplayName("異動時間")]
        public DateTime? CHECK_DATE { get; set; }

        /// <summary>
        /// Gets or sets 異動前
        /// </summary>
        [DisplayName("異動前")]
        public string LAST_CURRENT_QTY { get; set; }

        /// <summary>
        /// Gets or sets 異動後
        /// </summary>
        [DisplayName("異動後")]
        public string CURRENT_QTY { get; set; }

        /// <summary>
        /// Gets or sets 異動量
        /// </summary>
        [DisplayName("異動量")]
        public string DIFFERENT_QTY { get; set; }

        /// <summary>
        /// Gets or sets 異動原因
        /// </summary>
        [DisplayName("異動原因")]
        public string DIFF_REASON_CODE { get; set; }

        /// <summary>
        /// Gets or sets 備註
        /// </summary>
        [DisplayName("備註")]
        public string REMARK { get; set; }

        /// <summary>
        /// Gets or sets 建立者
        /// </summary>
        [DisplayName("建立者")]
        public string CREATED_USER { get; set; }
    }
}
