///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030108Dto.cs
//  程式名稱：
//  ERA2030108Dto
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-23       1.0.0.0     初始版本
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
    public class ERA2030108Dto : ERA2Dto
    {
        /// <summary>
        /// Gets or sets 縣市別
        /// </summary>
        [Display(Name = "縣市別")]
        public string CITY_NAME { get; set; }

        /// <summary>
        /// Gets or sets 鄉鎮別
        /// </summary>
        [Display(Name = "鄉鎮別")]
        public string TOWN_NAME { get; set; }

        /// <summary>
        /// Gets or sets 劃定數-低窪地區
        /// </summary>
        [Display(Name = "劃定數-低窪地區")]

        public int? DELIMIT_D_NUM { get; set; }

        /// <summary>
        /// Gets or sets 劃定數-山區
        /// </summary>
        [Display(Name = "劃定數-山區")]
        public int? DELIMIT_M_NUM { get; set; }

        /// <summary>
        /// Gets or sets 劃定數-海
        /// </summary>
        [Display(Name = "劃定數-海")]
        public int? DELIMIT_S_NUM { get; set; }

        /// <summary>
        /// Gets or sets 劃定數-河
        /// </summary>
        [Display(Name = "劃定數-河")]
        public int? DELIMIT_R_NUM { get; set; }

        /// <summary>
        /// Gets or sets 劃定數-其他
        /// </summary>
        [Display(Name = "劃定數-其他")]
        public int? DELIMIT_O_NUM { get; set; }

        /// <summary>
        /// Gets or sets 劃定數-建築物
        /// </summary>
        [Display(Name = "劃定數-建築物")]
        public int? DELIMIT_B_NUM { get; set; }

        /// <summary>
        /// Gets or sets 執行情形-勸導單開立數
        /// </summary>
        [Display(Name = "執行情形-勸導單開立數")]
        public int? EC_W_NUM { get; set; }

        /// <summary>
        /// Gets or sets 執行情形-舉發單開立數
        /// </summary>
        [Display(Name = "執行情形-舉發單開立數")]
        public int? EC_P_NUM { get; set; }

        /// <summary>
        /// Gets or sets 排序
        /// </summary>
        [Display(Name = "排序")]
        public string SHOW_ORDER { get; set; }
    }
}
