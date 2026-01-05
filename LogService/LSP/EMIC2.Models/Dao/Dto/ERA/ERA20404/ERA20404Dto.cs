///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA20404Dto.cs
//  程式名稱：
//  各部會處置報告最新填報狀況查詢頁面
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-07-19       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  各部會處置報告最新填報狀況查詢頁面，時所使用Dto
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Dao.Dto.ERA.ERA20404
{
    public class ERA20404Dto
    {
        public ERA20404Dto()
        {
            this.COL_A4 = 0;
            this.COL_D3 = 0;
        }

        /// <summary>
        /// Gets or sets 應變中心代碼
        /// </summary>
        public string EOC_ID { get; set; }

        /// <summary>
        /// Gets or sets 專案代號
        /// </summary>
        public int PRJ_NO { get; set; }

        public string APC_ID { get; set; }

        /// <summary>
        /// Gets or sets 縣市編號
        /// </summary>

        public string CITY_ID { get; set; }

        /// <summary>
        /// Gets or sets 縣市編號
        /// </summary>

        public string CITY_NAME { get; set; }

        /// <summary>
        /// Gets or sets 鄉鎮市區編號
        /// </summary>

        public string TOWN_ID { get; set; }

        /// <summary>
        /// Gets or sets 鄉鎮市區編號
        /// </summary>

        public string TOWN_NAME { get; set; }

        /// <summary>
        /// Gets or sets 地區
        /// </summary>
        public string AREA { get; set; }

        /// <summary>
        /// Gets or sets 疏散撤離人數（資料來源A4）
        /// </summary>

        public int? COL_A4 { get; set; }

        /// <summary>
        /// Gets or sets 收容安置人數（資料來源D3）
        /// </summary>

        public int? COL_D3 { get; set; }

        /// <summary>
        /// Gets or sets 排序
        /// </summary>

        public string SHOW_ORDER { get; set; }

    }
}
