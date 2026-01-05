using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020302
{
    public class EDD2_020302_MDto
    {
        /// <summary>
        ///  稽催作業代碼
        /// </summary>
        [DisplayName("稽催作業代碼")]
        public string AUDITING_ID { get; set; }

        /// <summary>
        ///  單位代碼
        /// </summary>
        [DisplayName("單位代碼")]
        public string UNIT_ID { get; set; }

        /// <summary>
        ///  稽催作業
        /// </summary>
        [DisplayName("稽催作業")]
        public string OPERATION_NAME { get; set; }

        /// <summary>
        ///  稽催作業
        /// </summary>
        [DisplayName("稽催作業內容")]
        public string OPERATION_CONTENT { get; set; }

        /// <summary>
        ///  稽催排程
        /// </summary>
        [DisplayName("稽催排程")]
        public string CYCLE_TIME { get; set; }

        /// <summary>
        ///  有效期間
        /// </summary>
        [DisplayName("有效期間")]
        public string PERIOD_DATE { get; set; }

        /// <summary>
        ///  縣市別
        /// </summary>
        [DisplayName("稽催日期")]
        public string SEND_DATE { get; set; }

        /// <summary>
        ///  縣市別
        /// </summary>
        [DisplayName("填報期限")]
        public string COMPLETED_DATE { get; set; }

        /// <summary>
        ///  縣市別
        /// </summary>
        [DisplayName("受稽催單位數")]
        public int? ALL_UNIT_CNT { get; set; }

        /// <summary>
        ///  已填報單位
        /// </summary>
        [DisplayName("已填報單位")]
        public int? YES_UNIT_CNT { get; set; }

        /// <summary>
        ///  未填報單位
        /// </summary>
        [DisplayName("未填報單位")]
        public int? NO_UNIT_CNT { get; set; }

        public int? DELAY_UNIT_CNT { get; set; }

        /// <summary>
        ///  縣市別
        /// </summary>
        [DisplayName("縣市別")]
        public string RECEIVE_STATUS { get; set; }
    }
}
