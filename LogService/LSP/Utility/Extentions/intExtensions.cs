///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  IntExtenstions.cs
//  程式名稱：
//  int擴充方法
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期             版本        備註
//  小柯                2019-03-13       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  int擴充方法。
///////////////////////////////////////////////////////////////////////////////////////

namespace Utility.Library.Extentions
{
    /// <summary>
    /// int 擴充方法
    /// </summary>
     public static class IntExtensions
    {
        /// <summary>
        /// 數字轉布林
        /// </summary>
        /// <param name="n">0 或 1</param>
        /// <returns>1 = true ， 0 = false</returns>
        public static bool ToBool(this int n)
        {
            return n == 1 ? true : false;
        }

        /// <summary>
        /// 數字轉Enum的值
        /// </summary>
        /// <typeparam name="T">要轉換的型別</typeparam>
        /// <param name="n">指定第N向</param>
        /// <returns>返回指定的Enum字串</returns>
        public static string ToEnumString<T>(this int n)
        {
            return ((T)(object)n).ToString();
        }
    }
}
