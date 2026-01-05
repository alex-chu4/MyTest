///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  IERA2030107Dao.cs
//  程式名稱：
//  IERA2030107Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-23       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  IERA2030107Dao
///////////////////////////////////////////////////////////////////////////////////////
using EMIC2.Models.Dao.Dto.ERA;
using EMIC2.Models.Dao.Dto.ERA.ERA2030107;
using EMIC2.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.ERA2.ERA2030107
{
    public interface IERA2030107Dao
    {
        /// <summary>
        /// 查詢通報表A2
        /// </summary>
        /// <returns></returns>
        List<ERA2030107Dto> ERA2_QRY_EEM2_EOC_PRJ(ERA2030107SearchModelDto data);

        /// <summary>
        /// 匯入通報表，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        IResult ImportRptToDisp(ERA2030107SearchModelDto data);
    }
}
