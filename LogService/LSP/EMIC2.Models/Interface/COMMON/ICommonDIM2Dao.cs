///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  CommonDIM2Dao.cs
//  程式名稱：
//  搜尋
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期              版本              備註
//  Enosh           2019-09-04       1.0.0.0           初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  待辦事項使用
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Interface.COMMON
{
    using EMIC2.Models.Dao.Dto.COMMON;

    public interface ICommonDIM2Dao
    {
        int Get_DIM2_QRY_CASE_NOT_ASSIGN_COUNT(OrgEocPrjDto dto);

        int Get_DIM2_QRY_CASE_NOT_REPLY_COUNT(OrgEocPrjDto dto);

        int Get_DIM2_QRY_TASK_NOT_REPLY_COUNT(OrgEocPrjDto dto);
    }
}
