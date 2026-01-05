
///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  IERA20403Dao.cs
//  程式名稱：
//  處置報告最新填報狀況SPEC介面
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-06-26       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  處置報告最新填報狀況SPEC介面
///////////////////////////////////////////////////////////////////////////////////////
using EMIC2.Models.Dao.Dto.ERA.ERA20403;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.ERA2.ERA20403
{
    public interface IERA20403Dao
    {
        /// <summary>
        /// 處置報告最新填報狀況SPEC資訊，呼叫 Stored Procedure 回傳資料
        /// </summary>
        /// <returns> IEnumerable<ERA20401Dto></returns>
        IEnumerable<ERA20403Dto> GetData(ERA20403Dto data);
    }
}
