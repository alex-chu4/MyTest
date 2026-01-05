
///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  IERA20401Dao.cs
//  程式名稱：
//  各機關最新填報狀況介面
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-04-26       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  各機關最新填報狀況，時所使用Dao介面
///////////////////////////////////////////////////////////////////////////////////////
using EMIC2.Models.Dao.Dto.ERA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.ERA
{
    public interface IERA20401Dao
    {
        /// <summary>
        /// 各機關最新填報狀況資訊，呼叫 Stored Procedure 回傳資料
        /// </summary>
        /// <returns> IEnumerable<ERA20401Dto></returns>
        IEnumerable<ERA20401Dto> GetData(ERA20401Dto data);
    }
}
