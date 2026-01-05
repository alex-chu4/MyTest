///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  SSO2020705Dto.cs
//  程式名稱：
//  帳號權限紀錄資料傳輸物件
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期             版本        備註
//  Vivian Chu      2019-05-27          1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供[帳號權限紀錄]傳輸物件的地方。 
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Dao.Dto.SSO2
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SSO2020705Dto
    {
        /// <summary>
        /// 序號
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 時間
        /// </summary>
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        public DateTime CREATED_TIME { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        public string USER_ID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string USER_NAME { get; set; }

        /// <summary>
        /// 機關
        /// </summary>
        public string COMPANY { get; set; }

        /// <summary>
        /// 應用系統
        /// </summary>
        public string SYS_CODE { get; set; }
        public string SYS_NAME { get; set; }

        /// <summary>
        /// 權限(群組\角色)
        /// GROUP_NAME\AUTH_NAME 之中擇一
        /// </summary>
        public string AUTH_NAME { get; set; }
    }
}
