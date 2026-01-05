
///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2Dto.cs
//  程式名稱：
//  ERA2共用資料存取屬性
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-05-20       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2共用資料存取屬性
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto
{
    public class ERA2Dto
    {
        /// <summary>
        /// Gets or sets 通報表主檔序號
        /// </summary>
        [Display(Name = "通報表主檔序號")]
        public int RPT_MAIN_ID { get; set; }

        /// <summary>
        /// Gets or sets 應變中心代碼
        /// </summary>
        [Display(Name = "應變中心代碼")]
        public string EOC_ID { get; set; }

        /// <summary>
        /// Gets or sets 專案代號
        /// </summary>
        [Display(Name = "專案代號")]
        public int PRJ_NO { get; set; }

        /// <summary>
        /// Gets or sets 填報機關代號
        /// </summary>
        public int ORG_ID { get; set; }

        /// <summary>
        /// Gets or sets 報表定義序號
        /// </summary>
        public int RPT_DEF_ID { get; set; }

        /// <summary>
        /// Gets or sets 報表代碼
        /// </summary>
        public string RPT_CODE { get; set; }

        /// <summary>
        /// Gets or sets 縣市別
        /// </summary>
        public string CITY_ID { get; set; }

        /// <summary>
        /// Gets or sets 鄉鎮別
        /// </summary>
        public string TOWN_ID { get; set; }

        /// <summary>
        /// Gets or sets 縣市別
        /// </summary>
        public string CITY_NAME { get; set; }

        /// <summary>
        /// Gets or sets 鄉鎮別
        /// </summary>
        public string TOWN_NAME { get; set; }

        /// <summary>
        /// Gets or sets 通報時間
        /// </summary>
        public DateTime? RPT_TIME { get; set; }

        /// <summary>
        /// Gets or sets 通報時間
        /// </summary>
        public string RPT_TIME_TEXT { get; set; }

        /// <summary>
        /// Gets or sets 通報時間A
        /// </summary>
        public DateTime? RPT_TIME_A { get; set; }

        /// <summary>
        /// Gets or sets 通報時間B
        /// </summary>
        public DateTime? RPT_TIME_B { get; set; }

        // --------------------------------

        /// <summary>
        /// Gets or sets 災害類型名稱
        /// </summary>
        public string DIS_NAME { get; set; }

        /// <summary>
        /// Gets or sets 專案名稱
        /// </summary>
        public string CASE_NAME { get; set; }

        /// <summary>
        /// Gets or sets 開設層級
        /// </summary>
        public string DIS_OPEN_LV { get; set; }

        /// <summary>
        /// Gets or sets 開設層級
        /// </summary>
        public string OPEN_LV { get; set; }

        /// <summary>
        /// Gets or sets 開設狀態
        /// </summary>
        public string OPEN_STATUS { get; set; }

        /// <summary>
        /// Gets or sets 災害序號
        /// </summary>
        public int? DIS_DATA_UID_DAO { get; set; }

        public DateTime? PRJ_STIME { get; set; }

        public DateTime? PRJ_ETIME { get; set; }

        public string PRJ_STIME_TEXT { get; set; }

        public string EVACUATE_TIME_TEXT { get; set; }

        // --------------------------------

        /// <summary>
        /// Gets or sets 速報區間-起
        /// </summary>
        public DateTime? P_RPT_TIME_S { get; set; }

        public string P_RPT_TIME_S_TEXT { get; set; }

        /// <summary>
        /// Gets or sets 速報區間-迄
        /// </summary>
        public DateTime? P_RPT_TIME_E { get; set; }

        public string P_RPT_TIME_E_TEXT { get; set; }

        /// <summary>
        /// Gets or sets 樁號範圍
        /// </summary>
        public string P_LINECODE { get; set; }

        public int? RPT_DETAIL_ID { get; set; }

        /// <summary>
        /// Gets or sets 1依道路別 2依阻斷時間
        /// </summary>
        public string ORDER_TYPE { get; set; }

        public string SHOW_ORDER { get; set; }


        /// <summary>
        /// Gets or sets 實際搶通(開放)時間
        /// </summary>
        public DateTime? OPEN_DATETIME { get; set; }

        // ------------------------------------------------

        /// <summary>
        /// F1 確認按鍵-顯示狀態 Y/N
        /// </summary>
        public string DIAPLAY_TYPE_ID { get; set; }

        /// <summary>
        /// Gets or sets 預警性封閉(處)
        /// </summary>
        public string ORG_CHECK { get; set; }

        public DateTime? SENDER_TIME { get; set; }

        // ------------------------------------------------F1 F1a

        public List<F1DaoResult> f1DaoResult { get; set; }

        public List<HightWayDaoResult> highwayDaoResult { get; set; }

        /// <summary>
        /// Gets or sets 狀態
        /// D0=未搶通 P0=封閉 D1 = 搶通 P1=解除封閉
        /// </summary>
        public string TRFSTATUS { get; set; }

        /// <summary>
        ///  災情編號
        /// </summary>
        public string CASE_NO { get; set; }

        /// <summary>
        ///  發生時間
        /// </summary>
        public DateTime? CASE_TIME { get; set; }

        /// <summary>
        ///  發生時間-中文
        /// </summary>
        public string CASE_TIME_TEXT { get; set; }

        /// <summary>
        ///  地區 (縣市+鄉鎮)
        /// </summary>
        public string CITY_AREA { get; set; }

        /// <summary>
        ///  附近地點
        /// </summary>
        public string CASE_LOCATION { get; set; }

        /// <summary>
        ///  災害地點(WGS84)
        /// </summary>
        public string CASE_WGS84_PTS { get; set; }

        /// <summary>
        ///  災情描述
        /// </summary>
        public string CASE_DESC { get; set; }

        public int MTMP_REC_ID { get; set; }
    }

    public class ERA2_CHECK_CONFIRM
    {
        public int ISSUCCESSFUL { get; set; }
        public string MSG { get; set; }
    }

    /// <summary>
    /// 公路總局外部匯入
    /// </summary>
    public class HightWayDaoResult
    {
        public DateTime? SENDER_TIME { get; set; }

        /// <summary>
        ///  地區 (縣市+鄉鎮)
        /// </summary>
        public string CITY_AREA { get; set; }

        public int MTMP_DETAIL_ID { get; set; }
        public int MTMP_REC_ID { get; set; }

        /// <summary>
        /// Gets or sets 狀態
        /// D0=未搶通 P0=封閉 D1 = 搶通 P1=解除封閉
        /// </summary>
        public string TRFSTATUS { get; set; }

        /// <summary>
        /// Gets or sets 道路別
        /// </summary>
        public string ROADTYPE_ID { get; set; }

        public  string ROADTYPE_NAME { get; set; }
        public string ROAD_ID { get; set; }
        public decimal? STARTMILE { get; set; }
        public decimal? STARTPLUS { get; set; }
        public decimal? ENDMILE { get; set; }
        public decimal? ENDPLUS { get; set; }

        /// <summary>
        /// Gets or sets 路線樁號/路名
        /// </summary>
        public string LINECODE { get; set; }
        public string CITY_ID { get; set; }
        public string CITY_NAME { get; set; }
        public string TOWN_ID { get; set; }
        public string TOWN_NAME { get; set; }

        /// <summary>
        /// Gets or sets 附近地名
        /// </summary>
        public string ADDNAME { get; set; }
        public string WGS84_X { get; set; }
        public string WGS84_Y { get; set; }
        public string WGS84_X_SCTL { get; set; }
        public string WGS84_Y_SCTL { get; set; }
        public string WGS84_X_ECTL { get; set; }
        public string WGS84_Y_ECTL { get; set; }
        public string CLOSE_TYPE { get; set; }
        public string CLOSE_TSUB { get; set; }

        /// <summary>
        /// Gets or sets 阻斷封閉時間
        /// </summary>
        public string CLOSE_DATETIME { get; set; }

        /// <summary>
        /// Gets or sets 阻斷封閉時間-中文
        /// </summary>
        public string CLOSE_DATETIME_TEXT { get; set; }

        public int? REPAIRE_DAY { get; set; }
        public string CLOSE_TEXT { get; set; }
        public string REPAIRE_TEXT { get; set; }
        public string ALT_ROUTE { get; set; }
        public int? BUILD_COST { get; set; }
        public string OPEN_TEXT { get; set; }
        public string OPEN_DATETIME { get; set; }
        public string REMARK { get; set; }
        public string BRG_CLOSE { get; set; }
        public string NO_DATA_MARK { get; set; }
        public string CASE_NO { get; set; }
        public string DATA_SOURCE { get; set; }
        public string CREATED_USER { get; set; }
        public DateTime? CREATED_TIME { get; set; }
        public string MODIFIED_USER { get; set; }
        public DateTime? MODIFIED_TIME { get; set; }

    }

    /// <summary>
    /// F1外部匯入
    /// </summary>
    public class F1DaoResult
    {
        /// <summary>
        ///  災情編號
        /// </summary>
        public string CASE_NO { get; set; }

        /// <summary>
        ///  發生時間
        /// </summary>
        public DateTime? CASE_TIME { get; set; }

        /// <summary>
        ///  發生時間-中文
        /// </summary>
        public string CASE_TIME_TEXT { get; set; }

        /// <summary>
        ///  地區 (縣市+鄉鎮)
        /// </summary>
        public string CITY_AREA { get; set; }

        /// <summary>
        ///  附近地點
        /// </summary>
        public string CASE_LOCATION { get; set; }

        /// <summary>
        ///  災害地點(WGS84)
        /// </summary>
        public string CASE_WGS84_PTS { get; set; }

        /// <summary>
        ///  災情描述
        /// </summary>
        public string CASE_DESC { get; set; }
    }

    /// <summary>
    /// 中央 D3, 縣市 D3a, 鄉鎮 D3a 外部匯入 中央 D3, 縣市 D3a, 鄉鎮 D3a
    /// </summary>
    public class EEA2_SHELTER
    {
        public EEA2_SHELTER()
        {
            this.REFUGEE = 0;
            this.REFUGEE_M = 0;
            this.REFUGEE_F = 0;
            this.REFUGEE_TOTAL = 0;
            this.REFUGEE_TOTAL_M = 0;
            this.REFUGEE_TOTAL_F = 0;
            this.LAST_REFUGEE_TOTAL = 0;
            this.SUPPLY_PEOPLE = 0;
            this.SUPPLY_DAY = 0;
            this.PEOPLE_NO = 0;
        }

        /// <summary>
        /// Gets or sets 鄉鎮市區合併名稱
        /// </summary>
        public string CITY_TOWN_VILLAGE_NAME { get; set; }

        /// <summary>
        /// Gets or sets 縣市別
        /// </summary>
        public string CITY_NAME { get; set; }

        /// <summary>
        /// Gets or sets 地區
        /// </summary>
        public string TOWN_NAME { get; set; }

        /// <summary>
        /// Gets or sets 收容場所
        /// </summary>
        public string SHELTER { get; set; }

        /// <summary>
        /// Gets or sets 開設起迄時間(年月日時)
        /// </summary>
        public DateTime? OPEN_DATETIME { get; set; }

        /// <summary>
        /// Gets or sets 開設起迄時間(年月日時)
        /// </summary>
        public string OPEN_DATETIME_TEXT { get; set; }

        /// <summary>
        /// Gets or sets 開設起迄時間時
        /// </summary>
        public int? OPEN_DATETIME_HOUR { get; set; }

        /// <summary>
        /// Gets or sets 開設起迄時間分
        /// </summary>
        public int? OPEN_DATETIME_MINUTE { get; set; }

        /// <summary>
        /// Gets or sets 撤除時間(年月日時)
        /// </summary>
        public DateTime? CLOSE_DATETIME { get; set; }

        /// <summary>
        /// Gets or sets 撤除時間(年月日時)
        /// </summary>
        public string CLOSE_DATETIME_TEXT { get; set; }

        /// <summary>
        /// Gets or sets 撤除時間時
        /// </summary>
        public int? CLOSE_DATETIME_HOUR { get; set; }

        /// <summary>
        /// Gets or sets 撤除時間分
        /// </summary>
        public int? CLOSE_DATETIME_MINUTE { get; set; }

        /// <summary>
        /// Gets or sets 目前收容人數-合計
        /// </summary>
        public int? REFUGEE { get; set; }

        /// <summary>
        /// Gets or sets 目前收容人數-男
        /// </summary>
        public int? REFUGEE_M { get; set; }

        /// <summary>
        /// Gets or sets 目前收容人數-女
        /// </summary>
        public int? REFUGEE_F { get; set; }

        /// <summary>
        /// Gets or sets 累計收容人數-合計
        /// </summary>
        public int? REFUGEE_TOTAL { get; set; }

        /// <summary>
        /// Gets or sets 累計收容人數-男
        /// </summary>
        public int? REFUGEE_TOTAL_M { get; set; }

        /// <summary>
        /// Gets or sets 累計收容人數-女
        /// </summary>
        public int? REFUGEE_TOTAL_F { get; set; }

        /// <summary>
        /// Gets or sets 目前儲糧預估可再供應狀況-合計
        /// </summary>
        public int? LAST_REFUGEE_TOTAL { get; set; }

        /// <summary>
        /// Gets or sets 目前儲糧預估可再供應狀況-人數
        /// </summary>
        public int? SUPPLY_PEOPLE { get; set; }

        /// <summary>
        /// Gets or sets 目前儲糧預估可再供應狀況-日數
        /// </summary>
        public int? SUPPLY_DAY { get; set; }

        /// <summary>
        /// Gets or sets 是否以開口契約或民間團體持續供應
        /// </summary>
        public string KEEP_SUPPLY { get; set; }

        /// <summary>
        /// Gets or sets 聯絡人
        /// </summary>
        public string CONTACT { get; set; }

        /// <summary>
        /// Gets or sets 聯絡電話(方式)
        /// </summary>
        public string CONTACT_PHONE { get; set; }

        /// <summary>
        /// Gets or sets 場所可收容人數
        /// </summary>
        public int? PEOPLE_NO { get; set; }
    }


    public class EEA2_EVACUATE
    {
        public EEA2_EVACUATE()
        {
            this.EVACUATE_EST = 0;
            this.EVACUATE_REAL = 0;
            this.EVACUATE_TOTAL = 0;
            this.LAST_EVACUATE_TOTAL = 0;
        }

        /// <summary>
        /// Gets or sets 縣市別
        /// </summary>
        [Display(Name = "縣市別")]

        public string CITY_NAME { get; set; }

        /// <summary>
        /// Gets or sets 鄉鎮別
        /// </summary>
        [Display(Name = "鄉鎮別")]

        public string TOWN_NAME { get; set; }

        /// <summary>
        /// Gets or sets 村里別
        /// </summary>
        [Display(Name = "村里別")]
        public string VILLAGE_NAME { get; set; }

        /// <summary>
        /// Gets or sets 鄉鎮市區合併名稱
        /// </summary>
        public string CITY_TOWN_VILLAGE_NAME { get; set; }

        /// <summary>
        /// Gets or sets 地點
        /// </summary>
        [Display(Name = "地點")]
        public string LOCATION { get; set; }

        /// <summary>
        /// Gets or sets 預計撤離人數
        /// </summary>
        [Display(Name = "預計撤離人數")]
        public int? EVACUATE_EST { get; set; }

        /// <summary>
        /// Gets or sets 實際撤離人數
        /// </summary>
        [Display(Name = "實際撤離人數")]
        public int? EVACUATE_REAL { get; set; }

        /// <summary>
        /// Gets or sets 累計撤離人數
        /// </summary>
        [Display(Name = "累計撤離人數")]
        public int? EVACUATE_TOTAL { get; set; }

        /// <summary>
        /// Gets or sets 過去合計
        /// </summary>
        [Display(Name = "過去合計")]
        public int? LAST_EVACUATE_TOTAL { get; set; }

        /// <summary>
        /// Gets or sets 撤離時間
        /// </summary>
        [Display(Name = "撤離時間")]
        public DateTime? EVACUATE_TIME { get; set; }

        /// <summary>
        /// Gets or sets 撤離時間
        /// </summary>
        [Display(Name = "撤離時間")]
        public string EVACUATE_TIME_TEXT { get; set; }

        /// <summary>
        /// Gets or sets 撤離時間時
        /// </summary>
        [Display(Name = "撤離時間時")]
        public int? EVACUATE_HOUR { get; set; }

        /// <summary>
        /// Gets or sets 撤離時間分
        /// </summary>
        [Display(Name = "撤離時間分")]
        public int? EVACUATE_MINUTE { get; set; }

        /// <summary>
        /// Gets or sets 收容處所
        /// </summary>
        [Display(Name = "收容處所")]
        public string SHELTER { get; set; }

        /// <summary>
        /// Gets or sets 備註
        /// </summary>
        [Display(Name = "備註")]
        public string MEMO { get; set; }
    }

}
