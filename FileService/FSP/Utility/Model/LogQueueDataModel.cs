///////////////////////////////////////////////////////////////////////////////////////
//  程式名稱：
//  程式描述：Log Queue Data Model
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本       備註
//  David             2019-11-09       0.0.1.0     初始版本
//  David             2019-11-14       0.0.2.0     新增ActionName
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
///////////////////////////////////////////////////////////////////////////////////////
namespace Utility.Model
{
    public class LogQueueDataModel
    {
        /// <summary>
        /// Log發生時間
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 處理狀態
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 系統代號
        /// </summary>
        public string SysCode { get; set; }

        /// <summary>
        /// 功能代號
        /// </summary>
        public string FunctionCode { get; set; }

        /// <summary>
        /// 動作名稱
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public int OpType { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 註記
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 使用者IP
        /// </summary>
        public string ClientIP { get; set; }

        /// <summary>
        /// 主機IP
        /// </summary>
        public string ServerIP { get; set; }

        /// <summary>
        /// 操作使用者
        /// </summary>
        public string CreateUser { get; set; }
    }

    /// <summary>
    /// OPTYPE.
    /// </summary>
    public enum ENUM_OPTYPE
    {
        PAGELOAD = 0,   // 頁面載入
        FIND = 10,      // 查詢
        CREATE = 11,    // 新增
        UPDATE = 12,    // 編輯
        DELETE = 13,    // 刪除
        UPLOAD = 14,    // 上傳
        EXPORT = 15,    // 匯出
        ASSIGN = 30,    // 指派
        REPLAY = 60,    // 回覆
        OTHERS = 99,    // 其他
    }
}
