using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020202
{
    public class EDD2020202Dto
    {
        /// <summary>
        /// Gets or sets 稽催填報流水號
        /// </summary>
        [DisplayName("稽催填報流水號")]
        public int AUDITING_RECORD_ID { get; set; }

        /// <summary>
        /// Gets or sets 單位名稱
        /// </summary>
        [DisplayName("單位名稱")]
        public string UNIT_NAME { get; set; }

        /// <summary>
        /// Gets or sets 稽催作業
        /// </summary>
        [DisplayName("稽催作業")]
        public string OPERATION_NAME { get; set; }

        /// <summary>
        /// Gets or sets 稽催作業內容
        /// </summary>
        [DisplayName("稽催作業內容")]
        public string OPERATION_CONTENT { get; set; }

        /// <summary>
        /// Gets or sets 稽催日期
        /// </summary>
        [DisplayName("稽催日期")]
        public DateTime? SEND_DATE { get; set; }

        /// <summary>
        /// Gets or sets 填報日期
        /// </summary>
        [DisplayName("填報日期")]
        public DateTime? COMPLETED_DATE { get; set; }

        /// <summary>
        /// Gets or sets 填報狀態
        /// </summary>
        [DisplayName("填報狀態")]
        public string CHECK_DESCR { get; set; }
    }
}
