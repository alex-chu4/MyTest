///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  CommonEEM2Dao.cs
//  程式名稱：
//  執行SQL User Defined Function EEM2_QRY_EOC_PRJ
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期              版本              備註
//  Enosh           2019-08-26       1.0.0.0           初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  切換應變中心與專案時須使用到的功能
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Interface.COMMON
{
    using System.Collections.Generic;

    using EMIC2.Models.Dao.Dto.COMMON;

    public interface ICommonEEM2Dao
    {
        IEnumerable<EEM2_QRY_EOC_PRJ_Dto> Get_EEM2_QRY_EOC_PRJ(string orgId);

        int Get_EEM2_QRY_WORK_NOT_REPLY_COUNT(OrgEocPrjDto dto);
    }
}
