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
    public class ERA2030116Dto : ERA2Dto
    {
        public ERA2030116Dto()
        {
            this.DEATH_NUM = 0;
            this.LOST_NUM = 0;
            this.HURT_NUM = 0;
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
        /// Gets or sets 地區
        /// </summary>
        [Display(Name = "地區")]
        public string TOWN_NAME { get; set; }

        /// <summary>
        /// Gets or sets 死亡人數
        /// </summary>
        [Display(Name = "死亡人數")]
        public int? DEATH_NUM { get; set; }

        /// <summary>
        /// Gets or sets 失蹤人數
        /// </summary>
        [Display(Name = "失蹤人數")]
        public int? LOST_NUM { get; set; }

        /// <summary>
        /// Gets or sets 受傷人數
        /// </summary>
        [Display(Name = "受傷人數")]
        public int? HURT_NUM { get; set; }

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
