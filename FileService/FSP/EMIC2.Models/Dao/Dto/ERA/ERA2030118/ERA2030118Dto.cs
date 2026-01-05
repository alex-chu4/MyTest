///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030118Dto.cs
//  程式名稱：
//  ERA2030118Dto
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-29       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2030118Dto
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.ERA
{
    public class ERA2030118Dto : ERA2Dto
    {
        public ERA2030118Dto()
        {
            this.FINISH_REPAIR = 0;
            this.ON_REPAIR = 0;
            this.BLOCK_TOTAL = 0;
        }

        /// <summary>
        /// Gets or sets 道路項目
        /// </summary>
        public string ROADTYPE_NAME { get; set; }

        /// <summary>
        /// Gets or sets 已搶通（處）
        /// </summary>
        public int? FINISH_REPAIR { get; set; }

        /// <summary>
        /// Gets or sets 未搶通（處）
        /// </summary>
        public int? ON_REPAIR { get; set; }

        /// <summary>
        /// Gets or sets 備註
        /// </summary>
        public string REMARK { get; set; }

        /// <summary>
        /// Gets or sets 合計
        /// </summary>
        public int? BLOCK_TOTAL { get; set; }

        // -------------------------------------------

        /// <summary>
        /// Gets or sets 項目
        /// </summary>
        public string ITEM_VALUE { get; set; }

        /// <summary>
        /// Gets or sets 搶修完成
        /// </summary>
        public int? REPAIR_NUM { get; set; }

        /// <summary>
        /// Gets or sets 尚待修復
        /// </summary>
        public int? WAIT_REPAIR { get; set; }

        /// <summary>
        /// Gets or sets 代碼主項
        /// </summary>
        public string CODE_NAME { get; set; }

        /// <summary>
        /// Gets or sets 代碼內容
        /// </summary>
        public string CODE_VALUE { get; set; }

        /// <summary>
        /// Gets or sets 代碼子項
        /// </summary>
        public string CODE_DETAIL { get; set; }
    }
}
