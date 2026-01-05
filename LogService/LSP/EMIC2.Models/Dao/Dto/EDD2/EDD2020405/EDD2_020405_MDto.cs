using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020405
{
    public class EDD2_020405_MDto
    {
        /// <summary>
        /// Gets or sets 機關單位流水號
        /// </summary>
        [DisplayName("機關單位流水號")]
        public int UNIT_ID { get; set; }

        /// <summary>
        /// Gets or sets 機關單位名稱
        /// </summary>
        [DisplayName("機關單位名稱")]
        public string UNIT_NAME { get; set; }

        /// <summary>
        /// Gets or sets 縣市
        /// </summary>
        [DisplayName("縣市")]
        public string CITY_NAME { get; set; }

        /// <summary>
        /// Gets or sets 鄉鎮
        /// </summary>
        [DisplayName("鄉鎮")]
        public string TOWN_NAME { get; set; }

        /// <summary>
        /// Gets or sets 單位層級
        /// </summary>
        [DisplayName("單位層級")]
        public int UNIT_LEVEL { get; set; }

        /// <summary>
        /// Gets or sets 機關單位OID
        /// </summary>
        [DisplayName("機關單位OID")]
        public string OID { get; set; }

        /// <summary>
        /// Gets or sets 填報狀況
        /// </summary>
        [DisplayName("填報狀況")]
        public string CHECK_DESCR { get; set; }

        /// <summary>
        /// Gets or sets 填報日期
        /// </summary>
        [DisplayName("填報日期")]
        public DateTime? CHECK_DATE { get; set; }

        /// <summary>
        /// Gets or sets 填報狀態說明
        /// </summary>
        [DisplayName("填報狀態說明")]
        public string CHECK_ACTION { get; set; }

        /// <summary>
        /// Gets or sets 填報狀態
        /// </summary>
        [DisplayName("填報狀態")]
        public int CHECK_STATUS { get; set; }
    }
}
