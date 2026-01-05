///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  SSO2020703Dto.cs
//  程式名稱：
//  帳號狀態紀錄資料傳輸物件
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		   日期             版本        備註
//  Vivian Chu      2019-05-28          1.0.0.0     初始版本
//  Vivian Chu      2019-06-05          1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供[帳號狀態紀錄]傳輸物件的地方。 
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Dao.Dto.SSO2
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SSO2020703Dto
    {
        /// <summary>
        /// 序號
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        public string USER_ID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string USER_NAME { get; set; }

        /// <summary>
        /// 機關單位
        /// </summary>
        public string COMPANY { get; set; }

        /// <summary>
        /// 帳號類別
        /// </summary>
        public string ACCOUNT_TYPE { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public string STATUS { get; set; }

        /// <summary>
        /// 修改時間
        /// </summary>
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        public DateTime MODIFIED_TIME { get; set; }

        public string MODIFIED_TIME_TXT { get; set; }
    }
}
