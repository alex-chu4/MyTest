
///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  IERA2020141Dao.cs
//  程式名稱：
//  A3通報表下層匯整介面
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-04-26       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  A3通報表下層匯整，時所使用Dto
///////////////////////////////////////////////////////////////////////////////////////
using EMIC2.Models.Dao.Dto.ERA;
using EMIC2.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.ERA
{
    public interface IERA2020141Dao
    {
        /// <summary>
        /// 匯入下層資訊，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        IResult LowerLevelCity(ERA2020141Dto data);
    }
}
