using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020405
{
    public class EDD2020405SearchModelDto
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
        /// Gets or sets 填報單位地區(縣市)
        /// </summary>
        [DisplayName("填報單位地區(縣市)")]
        public string CITY_NAME { get; set; }

        /// <summary>
        /// Gets or sets 填報單位地區(鄉鎮)
        /// </summary>
        [DisplayName("填報單位地區(鄉鎮)")]
        public string TOWN_NAME { get; set; }

        /// <summary>
        /// Gets or sets 填報單位名稱
        /// </summary>
        [DisplayName("填報單位名稱")]
        public string UNIT_NAME { get; set; }

        /// <summary>
        /// Gets or sets 填報單位代碼
        /// </summary>
        [DisplayName("填報單位代碼")]
        public int UNIT_ID { get; set; }

        /// <summary>
        /// Gets or sets 一級單位
        /// </summary>
        [DisplayName("一級單位")]
        public string unit_level_1 { get; set; }

        /// <summary>
        /// Gets or sets 二級單位
        /// </summary>
        [DisplayName("二級單位")]
        public string unit_level_2 { get; set; }

        /// <summary>
        /// Gets or sets 三級單位
        /// </summary>
        [DisplayName("三級單位")]
        public string unit_level_3 { get; set; }

        /// <summary>
        /// Gets or sets 四級單位
        /// </summary>
        [DisplayName("四級單位")]
        public string unit_level_4 { get; set; }

        /// <summary>
        /// Gets or sets 填報狀態
        /// </summary>
        [DisplayName("填報狀態")]
        public int status { get; set; }

        /// <summary>
        /// Gets or sets 填報時間
        /// </summary>
        [DisplayName("資源項目名稱")]
        public string RESOURCE_NAME { get; set; }
        
        /// <summary>
        /// Gets or sets 填報時間
        /// </summary>
        [DisplayName("填報時間")]
        public string MAX_CHECK_DATE { get; set; }
    }
}
