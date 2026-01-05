///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  EEM2010501Dto.cs
//  程式名稱：
//  取得匯入大事紀
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		   日期              版本        備註
//  Jesse Liao     2019-08-26         1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供[應變中心-匯入大事紀]傳輸物件的地方。 
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Dao.Dto.EEM2
{
    using System.ComponentModel.DataAnnotations;
    using System;
    public class EEM2010501Dto
    {
        [Display(Name = "類別")]
        public string REC_NAME { get; set; }

        [Display(Name = "紀錄時間")]
        public DateTime REC_TIME { get; set; }

        [Display(Name = "事件名稱")]
        public string REC_REASON { get; set; }

        [Display(Name = "內容")]
        public string REC_REMARK { get; set; }
    }
}
