using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020402
{
    public class EDD2020402SearchModelDto
    {
        /// <summary>
        /// Gets or sets 時間起
        /// </summary>
        [DisplayName("時間起")]
        public string datetimes { get; set; }

        /// <summary>
        /// Gets or sets 時間迄
        /// </summary>
        [DisplayName("時間迄")]
        public string datetimee { get; set; }

        /// <summary>
        /// Gets or sets 填報單位名稱
        /// </summary>
        [DisplayName("填報單位名稱")]
        public string UNIT_NAME { get; set; }

        /// <summary>
        /// Gets or sets 填報單位代碼
        /// </summary>
        [DisplayName("填報單位代碼")]
        public string UNIT_ID { get; set; }

        /// <summary>
        /// Gets or sets 主分類
        /// </summary>
        [DisplayName("主分類")]
        public string MASTER_TYPE_ID { get; set; }

        /// <summary>
        /// Gets or sets 次分類
        /// </summary>
        [DisplayName("次分類")]
        public string SECONDARY_TYPE_ID { get; set; }

        /// <summary>
        /// Gets or sets 細分類
        /// </summary>
        [DisplayName("細分類")]
        public string DETAIL_TYPE_ID { get; set; }

        /// <summary>
        /// Gets or sets 資源項目
        /// </summary>
        [DisplayName("資源項目")]
        public string RESOURCE_ID { get; set; }
    }
}
