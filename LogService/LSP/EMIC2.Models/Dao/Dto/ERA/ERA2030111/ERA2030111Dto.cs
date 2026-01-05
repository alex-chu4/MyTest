///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030111Dto.cs
//  程式名稱：
//  ERA2030111Dto
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-29       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2030111Dto
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.ERA
{
    public class ERA2030111Dto : ERA2Dto
    {
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
        /// Gets or sets 安置船上人數
        /// </summary>
        [Display(Name = "安置船上人數")]
        public int? PUT_BOAT_PN { get; set; }

        /// <summary>
        /// Gets or sets 安置岸上處所人數
        /// </summary>
        [Display(Name = "安置岸上處所人數")]
        public int? PUT_LAND_PN { get; set; }
    }
}
