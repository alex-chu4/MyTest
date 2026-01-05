///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ISSO2Dao.cs
//  程式名稱：
//  帳號相關資料查詢
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期             版本        備註
//  Vivian Chu      2019-06-05          1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供[帳號相關資料]資料庫SQL查詢指令。 
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Interface.SSO2
{
    using EMIC2.Models.Dao.Dto.SSO2;
    using System;
    using System.Collections.Generic;

    public interface ISSO2Dao
    {
        string GetUserRoles(string userId);

        string GetGroupCodes(string userId);

        string GetOrgIDFromOID(string oid);

        IEnumerable<SSO2020701Dto> GetLoginDataList(DateTime startTime, DateTime endTime, string accountType, string accessCode);

        IEnumerable<SSO2020703Dto> GetStatusDataList(DateTime startTime, DateTime endTime, string accountType, string status);

        IEnumerable<SSO2020705Dto> GetAuthDataList(DateTime startDate, DateTime endDate, string sysCode);

        IEnumerable<SSO2020707Dto> GetLongUnLoginDataList(int dateAgo, string accountType);

        IEnumerable<SSO2020702Dto> GetLoginStaDataList(DateTime startTime, DateTime endTime);

        IEnumerable<SSO2020704Dto> GetStatusStaDataList(DateTime startTime, DateTime endTime);

        IEnumerable<SSO2020706Dto> GetAuthStaDataList(DateTime startTime, DateTime endTime);

        IEnumerable<SSO2020708Dto> GeOnLineDataList(int timeAgo, string accountType);
    }
}
