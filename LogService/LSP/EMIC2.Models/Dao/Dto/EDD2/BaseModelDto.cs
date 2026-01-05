///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  BaseModel.cs
//  程式名稱：
//  EDD 基底共用參數
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本       備註
//  Joe              2019-09-12      1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  EDD 基底共用參數
///////////////////////////////////////////////////////////////////////////////////////

namespace EMIC2.Models.Dao.Dto.EDD2
{
    using System;
    using System.ComponentModel;

    public class BaseModelDto
    {
        // - 共用參數 -
        public int Limit { get; set; }

        public int Offset { get; set; }

        public string SortName { get; set; }

        public string SortOrder { get; set; }

        public int Total { get; set; }


        // - 共同欄位 -

        /// <summary>
        ///  縣市別
        /// </summary>
        [DisplayName("縣市別")]
        public string CITY_ID { get; set; }

        /// <summary>
        ///  鄉鎮別
        /// </summary>
        [DisplayName("鄉鎮別")]

        public string TOWN_ID { get; set; }

        /// <summary>
        ///  縣市別
        /// </summary>
        [DisplayName("縣市別")]

        public string CITY_NAME { get; set; }

        /// <summary>
        ///  鄉鎮別
        /// </summary>
        [DisplayName("鄉鎮別")]

        public string TOWN_NAME { get; set; }

        /// <summary>
        ///  村里別
        /// </summary>
        [DisplayName("村里別")]
        public string VILLAGE_NAME { get; set; }

        /// <summary>
        ///  鄉鎮市區名稱
        /// </summary>
        [DisplayName("鄉鎮市區名稱")]
        public string CITY_TOWN_NAME { get; set; }
        
        /// <summary>
        ///  鄉鎮市區合併名稱
        /// </summary>
        [DisplayName("鄉鎮市區合併名稱")]
        public string CITY_TOWN_VILLAGE_NAME { get; set; }

        /// <summary>
        ///  行政區排序
        /// </summary>
        [DisplayName("行政區排序")]
        public string SHOW_ORDER { get; set; }

        /// <summary>
        ///  無資料
        /// </summary>
        [DisplayName("無資料")]
        public string NO_DATA_MARK { get; set; }

        /// <summary>
        ///  登入者帳號
        /// </summary>
        [DisplayName("登入者帳號")]
        public string CREATED_USER { get; set; }

        /// <summary>
        ///  建立時間
        /// </summary>
        [DisplayName("建立時間")]
        public DateTime? CREATED_TIME { get; set; }

        /// <summary>
        ///  建立時間 - 中文
        /// </summary>
        [DisplayName("建立時間")]
        public string CREATED_TIME_TEXT { get; set; }

        /// <summary>
        ///  修改者帳號
        /// </summary>
        [DisplayName("修改者帳號")]
        public string MODIFIED_USER_NAME { get; set; }

        /// <summary>
        ///  修改者帳號
        /// </summary>
        [DisplayName("修改者帳號")]
        public string MODIFIED_USER { get; set; }

        /// <summary>
        ///  修改時間
        /// </summary>
        [DisplayName("修改時間")]
        public DateTime? MODIFIED_TIME { get; set; }

        /// <summary>
        ///  修改時間 - 中文
        /// </summary>
        [DisplayName("修改時間")]
        public string MODIFIED_TIME_TEXT { get; set; }
    }
}
