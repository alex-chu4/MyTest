///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030130Dto.cs
//  程式名稱：
//  ERA2030130Dto
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-27       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2030130Dto
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.ERA
{
    public class ERA2030130Dto : ERA2Dto
    {
        public ERA2030130Dto()
        {
            this.COUNTRYARMY = 0;
            this.AIRDUTY = 0;
            this.COUNTRYARMY1 = 0;
            this.AIRDUTY1 = 0;
            this.COUNTRYARMY2 = 0;
            this.AIRDUTY2 = 0;
            this.COUNTRYARMY3 = 0;
            this.AIRDUTY3 = 0;
            this.COUNTRYARMY4 = 0;
            this.AIRDUTY4 = 0;
            this.COUNTRYARMY5 = 0;
            this.AIRDUTY5 = 0;
            this.COUNTRYARMY6 = 0;
            this.AIRDUTY6 = 0;
            this.COUNTRYARMY7 = 0;
            this.AIRDUTY7 = 0;
        }

        /// <summary>
        /// Gets or sets 執行日期
        /// </summary>
        [Display(Name = "執行日期")]
        public DateTime? INPUTDATE { get; set; }

        /// <summary>
        /// Gets or sets 國軍
        /// </summary>
        [Display(Name = "國軍")]
        public int? COUNTRYARMY { get; set; }

        /// <summary>
        /// Gets or sets 空勤總隊
        /// </summary>
        [Display(Name = "空勤總隊")]
        public int? AIRDUTY { get; set; }

        /// <summary>
        /// Gets or sets 國軍
        /// </summary>
        [Display(Name = "國軍")]
        public int? COUNTRYARMY1 { get; set; }

        /// <summary>
        /// Gets or sets 空勤總隊
        /// </summary>
        [Display(Name = "空勤總隊")]
        public int? AIRDUTY1 { get; set; }

        /// <summary>
        /// Gets or sets 國軍
        /// </summary>
        [Display(Name = "國軍")]
        public int? COUNTRYARMY2 { get; set; }

        /// <summary>
        /// Gets or sets 空勤總隊
        /// </summary>
        [Display(Name = "空勤總隊")]
        public int? AIRDUTY2 { get; set; }

        /// <summary>
        /// Gets or sets 國軍
        /// </summary>
        [Display(Name = "國軍")]
        public int? COUNTRYARMY3 { get; set; }

        /// <summary>
        /// Gets or sets 空勤總隊
        /// </summary>
        [Display(Name = "空勤總隊")]
        public int? AIRDUTY3 { get; set; }

        /// <summary>
        /// Gets or sets 國軍
        /// </summary>
        [Display(Name = "國軍")]
        public int? COUNTRYARMY4 { get; set; }

        /// <summary>
        /// Gets or sets 空勤總隊
        /// </summary>
        [Display(Name = "空勤總隊")]
        public int? AIRDUTY4 { get; set; }

        /// <summary>
        /// Gets or sets 國軍
        /// </summary>
        [Display(Name = "國軍")]
        public int? COUNTRYARMY5 { get; set; }

        /// <summary>
        /// Gets or sets 空勤總隊
        /// </summary>
        [Display(Name = "空勤總隊")]
        public int? AIRDUTY5 { get; set; }

        /// <summary>
        /// Gets or sets 國軍
        /// </summary>
        [Display(Name = "國軍")]
        public int? COUNTRYARMY6 { get; set; }

        /// <summary>
        /// Gets or sets 空勤總隊
        /// </summary>
        [Display(Name = "空勤總隊")]
        public int? AIRDUTY6 { get; set; }

        /// <summary>
        /// Gets or sets 國軍
        /// </summary>
        [Display(Name = "國軍")]
        public int? COUNTRYARMY7 { get; set; }

        /// <summary>
        /// Gets or sets 空勤總隊
        /// </summary>
        [Display(Name = "空勤總隊")]
        public int? AIRDUTY7 { get; set; }

        /// <summary>
        /// Gets or sets 執行日期
        /// </summary>
        public string INPUTDATE_TEXT { get; set; }
    }
}
