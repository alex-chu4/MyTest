///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  IEDD2020602Dao.cs
//  程式名稱：
//  救災資源群組及分類查詢API
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-10-22       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  救災資源群組及分類查詢API
///////////////////////////////////////////////////////////////////////////////////////
using EMIC2.Models.Dao.Dto.EDD2.EDD2020602;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.EDD2.EDD2020602
{
    public interface IEDD2020602Dao
    {
        /// <summary>
        /// 救災資源群組及分類查詢
        /// </summary>
        /// <returns>List<EDD2020602Dto></returns>
        List<EDD2020602Dto> DisasterReliefResourceGroupAndClassification();
    }
}
