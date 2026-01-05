///////////////////////////////////////////////////////////////////////////////////////
//  程式名稱：IEDD2020503Dao.cs
//  資源項目分類Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員      日期       方法名稱          版本       功能說明
//  Joe        2019-09-23  資源項目維護Dao    1.0.0     資源項目分類Dao
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
///////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using EMIC2.Models.Dao.Dto.EDD2;
using EMIC2.Models.Dao.Dto.EDD2.EDD2020503;

namespace EMIC2.Models.Interface.EDD2.EDD2020503
{
    public interface IEDD2020503Dao
    {
        /// <summary>
        /// 功能說明：查詢資料
        /// </summary>
        /// <param name="data">查詢條件</param>
        /// <returns>List<RESOURCE_ITEM_ALLDto></returns>
        /// 開發人員            日期           異動內容                    解決的問題
        /// Joe             2019-10-23       新增此功能                     查詢資料
        List<RESOURCE_ITEM_ALLDto> EDD2_RESOURCE_ITEM_ALL(EDD2020503Dto data);
    }
}