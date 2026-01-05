///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  DecorateFactory.cs
//  程式名稱：
//  快取裝飾者工廠
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  小柯             2019/05/06       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  快取裝飾者工廠。
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Cache
{
    public class DecorateFacotry
    {
        ICache cache;

        public DecorateFacotry(ICache cache)
        {
            this.cache = cache;
        }

        /// <summary>
        /// 設定要裝飾的模組
        /// </summary>
        /// <param name="cache">預裝飾的模組</param>
        /// <returns></returns>
        public DecorateFacotry SetCache(CacheBase cache)
        {
            cache.SetDecorated(this.cache);
            this.cache = cache;
            return this;
        }

        public ICache GetCache()
        {
            return cache;
        }
    }
}
