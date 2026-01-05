///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2Dto.cs
//  程式名稱：
//  ERA20405
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-07-23       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA20405
///////////////////////////////////////////////////////////////////////////////////////
using System;

namespace EMIC2.Models.Dao.Dto.ERA.ERA20405
{
    public class ERA20405 : ERA2Dto
    {
        /// <summary>
        /// Gets or sets 狀態
        /// D0=未搶通 P0=封閉 D1 = 搶通 P1=解除封閉
        /// </summary>
        public string TRFSTATUS { get; set; }

        /// <summary>
        /// Gets or sets 道路分類代碼
        /// 0=國道 1=省道 2=代養縣道 3=自養縣道 5=鄉道 6=市區道路 7=農路 8=原住民族地區部落主要聯外道路
        /// </summary>
        public string ROADTYPE_ID { get; set; }

        public string ROADTYPE_NAME { get; set; }

        /// <summary>
        /// Gets or sets 道路代碼
        /// </summary>
        public int? ROADID { get; set; }

        /// <summary>
        /// Gets or sets 路段起始里程
        /// </summary>
        public int? STARTMILE { get; set; }

        /// <summary>
        /// Gets or sets 路段起始(+)
        /// </summary>
        public int? STARTPLUS { get; set; }

        /// <summary>
        /// Gets or sets 路段結束里程
        /// </summary>
        public int? ENDMILE { get; set; }

        /// <summary>
        /// Gets or sets 路段結束(+)
        /// </summary>
        public int? ENDPLUS { get; set; }

        /// <summary>
        /// Gets or sets 路線樁號/路名
        /// </summary>
        public string LINECODE { get; set; }

        /// <summary>
        /// Gets or sets 附近地名
        /// </summary>
        public string ADDNAME { get; set; }

        /// <summary>
        /// Gets or sets 災害地點(WGS84)_經度
        /// </summary>
        public string WGS84_X { get; set; }

        /// <summary>
        /// Gets or sets 災害地點(WGS84)_緯度
        /// </summary>
        public string WGS84_Y { get; set; }

        /// <summary>
        /// Gets or sets 前管制點(WGS84)_經度
        /// </summary>
        public string WGS84_X_SCTL { get; set; }

        /// <summary>
        /// Gets or sets 前管制點(WGS84)_緯度
        /// </summary>
        public string WGS84_Y_SCTL { get; set; }

        /// <summary>
        /// Gets or sets 後管制點(WGS84)_經度
        /// </summary>
        public string WGS84_X_ECTL { get; set; }

        /// <summary>
        /// Gets or sets 後管制點(WGS84)_緯度
        /// </summary>
        public string WGS84_Y_ECTL { get; set; }

        /// <summary>
        /// Gets or sets 交通阻斷原因類別_類別
        /// </summary>
        public string CLOSE_TYPE { get; set; }

        /// <summary>
        /// Gets or sets 交通阻斷原因類別_原因
        /// </summary>
        public string CLOSE_TSUB { get; set; }

        /// <summary>
        /// Gets or sets 阻斷封閉時間
        /// </summary>
        public DateTime? CLOSE_DATETIME { get; set; }

        /// <summary>
        /// Gets or sets 阻斷封閉時間
        /// </summary>
        public string CLOSE_DATETIME_TEXT { get; set; }

        /// <summary>
        /// Gets or sets 預估搶修工作天
        /// </summary>
        public int? REPAIRE_DAY { get; set; }

        /// <summary>
        /// Gets or sets 受損狀況
        /// </summary>
        public string CLOSE_TEXT { get; set; }

        /// <summary>
        /// Gets or sets 搶修中作為
        /// </summary>
        public string REPAIRE_TEXT { get; set; }

        /// <summary>
        /// Gets or sets 替代道路
        /// </summary>
        public string ALT_ROUTE { get; set; }

        /// <summary>
        /// Gets or sets 復建及搶修概估經費(千元)
        /// </summary>
        public int? BUILD_COST { get; set; }

        /// <summary>
        /// Gets or sets 交管措施
        /// </summary>
        public string OPEN_TEXT { get; set; }

        /// <summary>
        /// Gets or sets 實際搶通(開放)時間
        /// </summary>
        public DateTime? OPEN_DATETIME { get; set; }

        /// <summary>
        /// Gets or sets 備註
        /// </summary>
        public string REMARK { get; set; }

        /// <summary>
        /// Gets or sets 封橋與否 預設N (目前未使用) N=否 Y=是
        /// </summary>
        public string BRG_CLOSE { get; set; }

        /// <summary>
        /// Gets or sets 災情編號
        /// </summary>
        public string CASE_NO { get; set; }

        /// <summary>
        /// Gets or sets 資料來源
        /// </summary>
        public string DATA_SOURCE { get; set; }

        // ----------------------------------------------------------

        public int CODE_ID { get; set; }

        public string PARENT_CODE_VALUE { get; set; }

        public string CODE_USEEN { get; set; }

        public string CODE_USECH { get; set; }

        public string CODE_NAME { get; set; }

        public string CODE_DETAIL { get; set; }

        public string CODE_VALUE { get; set; }

        public string CODE_MEMO { get; set; }

        public short? CODE_LEVEL { get; set; }

        public short? CODE_STATUS { get; set; }

        // ----------------------------------------------------------

        /// <summary>
        /// Gets or sets 通報表明細檔序號
        /// </summary>
        public int? REPORT_DETAIL { get; set; }

        /// <summary>
        /// Gets or sets 通報表主檔序號
        /// </summary>
        public int? REPORT_SEQ { get; set; }

        /// <summary>
        /// Gets or sets 筆數編號
        /// </summary>
        public int? REPORT_ROWSEQ { get; set; }

        /// <summary>
        /// Gets or sets 鄉鎮名稱
        /// </summary>
        public string AREACODE { get; set; }

        /// <summary>
        /// Gets or sets 災害地點(WGS84)_經度
        /// </summary>
        public string WGS84_Long { get; set; }

        /// <summary>
        /// Gets or sets 災害地點(WGS84)_緯度
        /// </summary>
        public string WGS84_La { get; set; }

        /// <summary>
        /// Gets or sets 前管制點(WGS84)_經度
        /// </summary>
        public string CONTROL_S_LONG { get; set; }

        /// <summary>
        /// Gets or sets 前管制點(WGS84)_緯度
        /// </summary>
        public string CONTROL_S_LA { get; set; }

        /// <summary>
        /// Gets or sets 後管制點(WGS84)_經度
        /// </summary>
        public string CONTROL_E_LONG { get; set; }

        /// <summary>
        /// Gets or sets 後管制點(WGS84)_緯度
        /// </summary>
        public string CONTROL_E_LA { get; set; }

        /// <summary>
        /// Gets or sets 預估搶修工作天
        /// </summary>
        public string REPAIRE_DATETIME { get; set; }

        /// <summary>
        /// Gets or sets 替代道路
        /// </summary>
        public string CTRLMODE { get; set; }

        /// <summary>
        /// Gets or sets 復建及搶修概估經費(千元)
        /// </summary>
        public int? BUILD_MONE { get; set; }
    }
}
