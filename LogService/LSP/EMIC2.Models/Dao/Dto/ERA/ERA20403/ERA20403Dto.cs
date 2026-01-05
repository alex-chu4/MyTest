///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA20403Dto.cs
//  程式名稱：
//  各機關處置報告填報狀況
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-06-24       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  各機關處置報告填報狀況，時所使用Dto
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Dao.Dto.ERA.ERA20403
{
    public class ERA20403Dto
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
        public string NOT_DISP_CNT { get; set; }

        /// <summary>
        /// Gets or sets 未填報項目
        /// </summary>
        public string NOT_DISP_ITEM { get; set; }

        /// <summary>
        /// Gets or sets 機關ID
        /// </summary>
        public string ORG_ID { get; set; }

        /// <summary>
        /// Gets or sets 排序
        /// </summary>
        public string SHOW_ORDER { get; set; }
    }
}
