///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  EEM2010102Dto.cs
//  程式名稱：
//  工作會報-指示事項回報資料傳輸物件
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		   日期              版本             備註
//  Vivian Chu      2019-07-04          1.0.0.0         初始版本
//  Vivian Chu      2019-10-21          2.0.0.0         新增EOC_ID資料
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供[指示事項回報]傳輸物件的地方。 
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Dao.Dto.EEM2
{
    using System;
    using System.Collections.Generic;

    public class EEM2010102Dto
    {
        /// <summary>
        /// 工作會報案號
        /// </summary>
        public string WORK_CODE { get; set; }

        /// <summary>
        /// 回報狀態
        /// </summary>
        public int RECORD_INDEX { get; set; }

        /// <summary>
        /// 專案序號
        /// </summary>
        public decimal PRJ_NO { get; set; }

        /// <summary>
        /// 專案名稱
        /// </summary>
        public string CASE_NAME { get; set; }

        /// <summary>
        /// 會議名稱
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 會議分類
        /// </summary>
        public byte MEETING_TYPE { get; set; }

        /// <summary>
        /// 指示事項內容
        /// </summary>
        public string WORK_ITEM { get; set; }

        /// <summary>
        /// 處理情形
        /// </summary>
        public string REPLY_PROC { get; set; }

        /// <summary>
        /// 應變中心代碼
        /// </summary>
        public string EOC_ID { get; set; }

        /// <summary>
        /// 機關單位編碼
        /// </summary>
        public string ORG_ID { get; set; }

        /// <summary>
        /// 機關單位
        /// </summary>
        public string ORG_NAME { get; set; }

        /// <summary>
        /// 回報時間
        /// </summary>
        public DateTime? REPLY_TIME { get; set; }

        public string REPLY_TIME_TEXT { get; set; }

        /// <summary>
        /// 列管狀態
        /// </summary>
        public string WORK_STATUS { get; set; }

        /// <summary>
        /// 指示事項分辦機關序號
        /// </summary>
        public string WORK_ITEM_ORG_UID { get; set; }
    }

    public class EEM2010102WORKDto
    {
        public string EOC_ID { get; set; }

        /// <summary>
        /// 專案序號
        /// </summary>
        public decimal PRJ_NO { get; set; }

        /// <summary>
        /// 專案名稱
        /// </summary>
        public string CASE_NAME { get; set; }

        /// <summary>
        /// 指揮官姓名
        /// </summary>
        //public string CMMAND_NAME { get; set; }
        public string COMMANDER { get; set; }

        /// <summary>
        /// 會議分類
        /// </summary>
        public string MEETING_TYPE { get; set; }

        /// <summary>
        /// 會議名稱
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 工作會報時間
        /// </summary>
        public DateTime WK_MEET_TIME { get; set; }

        /// <summary>
        /// 下次會報時間
        /// </summary>
        public DateTime? NEX_MEET_TIME { get; set; }

        /// <summary>
        /// 指示事項序號
        /// </summary>
        public string WORK_ITEM_UID { get; set; }

        /// <summary>
        /// 指示事項內容
        /// </summary>
        public string WORK_ITEM { get; set; }

        /// <summary>
        /// 處理情形
        /// </summary>
        public string REPLY_PROC { get; set; }

        /// <summary>
        /// 交辦單位
        /// </summary>
        public string ORG_NAME { get; set; }

        /// <summary>
        /// 交辦時間
        /// </summary>
        public string SUB_TIME { get; set; }

        /// <summary>
        /// 檔名ID
        /// </summary>
        public string FILE_ID { get; set; }

        /// <summary>
        /// 檔名:會議記錄
        /// </summary>
        public string FILE_NAME { get; set; }

        /// <summary>
        /// 會議記錄檔案建立時間
        /// </summary>
        public DateTime? CREATED_TIME { get; set; }

        /// <summary>
        /// 指示事項分辦機關序號
        /// </summary>
        public string WORK_ITEM_ORG_UID { get; set; }
    }

    public class EEM2010102REPLYDto
    {
        public decimal WORK_REPLY_UID { get; set; }

        /// <summary>
        /// 回報時間
        /// </summary>
        public DateTime? REPLY_TIME { get; set; }

        public string REPLY_TIME_TEXT { get; set; }

        /// <summary>
        /// 處理情形
        /// </summary>
        public string REPLY_PROC { get; set; }

        /// <summary>
        /// 檔名ID
        /// </summary>
        public string FILE_ID { get; set; }

        /// <summary>
        /// 檔名
        /// </summary>
        public string FILE_NAME { get; set; }
    }

    public class EEM2010102ITEMDto
    {
        /// <summary>
        /// 指示事項回報序號
        /// </summary>
        public string WORK_REPLY_UID { get; set; }
      
        /// <summary>
        /// 指示事項內容
        /// </summary>
        public string WORK_ITEM { get; set; }

        /// <summary>
        /// 交辦時間
        /// </summary>
        public DateTime? SUB_TIME { get; set; }

        public string SUB_TIME_TEXT { get; set; }

        /// <summary>
        /// 機關單位
        /// </summary>
        public string ORG_NAME { get; set; }
    }
}
