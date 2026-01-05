///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030109Dto.cs
//  程式名稱：
//  ERA2030109Dto
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-26       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2030109Dto
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.ERA
{
    public class ERA2030109Dto : ERA2Dto
    {
        /// <summary>
        /// Gets or sets 縣市別
        /// </summary>
        [Display(Name = "縣市別")]
        public string CITY_NAME { get; set; }

        /// <summary>
        /// Gets or sets (黃色警戒)土石流潛勢溪流數
        /// </summary>
        [Display(Name = "土石流潛勢溪流數")]
        public int? AMBER_RIVERS { get; set; }

        /// <summary>
        /// Gets or sets (黃色警戒)座落鄉鎮數
        /// </summary>
        [Display(Name = "座落鄉鎮數")]
        public int? AMBER_TOWNSHIP { get; set; }

        /// <summary>
        /// Gets or sets (黃色警戒)座落村里數
        /// </summary>
        [Display(Name = "座落村里數")]
        public int? AMBER_VILLAGE { get; set; }

        /// <summary>
        /// Gets or sets (紅色警戒)土石流潛勢溪流數
        /// </summary>
        [Display(Name = "土石流潛勢溪流數")]
        public int? RED_RIVERS { get; set; }

        /// <summary>
        /// Gets or sets (紅色警戒)座落鄉鎮數
        /// </summary>
        [Display(Name = "座落鄉鎮數")]
        public int? RED_TOWNSHIP { get; set; }

        /// <summary>
        /// Gets or sets (紅色警戒)座落村里數
        /// </summary>
        [Display(Name = "座落村里數")]
        public int? RED_VILLAGE { get; set; }

        /// <summary>
        /// Gets or sets (合計)土石流潛勢溪流數
        /// </summary>
        public int? C4_TOTAL { get; set; }

        /// <summary>
        /// Gets or sets 狀態
        /// </summary>
        [Display(Name = "狀態")]
        public string STATUS_C4 { get; set; }

        /// <summary>
        /// Gets or sets 排序
        /// </summary>
        public string SHOW_ORDER { get; set; }
    }
}
