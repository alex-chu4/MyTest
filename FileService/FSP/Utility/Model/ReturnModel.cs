///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ReturnModel.cs
//  程式名稱：
//  共用回傳物件
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期             版本        備註
//  小柯                2019/03/12       1.0.0.0     初始版本
//  Enosh              2019/10/15       1.0.1.0     增加ReturnModel<T>
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供給function回傳使用的共用Model。
///////////////////////////////////////////////////////////////////////////////////////
namespace Utility.Model
{
    /// <summary>
    /// 回傳使用的Model
    /// </summary>
    public class ReturnModel
    {
        /// <summary>
        /// 回傳狀態
        /// </summary>
        public bool Status { get; set; } = false;
        /// <summary>
        /// 回傳訊息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// 回傳的物件
        /// </summary>
        public object Data { get; set; } = new { };
    }

    public class ReturnModel<T>
    {
        public bool Status { get; set; } = false;

        public string Message { get; set; } = string.Empty;

        public T Data { get; set; } = default(T);
    }
}
