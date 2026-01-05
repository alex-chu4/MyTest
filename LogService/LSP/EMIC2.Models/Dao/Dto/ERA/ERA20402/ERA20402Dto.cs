
///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA20402Dto.cs
//  程式名稱：
//  各機關最新填報狀況
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-05-16       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA20402Dto，時所使用Dto
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.ERA
{
    public class ERA20402Dto
    {
        /// <summary>
        /// Gets or sets 應變中心代碼，對應 ERA2_RPT_MAIN.EOC_ID
        /// </summary>
        public string EOC_ID { get; set; }

        /// <summary>
        /// Gets or sets 選單應變中心代碼
        /// </summary>
        public string P_EOC_ID { get; set; }

        /// <summary>
        /// Gets or sets 應變中心名稱，對應 ERA2_RPT_MAIN.EOC_NAME
        /// </summary>
        public string EOC_NAME { get; set; }

        /// <summary>
        /// Gets or sets 專案代號
        /// </summary>
        public int PRJ_NO { get; set; }

        /// <summary>
        /// Gets or sets 專案代號
        /// </summary>
        public DateTime? P_RPT_TIME { get; set; }

        /// <summary>
        /// Gets or sets 專案代號時
        /// </summary>
        public int? P_RPT_TIME_HOUR { get; set; }

        /// <summary>
        /// Gets or sets 專案代號時
        /// </summary>
        public int? P_RPT_TIME_MINUTE { get; set; }

        /// <summary>
        /// Gets or sets  時
        /// </summary>
        public int HOUR { get; set; }

        /// <summary>
        /// Gets or sets  分
        /// </summary>
        public int Minute { get; set; }

        /// <summary>
        /// Gets or sets  近 N 小時
        /// </summary>
        public int P_HOUR { get; set; }

        /// <summary>
        /// Gets or sets 通報表 A1a
        /// </summary>
        public string A1a { get; set; }

        /// <summary>
        /// Gets or sets 通報表 A2a
        /// </summary>
        public string A2a { get; set; }

        /// <summary>
        /// Gets or sets 通報表 A3a
        /// </summary>
        public string A3a { get; set; }

        /// <summary>
        /// Gets or sets 通報表 A4a
        /// </summary>
        public string A4a { get; set; }

        /// <summary>
        /// Gets or sets 通報表 D1a
        /// </summary>
        public string D1a { get; set; }

        /// <summary>
        /// Gets or sets 通報表 D2a
        /// </summary>
        public string D2a { get; set; }

        /// <summary>
        /// Gets or sets 通報表 D3a
        /// </summary>
        public string D3a { get; set; }

        /// <summary>
        /// Gets or sets 通報表 D4a
        /// </summary>
        public string D4a { get; set; }

        /// <summary>
        /// Gets or sets 通報表 E5a
        /// </summary>
        public string E5a { get; set; }

        /// <summary>
        /// Gets or sets 通報表 E6a
        /// </summary>
        public string E6a { get; set; }

        /// <summary>
        /// Gets or sets 通報表 E6b
        /// </summary>
        public string E6b { get; set; }

        /// <summary>
        /// Gets or sets 通報表 F1a
        /// </summary>
        public string F1a { get; set; }

        /// <summary>
        /// Gets or sets 通報表 K2a
        /// </summary>
        public string K2a { get; set; }

        /// <summary>
        /// Gets or sets 通報表 K3a
        /// </summary>
        public string K3a { get; set; }

        /// <summary>
        /// Gets or sets 排序
        /// </summary>
        public string SHOW_ORDER { get; set; }
    }
}
