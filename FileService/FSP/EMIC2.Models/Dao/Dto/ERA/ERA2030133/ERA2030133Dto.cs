///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030133Dto.cs
//  程式名稱：
//  ERA2030133Dto
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-30       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2030133Dto
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.ERA
{
    public class ERA2030133Dto : ERA2Dto
    {
        /// <summary>
        /// Gets or sets 路線樁號
        /// </summary>
        public string LINECODE { get; set; }

        /// <summary>
        /// Gets or sets 附近地名
        /// </summary>
        public string ADDNAME { get; set; }

        /// <summary>
        /// Gets or sets 受損狀況
        /// </summary>
        public string CLOSE_TEXT { get; set; }

        /// <summary>
        /// Gets or sets 交通阻斷日期時間
        /// </summary>
        public DateTime? CLOSE_DATETIME { get; set; }

        /// <summary>
        /// Gets or sets 交通阻斷日期時間時
        /// </summary>
        public int? CLOSE_DATETIME_HOUR { get; set; }

        /// <summary>
        /// Gets or sets 交通阻斷日期時間分
        /// </summary>
        public int? CLOSE_DATETIME_MINUTE { get; set; }

        /// <summary>
        /// Gets or sets 交通阻斷日期時間
        /// </summary>
        public string CLOSE_DATETIME_TEXT { get; set; }

        /// <summary>
        /// Gets or sets 預計搶通日
        /// </summary>
        public int? REPAIRE_DAY { get; set; }

        /// <summary>
        /// Gets or sets 預計搶通日期時間
        /// </summary>
        public DateTime? REPAIRE_DATETIME { get; set; }

        /// <summary>
        /// Gets or sets 預計搶通日期時間時
        /// </summary>
        public int? REPAIRE_DATETIME_HOUR { get; set; }

        /// <summary>
        /// Gets or sets 預計搶通日期時間分
        /// </summary>
        public int? REPAIRE_DATETIME_MINUTE { get; set; }

        /// <summary>
        /// Gets or sets 預計搶通日期時間
        /// </summary>
        public string REPAIRE_DATETIME_TEXT { get; set; }

        /// <summary>
        /// Gets or sets 替代道路
        /// </summary>
        public string ALT_ROUTE { get; set; }

        /// <summary>
        /// Gets or sets 備註
        /// </summary>
        public string REMARK { get; set; }
    }
}
