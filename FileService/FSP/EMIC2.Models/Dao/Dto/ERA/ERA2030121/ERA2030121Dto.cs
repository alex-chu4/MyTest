///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030121Dto.cs
//  程式名稱：
//  ERA2030121Dto
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-29       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2030121Dto
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.ERA
{
    public class ERA2030121Dto : ERA2Dto
    {
        /// <summary>
        /// Gets or sets 發生時間
        /// </summary>
        [Display(Name = "發生時間")]
        public DateTime? STOP_TIME { get; set; }

        /// <summary>
        /// Gets or sets 開設起迄時間(年月日時)
        /// </summary>
        [Display(Name = "發生時間")]
        public string STOP_TIME_TEXT { get; set; }

        /// <summary>
        /// Gets or sets 開設起迄時間時
        /// </summary>
        [Display(Name = "發生時間時")]
        public int? STOP_TIME_HOUR { get; set; }

        /// <summary>
        /// Gets or sets 開設起迄時間分
        /// </summary>
        [Display(Name = "發生時間分")]
        public int? STOP_TIME_MINUTE { get; set; }

        /// <summary>
        /// Gets or sets 航空公司別
        /// </summary>
        [Display(Name = "航空公司別")]
        public string AIRLINE { get; set; }

        /// <summary>
        /// Gets or sets 航線或班次
        /// </summary>
        [Display(Name = "航線或班次")]
        public string ROUTE_SERVICE { get; set; }

        /// <summary>
        /// Gets or sets 航班狀態
        /// </summary>
        [Display(Name = "航班狀態")]
        public string AIR_STATUS { get; set; }

        /// <summary>
        /// Gets or sets 原因
        /// </summary>
        [Display(Name = "原因")]
        public string REASON { get; set; }

        /// <summary>
        /// Gets or sets 備註（預計通航時間）
        /// </summary>
        [Display(Name = "備註（預計通航時間）")]
        public string REMARK { get; set; }
    }
}
