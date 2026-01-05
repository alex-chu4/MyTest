///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  IERA2030102Dao.cs
//  程式名稱：
//  IERA2030102Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-23       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  IERA2030102Dao
///////////////////////////////////////////////////////////////////////////////////////
using EMIC2.Models.Dao.Dto.ERA;
using EMIC2.Models.Dao.Dto.ERA.ERA2030102;
using EMIC2.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.ERA2.ERA2030102
{
    public interface IERA2030102Dao
    {
        /// <summary>
        /// 查詢通報表A2
        /// </summary>
        /// <returns></returns>
        List<ERA2030102Dto> ERA2_QRY_MAX_D3(ERA2030102SearchModelDto data);

        /// <summary>
        /// 查詢通報表A2a
        /// </summary>
        /// <returns></returns>
        List<ERA2030102Dto> ERA2_QRY_MAX_D3a(ERA2030102SearchModelDto data);

        /// <summary>
        /// 匯入通報表，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        IResult ImportRptToDisp(ERA2030102SearchModelDto data);
    }
}
