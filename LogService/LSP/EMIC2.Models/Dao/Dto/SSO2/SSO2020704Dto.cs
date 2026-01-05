///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  SSO2020704Dto.cs
//  程式名稱：
//  帳號狀態統計資料傳輸物件
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		   日期             版本        備註
//  Vivian Chu      2019-05-28          1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供[帳號狀態統計紀錄]傳輸物件的地方。 
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Dao.Dto.SSO2
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SSO2020704Dto
    {
        /// <summary>
        /// 序號
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 帳號類別
        /// </summary>
        public string ACCOUNT_TYPE_NAME { get; set; }

        /// <summary>
        /// 啟用帳號數量
        /// </summary>
        public int A { get; set; }

        /// <summary>
        /// 停用帳號數量
        /// </summary>
        public int T { get; set; }

        /// <summary>
        /// 鎖定次數
        /// </summary>
        public int L { get; set; }
    }
}
