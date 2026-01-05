///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  JsonService.cs
//  程式名稱：
//  Json操作元件
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期             版本        備註
//  小柯             2019/03/14      1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  Json操控元件。
///////////////////////////////////////////////////////////////////////////////////////
using Newtonsoft.Json;

namespace Utility.Json
{
    /// <summary>
    /// Json操控元件
    /// </summary>
    public static class JsonService
    {
        /// <summary>
        /// JSON字串 轉 物件
        /// </summary>
        /// <typeparam name="T">要轉出的物件</typeparam>
        /// <param name="jsonString">要轉化的字串</param>
        /// <returns>回傳指定物件</returns>
        public static T ConvertJsonStringToObject<T>(string jsonString)
        {
            if (jsonString == null)
                return default(T);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// 物件 轉 JSON字串
        /// </summary>
        /// <param name="obj">物件</param>
        /// <param name="Indented">預設: true = 美化格式</param>
        /// <returns>回傳字串</returns>
        public static string ConvertObjectToJsonString(object obj, bool Indented = true)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            if (Indented)
                return JsonConvert.SerializeObject(obj, Formatting.Indented);
            return JsonConvert.SerializeObject(obj);
        }
    }
}
