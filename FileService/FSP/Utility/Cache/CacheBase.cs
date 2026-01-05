///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  CacheBase.cs
//  程式名稱：
//  裝飾者模式的基底
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  小柯             2019/05/06       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  裝飾者模式的基底。
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Cache
{
    public abstract class CacheBase : ICache
    {
        protected ICache cache;
        public abstract string Get(string key);
        public abstract T Get<T>(string key);
        public abstract bool Remove(string key);
        public abstract bool Remove(string[] key);
        public abstract bool Set(string key, string value);
        public abstract bool Set(string key, string value, int expiresAtMinutes);
        public abstract bool Set(string key, object value);

        /// <summary>
        /// 新增這個方法可以附加功能
        /// </summary>
        /// <param name="cache"></param>
        public virtual void SetDecorated(ICache cache)
        {
            this.cache = cache;
        }
    }
}
