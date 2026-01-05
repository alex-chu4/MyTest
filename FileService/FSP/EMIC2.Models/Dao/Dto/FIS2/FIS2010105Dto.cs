///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  FIS2010106Dto.cs
//  程式名稱：
//  我正在找的人-資料傳輸物件
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		   日期              版本        備註
//  Vivian Chu      2019-09-17         1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供[設定自動尋人]傳輸物件的地方。 
///////////////////////////////////////////////////////////////////////////////////////

namespace EMIC2.Models.Dao.Dto.FIS2
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class FIS2010105Dto
    {
        /// <summary>
        /// 待尋人檔序號
        /// </summary>
        public decimal LOOKFOR_SEQNO { get; set; }

        /// <summary>
        /// 被尋者姓名
        /// </summary>
        public string LOOKFOR_PERSON_NAME { get; set; }

        /// <summary>
        /// 媒合成功筆數
        /// </summary>
        public int SUCCESS_REC_NO { get; set; }

        /// <summary>
        /// 系統取用序號
        /// </summary>
        public decimal? STARTUP_SEQNO { get; set; }

        /// <summary>
        /// 設定時間 
        /// </summary>
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        public DateTime? CREATED_TIME { get; set; }

        /// <summary>
        /// 最後搜尋時間
        /// </summary>
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        public DateTime? LASTSEARCH_TIME { get; set; }

        /// <summary>
        /// 媒合狀態
        /// </summary>
        /// <remarks>Y:已媒合 N:未媒合(Default) D:放棄</remarks>
        public string LOOKFOR_STATUS { get; set; } 
    }
}
