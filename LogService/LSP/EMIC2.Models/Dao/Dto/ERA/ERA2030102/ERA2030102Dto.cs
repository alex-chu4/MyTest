///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030108Dto.cs
//  程式名稱：
//  ERA2030108Dto
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-27       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2030108Dto
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.ERA
{
    public class ERA2030102Dto : ERA2Dto
    {
        public ERA2030102Dto()
        {
            this.ACCEPT_PEOPLE_MALE = 0;
            this.ACCEPT_PEOPLE_FEMALE = 0;
            this.COUNT_PEOPLE_MALE = 0;
            this.COUNT_PEOPLE_FEMALE = 0;
            this.PEOPLE_FOOD_NUM = 0;
            this.PEOPLE_FOOD_DAY = 0;
        }

        /// <summary>
        /// Gets or sets 縣市別地區
        /// </summary>
        [Display(Name = "縣市別地區")]
        public string CITY_TOWN_NAME { get; set; }

        /// <summary>
        /// Gets or sets 縣市別
        /// </summary>
        [Display(Name = "縣市別")]
        public string CITY_NAME { get; set; }

        /// <summary>
        /// Gets or sets 鄉鎮
        /// </summary>
        [Display(Name = "鄉鎮")]
        public string TOWN_NAME { get; set; }

        /// <summary>
        /// Gets or sets 收容所名稱
        /// </summary>
        [Display(Name = "收容所名稱")]
        public string ACCCEPT_PLACE { get; set; }

        /// <summary>
        /// Gets or sets 開設時間
        /// </summary>
        [Display(Name = "開設時間")]
        public DateTime? ESTABLISH_TIME { get; set; }

        /// <summary>
        /// Gets or sets 開設時間
        /// </summary>
        [Display(Name = "開設時間")]
        public string ESTABLISH_TIME_TEXT { get; set; }

        /// <summary>
        /// Gets or sets 開設時間時
        /// </summary>
        [Display(Name = "開設時間時")]
        public int? ESTABLISH_TIME_HOUR { get; set; }

        /// <summary>
        /// Gets or sets 開設時間分
        /// </summary>
        [Display(Name = "開設時間分")]
        public int? ESTABLISH_TIME_MINUTE { get; set; }

        /// <summary>
        /// Gets or sets 撤除時間
        /// </summary>
        [Display(Name = "撤除時間")]
        public DateTime? RETREAT_TIME { get; set; }

        /// <summary>
        /// Gets or sets 撤除時間
        /// </summary>
        [Display(Name = "撤除時間")]
        public string RETREAT_TIME_TEXT { get; set; }

        /// <summary>
        /// Gets or sets 撤除時間時
        /// </summary>
        [Display(Name = "撤除時間時")]
        public int? RETREAT_TIME_HOUR { get; set; }

        /// <summary>
        /// Gets or sets 撤除時間分
        /// </summary>
        [Display(Name = "撤除時間分")]
        public int? RETREAT_TIME_MINUTE { get; set; }

        /// <summary>
        /// Gets or sets 目前收容人數-男
        /// </summary>
        [Display(Name = "目前收容人數-男")]
        public int? ACCEPT_PEOPLE_MALE { get; set; }

        /// <summary>
        /// Gets or sets 目前收容人數-女
        /// </summary>
        [Display(Name = "目前收容人數-女")]
        public int? ACCEPT_PEOPLE_FEMALE { get; set; }

        /// <summary>
        /// Gets or sets 累計收容人數-男
        /// </summary>
        [Display(Name = "累計收容人數-男")]
        public int? COUNT_PEOPLE_MALE { get; set; }

        /// <summary>
        /// Gets or sets 累計收容人數-女
        /// </summary>
        [Display(Name = "累計收容人數-女")]
        public int? COUNT_PEOPLE_FEMALE { get; set; }

        /// <summary>
        /// Gets or sets 目前儲糧預估可再供應狀況-人數
        /// </summary>
        [Display(Name = "目前儲糧預估可再供應狀況-人數")]
        public int? PEOPLE_FOOD_NUM { get; set; }

        /// <summary>
        /// Gets or sets 目前儲糧預估可再供應狀況-日數
        /// </summary>
        [Display(Name = "目前儲糧預估可再供應狀況-日數")]
        public int? PEOPLE_FOOD_DAY { get; set; }

        /// <summary>
        /// Gets or sets 是否以開口契約或民間團體持續供應熱食
        /// </summary>
        [Display(Name = "是否以開口契約或民間團體持續供應熱食")]
        public string TRUE_FALSE_CHOOSE { get; set; }

        /// <summary>
        /// Gets or sets 備註
        /// </summary>
        [Display(Name = "備註")]
        public string REMARK { get; set; }

        /// <summary>
        /// Gets or sets 排序
        /// </summary>
        [Display(Name = "排序")]
        public string SHOW_ORDER { get; set; }
    }
}
