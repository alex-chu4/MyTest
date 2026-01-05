
///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA20401Dto.cs
//  程式名稱：
//  各機關最新填報狀況
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-05-14       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  各機關最新填報狀況，時所使用Dto
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.ERA
{
    public class ERA20401Dto
    {
        /// <summary>
        /// Gets or sets 應變中心代碼
        /// </summary>
        public string EOC_ID { get; set; }

        /// <summary>
        /// Gets or sets 專案代號
        /// </summary>
        public long P_PRJ_NO { get; set; }

        /// <summary>
        /// Gets or sets  近 N 小時
        /// </summary>
        public int P_HOUR { get; set; }

        /// <summary>
        /// Gets or sets 填報單位
        /// </summary>
        public string ORG_NAME { get; set; }

        /// <summary>
        /// Gets or sets 未填報項目數
        /// </summary>
        public int NOT_RPT_CNT { get; set; }

        /// <summary>
        /// Gets or sets 未填報項目
        /// </summary>
        public string NOT_RPT_CODE { get; set; }
    }
}
