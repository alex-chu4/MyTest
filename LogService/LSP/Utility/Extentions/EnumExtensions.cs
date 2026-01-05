///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  EnumExtensions.cs
//  程式名稱：
//  Enum擴充方法
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期             版本        備註
//  小柯                2019-03-13       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  Enum擴充方法。
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Utility.Extentions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 取得 ENUM Value 並將 int 轉成字串
        /// </summary>
        /// <param name="input">Enum項目</param>
        /// <returns>返回Enum項目</returns>
        public static string ToIntString(this Enum input)
        {
            FieldInfo fi = input.GetType().GetField(input.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            // 先取 Description，沒有則改取 value
            if ((attributes != null) && (attributes.Length > 0))
            {
                return attributes[0].Description;
            }
            else
            {
                return input.ToString();
            }
        }

        /// <summary>
        /// 輸入參數取得敘述
        /// </summary>
        /// <typeparam name="T">ENUM</typeparam>
        /// <param name="input">輸入參數</param>
        /// <returns>敘述字串</returns>
        public static string GetDesscription<T>(string input)
        {
            var enumName = typeof(T).GetEnumName(Convert.ToInt32(input));
            DescriptionAttribute descriptionAttrubite = (DescriptionAttribute)typeof(T)
                .GetFields().Where(o => o.Name == enumName).FirstOrDefault()
                .GetCustomAttributes(typeof(DescriptionAttribute), false)[0];

            return descriptionAttrubite.Description;
        }

        /// <summary>
        /// 輸入參數取得敘述
        /// </summary>
        /// <typeparam name="T">ENUM</typeparam>
        /// <param name="input">輸入參數</param>
        /// <returns>敘述字串</returns>
        public static string GetDesscription<T>(int input)
        {
            var enumName = typeof(T).GetEnumName(input);
            DescriptionAttribute descriptionAttrubite = (DescriptionAttribute)typeof(T)
                .GetFields().Where(o => o.Name == enumName).FirstOrDefault()
                .GetCustomAttributes(typeof(DescriptionAttribute), false)[0];

            return descriptionAttrubite.Description;
        }

        /// <summary>
        /// 提供給ToSelectListItem使用 Private Method
        /// </summary>
        /// <typeparam name="T">TEnumType</typeparam>
        /// <param name="e">Enum</param>
        /// <returns>string</returns>
        private static string GetDescription<T>(this T e)
            where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 將傳入Enum物件轉型為SelectListItem
        /// </summary>
        /// <typeparam name="T">TEnumType</typeparam>
        /// <param name="value">Enum</param>
        /// <returns>泛型集合</returns>
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this T value)
            where T : IConvertible
        {
            Type type = value.GetType();
            return (Enum.GetValues(type) as T[])
                .Select(s => new SelectListItem
                {
                    Value = s.ToString(),
                    Text = s.GetDescription()
                });
        }
    }
}
