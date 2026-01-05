///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  IEDD2020403Dao.cs
//  程式名稱：
//  救災資源查詢與統計
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-10-02       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  救災資源查詢與統計
///////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using EMIC2.Models.Dao.Dto.EDD2.EDD2020403;

namespace EMIC2.Models.Interface.EDD2.EDD2020403
{
    public interface IEDD2020403Dao
    {
        /// <summary>
        ///  救災資源查詢與統計取得資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns>List<EDD2020403Dto></returns>
        List<EDD2020403Dto> EDD2_020403_M(EDD2020403Dto data);

        /// <summary>
        /// 功能說明：取得保管場所地點資料
        /// </summary>
        /// <param name="data"> SearchModelDto: 查詢使用Dto (預設)
        /// </param>
        /// <returns>
        /// ResultModelDto
        /// </returns>
        /// 開發人員            日期           異動內容                    解決的問題
        /// Joe             2019-10-09        新增此功能                  保管場所地點資料
        List<EDD2020403Dto> EDD2_020403_LOCATION(EDD2020403Dto data);

        /// <summary>
        ///  詳細資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns>EDD2020403Dto</returns>
        EDD2020403Dto GetDeatilData(EDD2020403Dto data);
    }
}
