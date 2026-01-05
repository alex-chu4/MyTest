///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  IERA20502Dao.cs
//  程式名稱：
//  查詢未完成的處置報告項目
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-20       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  道路通阻案件
///////////////////////////////////////////////////////////////////////////////////////
using EMIC2.Models.Dao.Dto.ERA.ERA20502;
using EMIC2.Models.Dao.ERA.ERA20502;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.ERA2.ERA20502
{
    public interface IERA20502Dao
    {
        /// <summary>
        /// 專案查詢 道路通阻案件查詢
        /// </summary>
        /// <returns>List<ERA2Dto></returns>
        List<ERA20502Dto> ERA2_0502_M(ERA20502SearchModelDto data);
    }
}
