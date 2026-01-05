///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  IFIS2Dao.cs
//  程式名稱：
//  目前撤離區域-查詢
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員           日期             版本           備註
//  Vivian Chu      2019-09-06         1.0.0.0       初始版本
//  Vivian Chu      2019-09-17         2.0.0.0       初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供[目前撤離區域]資料庫SQL查詢指令。 
///////////////////////////////////////////////////////////////////////////////////////
///
namespace EMIC2.Models.Interface.FIS2
{
    using EMIC2.Models.Dao.Dto.FIS2;
    using System.Collections.Generic;

    public interface IFIS2Dao
    {
        /// <summary>
        /// 取得撤離區域列表
        /// </summary>
        /// <param name="cityName"></param>
        /// <param name="townName"></param>
        /// <returns></returns>
        IEnumerable<FIS2010106Dto> GetDataList(string cityName, string townName);

        /// <summary>
        /// 取得[我正在找的人]-[設定自動尋人]列表
        /// </summary>
        /// <param name="personUid">登入者序號(尋人者)</param>
        /// <param name="startupSeqno">系統啟用序號</param>
        /// <returns></returns>
        IEnumerable<FIS2010105Dto> GetLookForDataList(string personUid, string startupSeqno);

        /// <summary>
        /// 建立自動尋人資料
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        bool Create(FIS2_LOOKFOR dto);
    }
}
