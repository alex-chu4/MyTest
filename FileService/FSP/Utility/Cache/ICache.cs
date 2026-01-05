///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ICache.cs
//  程式名稱：
//  快取介面
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  小柯             2019/05/06       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  快取介面。
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Cache
{
    public interface ICache
    {

        string Get(string key);

        T Get<T>(string key);

        bool Remove(string key);

        bool Remove(string[] key);

        bool Set(string key, string value);

        bool Set(string key, string value, int expiresAtMinutes);

        bool Set(string key, object value);
    }
}
