///////////////////////////////////////////////////////////////////////////////////////
//  程式名稱：IEDD2020504Dao.cs
//  資源項目維護Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員      日期       方法名稱          版本       功能說明
//  Joe        2019-09-19  資源項目維護Dao    1.0.0     資源項目維護
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
///////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using EMIC2.Models.Dao.Dto.EDD2;
using EMIC2.Models.Dao.Dto.EDD2.EDD2020504;

namespace EMIC2.Models.Interface
{
    public interface IEDD2020504Dao
    {

        /// <summary>
        /// 功能說明：查詢資料
        /// </summary>
        /// <param name="data">查詢條件</param>
        /// <returns>List<RESOURCE_ITEM_ALLDto></returns>
        /// 開發人員            日期           異動內容                    解決的問題
        /// Joe             2019-10-21       新增此功能                     查詢資料
        (int Total,List<RESOURCE_ITEM_ALLDto>) EDD2_RESOURCE_ITEM_ALL(EDD2020504Dto data);

        /// <summary>
        /// 功能說明：查詢資料單筆
        /// </summary>
        /// <param name="data">查詢條件</param>
        /// <returns>RESOURCE_ITEM_ALLDto</returns>
        /// 開發人員            日期           異動內容                    解決的問題
        /// Joe             2019-10-22       新增此功能                     查詢資料
        RESOURCE_ITEM_ALLDto EDD2_RESOURCE_ITEM(EDD2020504Dto data);
    }
}