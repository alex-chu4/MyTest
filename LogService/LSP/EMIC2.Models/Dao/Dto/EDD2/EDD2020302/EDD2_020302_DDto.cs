using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020302
{
    public class EDD2_020302_DDto
    {
        /// <summary>
        ///  單位名稱
        /// </summary>
        [DisplayName("單位名稱")]
        public string UNIT_NAME { get; set; }

        /// <summary>
        ///  填報狀態
        /// </summary>
        [DisplayName("填報狀態")]
        public string CHECK_DESCR { get; set; }

        /// <summary>
        ///  逾期天數
        /// </summary>
        [DisplayName("逾期天數")]
        public string CHECK_DATE_DESCR { get; set; }

        /// <summary>
        ///  聯絡人
        /// </summary>
        [DisplayName("聯絡人")]
        public string CONTACT_NAME { get; set; }

        /// <summary>
        ///  連絡電話
        /// </summary>
        [DisplayName("連絡電話")]
        public string CONTACT_TEL { get; set; }

        /// <summary>
        ///  填報日
        /// </summary>
        [DisplayName("填報日")]
        public string CHECK_DATE { get; set; }

        /// <summary>
        ///  填報狀態代碼
        /// </summary>
        [DisplayName("填報狀態代碼")]
        public string CHECK_STATUS { get; set; }

        /// <summary>
        ///  逾期狀態代碼
        /// </summary>
        [DisplayName("逾期狀態代碼")]
        public string DELAY_STATUS { get; set; }
    }
}
