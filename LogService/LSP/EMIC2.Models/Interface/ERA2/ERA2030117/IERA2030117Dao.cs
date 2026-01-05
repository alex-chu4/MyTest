///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  IERA2030117Dao.cs
//  程式名稱：
//  IERA2030117Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-30       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  IERA2030117Dao
///////////////////////////////////////////////////////////////////////////////////////
using EMIC2.Models.Dao.Dto.ERA;
using EMIC2.Models.Dao.Dto.ERA.ERA2030117;
using EMIC2.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.ERA2.ERA2030117
{
    public interface IERA2030117Dao
    {
        /// <summary>
        /// 查詢通報表A2
        /// </summary>
        /// <returns></returns>
        List<ERA2030117Dto> ERA2_QRY_MAX_E1_E2_E4_J2_J3(ERA2030117SearchModelDto data);

        /// <summary>
        /// 匯入通報表，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        IResult ImportRptToDisp(ERA2030117SearchModelDto data);
    }
}
