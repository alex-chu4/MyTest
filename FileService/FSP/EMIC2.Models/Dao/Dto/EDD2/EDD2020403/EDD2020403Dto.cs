///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  EDD2020403Dto.cs
//  程式名稱：
//  救災資源查詢與統計
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-10-02       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  救災資源查詢與統計Modtl
///////////////////////////////////////////////////////////////////////////////////////

using System;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020403
{
    public class EDD2020403Dto
    {
        public int Limit { get; set; }

        public int Offset { get; set; }

        public int Total { get; set; }

        /// <summary>
        /// Gets or sets 檢視角度 O=依填報單位 R=依資源項目
        /// </summary>
        public string P_VIEW_TYPE { get; set; }

        /// <summary>
        /// Gets or sets 填報單位 -1=一級單位的全部 OR 指定的UNIT_ID
        /// </summary>
        public string P_UNIT_ID { get; set; }

        /// <summary>
        /// Gets or sets 資源項目(大類) -1=全部 OR 指定的MASTER_TYPE_ID
        /// </summary>
        public string P_MASTER_TYPE_ID { get; set; }

        /// <summary>
        /// Gets or sets 資源項目(中類) -1=全部 OR 指定的SECONDARY_TYPE_ID
        /// </summary>
        public string P_SECONDARY_TYPE_ID { get; set; }

        /// <summary>
        /// Gets or sets 資源項目(細類) -1=全部 OR 指定的DETAIL_TYPE_ID
        /// </summary>
        public string P_DETAIL_TYPE_ID { get; set; }

        /// <summary>
        /// Gets or sets 資源項目
        /// </summary>
        public string P_RESOURCE_ID { get; set; }

        /// <summary>
        /// Gets or sets 保管場所(縣市) 未輸入給NULL OR 指定的CITY_NAME
        /// </summary>
        public string P_CITY_NAME { get; set; }

        /// <summary>
        /// Gets or sets 保管場所(鄉鎮) 未輸入給NULL OR 指定的TOWN_NAME
        /// </summary>
        public string P_TOWN_NAME { get; set; }

        /// <summary>
        /// Gets or sets 一級單位
        /// </summary>
        public string unit_level_1 { get; set; }

        /// <summary>
        /// Gets or sets 二級單位
        /// </summary>
        public string unit_level_2 { get; set; }

        /// <summary>
        /// Gets or sets 三級單位
        /// </summary>
        public string unit_level_3 { get; set; }

        /// <summary>
        /// Gets or sets 四級單位
        /// </summary>
        public string unit_level_4 { get; set; }

        /// <summary>
        /// Gets or sets 填報單位名稱 未輸入給NULL OR 指定的UNIT_NAME
        /// </summary>
        public string P_UNIT_NAME { get; set; }

        // - 輸出 -

        /// <summary>
        ///  救災資源存量流水號
        /// </summary>
        public  int RESOURCE_STOCK_ID { get; set; }

        /// <summary>
        ///  填報單位保管場所流水號
        /// </summary>
        public int UNIT_LOCATION_ID { get; set; }

        /// <summary>
        ///  填報單位ID
        /// </summary>
        public int UNIT_ID { get; set; }

        /// <summary>
        ///  填報單位
        /// </summary>
        public string UNIT_NAME { get; set; }

        /// <summary>
        ///  機關階層
        /// </summary>
        public int UNIT_LEVE { get; set; }

        /// <summary>
        ///  資源項目ID
        /// </summary>
        public int RESOURCE_ID { get; set; }

        /// <summary>
        ///  資源項目
        /// </summary>
        public string RESOURCE_NAME { get; set; }

        /// <summary>
        ///  保管場所地區
        /// </summary>
        public string AREA_NAME { get; set; }

        /// <summary>
        ///  保管場所
        /// </summary>
        public string LOCATION_NAME { get; set; }

        /// <summary>
        ///  現有存量
        /// </summary>
        public string CURRENT_QTY { get; set; }

        /// <summary>
        ///  可調度量
        /// </summary>
        public string AVAILABLE_QTY { get; set; }

        /// <summary>
        ///  已派遺量
        /// </summary>
        public string SUPPORT_QTY { get; set; }

        /// <summary>
        ///  尚可調度量
        /// </summary>
        public string INVENTORY_QTY { get; set; }

        /// <summary>
        ///  最後更新時間
        /// </summary>
        public DateTime? MODIFIED_TIME { get; set; }

        /// <summary>
        ///  最後更新時間(中)
        /// </summary>
        public string MODIFIED_TIME_TEXT { get; set; }

        /// <summary>
        ///  排序序號 1=明細 2=小計
        /// </summary>
        public int SORT_SEQ  { get; set; }

        /// <summary>
        ///  排序序號 1=明細 2=小計
        /// </summary>
        public string SORT_SEQ_TEXT { get; set; }

        // - 支援調度系統-EDD2_LOCATION_MASTER資料表 --------------------------------------------------------

        /// <summary>
        ///  保管場所流水號
        /// </summary>
        public int LOCATION_ID { get; set; }

        /// <summary>
        /// 縣市
        /// </summary>
        public string CITY_NAME { get; set; }

        /// <summary>
        /// 鄉鎮市區
        /// </summary>
        public string TOWN_NAME { get; set; }

        /// <summary>
        /// 保管場所地址
        /// </summary>
        public string LOCATION_ADDRESS { get; set; }

        /// <summary>
        /// 場所位置座標_X
        /// </summary>
        public string LOCATION_COORDINATE_X { get; set; }

        /// <summary>
        /// 場所位置座標_Y
        /// </summary>
        public string LOCATION_COORDINATE_Y { get; set; }

        /// <summary>
        /// 場所位置經度
        /// </summary>
        public string LOCATION_LONGITUDE { get; set; }

        /// <summary>
        /// 單位位置緯度
        /// </summary>
        public string LOCATION_LATITUDE { get; set; }

        /// <summary>
        /// 聯絡人姓名
        /// </summary>
        public string CONTACT_NAME { get; set; }

        /// <summary>
        /// 聯絡人職稱
        /// </summary>
        public string CONTACT_TITLE { get; set; }

        /// <summary>
        /// 連絡電話號碼
        /// </summary>
        public string CONTACT_TEL { get; set; }

        /// <summary>
        /// 聯絡分機號碼
        /// </summary>
        public string CONTACT_EXT { get; set; }

        /// <summary>
        /// 傳真號碼
        /// </summary>
        public string CONTACT_FAX { get; set; }

        /// <summary>
        /// 聯絡人手機號碼
        /// </summary>
        public string CONTACT_MOBILE { get; set; }

        /// <summary>
        /// 聯絡人電子信箱
        /// </summary>
        public string CONTACT_EMAIL { get; set; }

        // - 支援調度系統-EDD2_UNIT_MASTER 資料表 --------------------------------------------------------

        /// <summary>
        /// 機關OID
        /// </summary>
        public string OID { get; set; }

        /// <summary>
        /// 上級機關單位流水號
        /// </summary>
        public int? PARENT_UNIT_ID { get; set; }

        /// <summary>
        /// 機關階層
        /// </summary>
        public short UNIT_LEVEL { get; set; }

        /// <summary>
        /// 單位地址
        /// </summary>
        public string UNIT_ADDRESS { get; set; }

        /// <summary>
        /// 單位位置座標_X
        /// </summary>
        public string UNIT_COORDINATE_X { get; set; }

        /// <summary>
        /// 單位位置座標_Y
        /// </summary>
        public string UNIT_COORDINATE_Y { get; set; }

        /// <summary>
        /// 單位位置經度
        /// </summary>
        public string UNIT_LONGITUDE { get; set; }

        /// <summary>
        /// 單位位置緯度
        /// </summary>
        public string UNIT_LATITUDE { get; set; }

        /// <summary>
        /// 備用聯絡人姓名
        /// </summary>
        public string CONTACT_NAME_1 { get; set; }

        /// <summary>
        /// 備用聯絡人電子郵箱
        /// </summary>
        public string CONTACT_EMAIL_1 { get; set; }

        // - 支援調度系統-EDD2_RESOURCE_STOCK_DATA資料表 --------------------------------------------------------

        /// <summary>
        /// 備註
        /// </summary>
        public string REMARK { get; set; }


        // - RESOURCE_ITEM_ALL --------------------------------------------
         /// <summary>
        /// 資源主分類流水號
        /// </summary>
         public int MASTER_TYPE_ID { get; set; }

        /// <summary>
        /// 主分類名稱
        /// </summary>
        public string MASTER_TYPE_NAME { get; set; }

        /// <summary>
        /// 資源次分類流水號
        /// </summary>
        public int SECONDARY_TYPE_ID { get; set; }

        /// <summary>
        /// 次分類名稱
        /// </summary>
        public string SECONDARY_TYPE_NAME { get; set; }

        /// <summary>
        /// 保管場所序號
        /// </summary>
        public int DETAIL_TYPE_ID { get; set; }

        /// <summary>
        /// 保管場所序號
        /// </summary>
        public string DETAIL_TYPE_NAME { get; set; }

        /// <summary>
        /// 資源分類
        /// </summary>
        public string Resource_classification { get; set; }

        /// <summary>
        /// 外觀長度
        /// </summary>
        public double? LENGTH { get; set; }

        /// <summary>
        /// 外觀寛度
        /// </summary>
        public double? WIDTH { get; set; }

        /// <summary>
        /// 外觀高度
        /// </summary>
        public double? HEIGHT { get; set; }

        /// <summary>
        /// 外觀尺寸單位
        /// </summary>
        public int? SIZE_UOM { get; set; }

        /// <summary>
        /// 外觀尺寸規格
        /// </summary>
        public string SIZE_TEXT { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public double? WEIGHT { get; set; }

        /// <summary>
        /// 重量單位
        /// </summary>
        public int? WEIGHT_UOM { get; set; }

        /// <summary>
        /// 重量規格
        /// </summary>
        public string WEIGHT_TEXT { get; set; }

        /// <summary>
        /// 功率能量
        /// </summary>
        public double? ENERGY_VOLUMN { get; set; }

        /// <summary>
        /// 功率能量單位
        /// </summary>
        public int? ENERGY_UOM { get; set; }

        /// <summary>
        /// 功率規格
        /// </summary>
        public string ENERGY_TEXT { get; set; }

        /// <summary>
        /// 效能
        /// </summary>
        public double? EFFICACY { get; set; }

        /// <summary>
        /// 效能單位
        /// </summary>
        public int? EFFICACY_UOM { get; set; }

        /// <summary>
        /// 效能規格
        /// </summary>
        public string EFFICACY_TEXT { get; set; }

        /// <summary>
        /// 承載人員數量
        /// </summary>
        public int? BEARING_NO { get; set; }

        /// <summary>
        /// 操作人員數量
        /// </summary>
        public int? OPERATOR_NO { get; set; }

        /// <summary>
        /// 使用限制
        /// </summary>
        public string BEARING_OPERATOR_TEXT { get; set; }

        /// <summary>
        /// 其他備註說明
        /// </summary>
        public string RESOURCE_SPEC { get; set; }

        /// <summary>
        /// 標準計量單位
        /// </summary>
        public int STANDARD_UOM { get; set; }

        /// <summary>
        /// 需回收資源
        /// </summary>
        public string RETURN_CODE { get; set; }

        /// <summary>
        /// 共用資源
        /// </summary>
        public string SHARE_FLAG { get; set; }

        /// <summary>
        /// 消防資源
        /// </summary>
        public string FIREFIGHTING_RESOURCE { get; set; }

        /// <summary>
        /// 外觀尺寸單位
        /// </summary>
        public string SIZE_UOM_NAME { get; set; }

        /// <summary>
        /// 重量單位
        /// </summary>
        public string WEIGHT_UOM_NAME { get; set; }

        /// <summary>
        /// 功率能量單位
        /// </summary>
        public string ENERGY_UOM_NAME { get; set; }

        /// <summary>
        /// 效能單位
        /// </summary>
        public string EFFICACY_UOM_NAME { get; set; }

        /// <summary>
        /// 標準計量單位
        /// </summary>
        public string STANDARD_UOM_NAME { get; set; }


        // 地圖點搜尋使用-----------------------------------------------------------------------------

        /// <summary>
        ///  保管場所流水號-多筆
        /// </summary>
        public string LOCATION_ID_ITEMS { get; set; }

    }
}
