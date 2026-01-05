///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  JWTToken.cs
//  程式名稱：
//  JWT Token元件 
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期             版本        備註
//  小柯                2019/03/11       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供EMIC SSO登入使用。
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Token;

namespace Utility.Model
{
    /// <summary>
    /// Token必備物件屬性
    /// </summary>
    public class JwtTokenModel
    {

        /// <summary>
        /// JWT Payload
        /// </summary>
        public Dictionary<string, object> Payload { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// 完整token
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// 存活時間
        /// </summary>
        public string Expire { get; set; } = string.Empty;

        /// <summary>
        /// 發行者
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// 訂閱者
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// 回傳的訊息
        /// </summary>
        public string Message { get; set; } = string.Empty;

    }
}
