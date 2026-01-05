using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020405
{
    public class EDD2_020405_DDto
    {
        /// <summary>
        /// Gets or sets 資源項目流水號
        /// </summary>
        [DisplayName("資源項目流水號")]
        public int? RESOURCE_ID { get; set; }

        /// <summary>
        /// Gets or sets 資源項目名稱
        /// </summary>
        [DisplayName("資源項目名稱")]
        public string RESOURCE_NAME { get; set; }

        /// <summary>
        /// Gets or sets 保管場所
        /// </summary>
        [DisplayName("保管場所")]
        public string LOCATION_NAME { get; set; }

        /// <summary>
        /// Gets or sets 原有存量
        /// </summary>
        [DisplayName("原有存量")]
        public string LAST_CURRENT_QTY { get; set; }

        /// <summary>
        /// Gets or sets 更新後存量
        /// </summary>
        [DisplayName("更新後存量")]
        public string CURRENT_QTY { get; set; }

        /// <summary>
        /// Gets or sets 可調度量
        /// </summary>
        [DisplayName("可調度量")]
        public string AVAILABLE_QTY { get; set; }

        /// <summary>
        /// Gets or sets 備註
        /// </summary>
        [DisplayName("備註")]
        public string REMARK { get; set; }

        /// <summary>
        /// Gets or sets 填報時間
        /// </summary>
        [DisplayName("填報時間")]
        public DateTime? CHECK_DATE { get; set; }
    }
}
