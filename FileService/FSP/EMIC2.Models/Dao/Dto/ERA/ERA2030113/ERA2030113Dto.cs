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
    public class ERA2030113Dto : ERA2Dto
    {
        /// <summary>
        /// Gets or sets 縣市別地區
        /// </summary>
        [Display(Name = "縣市別地區")]
        public string CITY_TOWN_VILLAGE_NAME { get; set; }

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
        /// Gets or sets 村里
        /// </summary>
        [Display(Name = "村里")]
        public string VILLAGE_NAME { get; set; }

        /// <summary>
        /// Gets or sets 資料輸入時間
        /// </summary>
        [Display(Name = "資料輸入時間")]
        public DateTime? INPUTDATE { get; set; }

        /// <summary>
        /// Gets or sets 資料輸入時間
        /// </summary>
        [Display(Name = "資料輸入時間")]
        public string INPUTDATE_TEXT { get; set; }

        /// <summary>
        /// Gets or sets 資料輸入時間時
        /// </summary>
        [Display(Name = "資料輸入時間時")]
        public int? INPUTDATE_HOUR { get; set; }

        /// <summary>
        /// Gets or sets 資料輸入時間分
        /// </summary>
        [Display(Name = "資料輸入時間分")]
        public int? INPUTDATE_MINUTE { get; set; }

        /// <summary>
        /// Gets or sets 附近地名
        /// </summary>
        [Display(Name = "附近地名")]
        public string PLACE { get; set; }

        /// <summary>
        /// Gets or sets 收容地點
        /// </summary>
        [Display(Name = "收容地點")]
        public string ACCEPT_PLACE { get; set; }

        /// <summary>
        /// Gets or sets 預計撤離人數
        /// </summary>
        [Display(Name = "預計撤離人數")]
        public int? ESTIMATE_PNUM { get; set; }

        /// <summary>
        /// Gets or sets 實際撤離人數
        /// </summary>
        [Display(Name = "實際撤離人數")]
        public int? RETREAT_PNUM { get; set; }

        /// <summary>
        /// Gets or sets 累計撤離人數
        /// </summary>
        [Display(Name = "累計撤離人數")]
        public int? SUM_PNUM { get; set; }

        /// <summary>
        /// Gets or sets 排序
        /// </summary>
        [Display(Name = "排序")]
        public string SHOW_ORDER { get; set; }
    }
}
