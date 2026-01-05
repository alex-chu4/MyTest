///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  EDD2020301Dto.cs
//  程式名稱：
//  EDD2020301Dto
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-09-24       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  回傳結果時所使用Model
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020301
{
    public class EDD2020301Dto
    {
        // EDD2_QRY_UNIT_OWN

        // 參數

        /// <summary>
        /// Gets or sets 機關ID
        /// </summary>
        public int P_UNIT_ID { get; set; }

        /// <summary>
        /// Gets or sets 機關查詢類型
        /// </summary>
        public int P_QRY_TYPE { get; set; }

        // 輸出
        public int UNIT_ID_1 { get; set; }

        public int UNIT_ID_2 { get; set; }

        public int UNIT_ID_3 { get; set; }

        public int UNIT_ID_4 { get; set; }

        public int UNIT_ID_5 { get; set; }

        public int UNIT_ID_6 { get; set; }

        public int UNIT_ID_7 { get; set; }

        public int UNIT_ID { get; set; }

        /// <summary>
        ///  Gets or sets 發布機關單位名稱
        /// </summary>
        public string UNIT_NAME { get; set; }

        public int PARENT_UNIT_ID { get; set; }

        public int UNIT_LEVEL { get; set; }

        public string CITY_NAME { get; set; }

        public string TOWN_NAME { get; set; }

        /// <summary>
        ///  Gets or sets 發布機關OID
        /// </summary>
        public string OID { get; set; }
    }
}
