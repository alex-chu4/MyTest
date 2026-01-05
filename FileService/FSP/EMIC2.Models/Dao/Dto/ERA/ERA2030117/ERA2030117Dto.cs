///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030117Dto.cs
//  程式名稱：
//  ERA2030117Dto
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-30       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2030117Dto
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.ERA
{
    public class ERA2030117Dto : ERA2Dto
    {
        public ERA2030117Dto()
        {
            this.REPAIR_NUM = 0;
            this.WAIT_REPAIR = 0;
        }

        /// <summary>
        /// Gets or sets 項目
        /// </summary>
        [Display(Name = "項目")]
        public string ITEM_VALUE { get; set; }

        /// <summary>
        /// Gets or sets 搶修完成
        /// </summary>
        [Display(Name = "搶修完成")]
        public int? REPAIR_NUM { get; set; }

        /// <summary>
        /// Gets or sets 尚待修復
        /// </summary>
        [Display(Name = "尚待修復")]
        public int? WAIT_REPAIR { get; set; }

        /// <summary>
        /// Gets or sets 備註
        /// </summary>
        [Display(Name = "備註")]
        public string REMARK { get; set; }

        /// <summary>
        /// Gets or sets 代碼主項
        /// </summary>
        [Display(Name = "代碼主項")]
        public string CODE_NAME { get; set; }

        /// <summary>
        /// Gets or sets 代碼內容
        /// </summary>
        [Display(Name = "代碼內容")]
        public string CODE_VALUE { get; set; }
    }
}
