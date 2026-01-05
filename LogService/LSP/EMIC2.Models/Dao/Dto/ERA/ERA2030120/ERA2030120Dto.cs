///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030108Dto.cs
//  程式名稱：
//  ERA2030108Dto
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-28       1.0.0.0     初始版本
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
    public class ERA2030120Dto : ERA2Dto
    {
        /// <summary>
        /// Gets or sets 資料序號
        /// </summary>
        public int SEQ_ID { get; set; }

        /// <summary>
        /// Gets or sets 明細檔序號
        /// </summary>
        public int DISP_DETAIL_ID { get; set; }

        /// <summary>
        /// Gets or sets 停駛時間
        /// </summary>
        public DateTime? STOP_TIME { get; set; }

        /// <summary>
        /// Gets or sets 停駛時間
        /// </summary>
        public string sHH { get; set; }

        /// <summary>
        /// Gets or sets 停駛時間
        /// </summary>
        public string sMM { get; set; }

        /// <summary>
        /// Gets or sets 停駛時間
        /// </summary>
        public string STOP_TIMEtoString { get; set; }

        /// <summary>
        /// Gets or sets 線別
        /// </summary>
        public string LINE { get; set; }

        /// <summary>
        /// Gets or sets 班次
        /// </summary>
        public string SERVICE { get; set; }

        /// <summary>
        /// Gets or sets 原因
        /// </summary>
        public string REASON { get; set; }

        /// <summary>
        /// Gets or sets 備註
        /// </summary>
        public string REMARK { get; set; }
    }
}
