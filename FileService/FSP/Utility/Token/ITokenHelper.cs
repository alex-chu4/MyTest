///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ITokenService.cs
//  程式名稱：
//  IToken共用介面
//  ITokenService 共用介面
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期             版本        備註
//  小柯                2019/03/11       1.0.0.0     初始版本
//////////////////////////////////  /////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供要製作Token的主要Interface。
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Model;

namespace Utility.Token
{
    /// <summary>
    /// 泛型限定實作IToken的物件
    /// </summary>
    /// <typeparam name="T">實作Token的類別</typeparam>
    public interface ITokenHelper
    {
        /// <summary>
        /// 建立Token
        /// </summary>
        /// <param name="payload">要製作Token的內容</param>
        /// <returns>回傳Token字串</returns>
        string EnCode(Dictionary<string, object> payload);

        /// <summary>
        /// 檢查Token
        /// </summary>
        /// <param name="token">金鑰</param>
        /// <returns>True / False</returns>
        ReturnModel Check(string token, bool bCheckTime);

        /// <summary>
        /// 將token字串轉換成物件
        /// </summary>
        /// <param name="token">token字串</param>
        /// <returns>回傳指定物件</returns>
        JwtTokenModel DeCode(string token);
    }
}
