///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  SSO2020704Dto.cs
//  程式名稱：
//  帳號權限統計資料傳輸物件
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		   日期             版本        備註
//  Vivian Chu      2019-05-29          1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供[帳號權限統計紀錄]傳輸物件的地方。 
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Dao.Dto.SSO2
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SSO2020706Dto
    {
        /// <summary>
        /// 序號
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 應用系統
        /// </summary>
        public string SYS_NAME { get; set; }
        /// <summary>
        /// 權限(群組/角色)
        /// </summary>
        public string GR_NAME { get; set; }
        public string GROUP_NAME { get; set; }
        public string ROLE_NAME { get; set; }
        /// <summary>
        /// 一般帳號
        /// </summary>
        public string GEN { get; set; }
        /// <summary>
        /// 機關帳號
        /// </summary>
        public string ORG { get; set; }
        /// <summary>
        /// 救災資源帳號
        /// </summary>
        public string EDD { get; set; }
        /// <summary>
        /// 舊系統帳號(egov)
        /// </summary>
        public string EGOV { get; set; }
    }
}
