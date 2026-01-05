///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  EEM2010801Dto.cs
//  程式名稱：
//  災情看板-查詢
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		   日期              版本              備註
//  Vivian Chu       2019-08-05         1.0.0.0           初始版本
//  Vivian Chu       2019-09-20         2.0.0.0           匯出更新
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供[災情看板-匯入]傳輸物件的地方。 
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Dao.Dto.EEM2
{
    using System.ComponentModel.DataAnnotations;

    public class EEM2010801Dto
    {
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
    }

    /// <summary>
    /// 通報表(C4-1)
    /// </summary>
    public class BOARD_C4_1Dto
    {
        /// <summary>
        /// Gets or sets 紅色警戒溪流數
        /// </summary>
        [Display(Name = "紅色警戒溪流數")]
        public int RED_RIVERS { get; set; }

        /// <summary>
        /// Gets or sets 紅色警戒分布縣市
        /// </summary>
        [Display(Name = "紅色警戒分布縣市")]
        public int RED_CITY_NAME { get; set; }

        /// <summary>
        /// Gets or sets 紅色警戒分布鄉鎮
        /// </summary>
        [Display(Name = "紅色警戒分布鄉鎮")]
        public int RED_TOWNSHIP { get; set; }

        /// <summary>
        /// Gets or sets 紅色警戒分布村里
        /// </summary>
        [Display(Name = "紅色警戒分布村里")]
        public int RED_VILLAGE { get; set; }
    }

    /// <summary>
    /// 通報表(C4-2)
    /// </summary>
    public class BOARD_C4_2Dto
    {
        /// <summary>
        /// Gets or sets 黃色警戒溪流數
        /// </summary>
        [Display(Name = "黃色警戒溪流數")]
        public int AMBER_RIVERS { get; set; }

        /// <summary>
        /// Gets or sets 黃色警戒分布縣市
        /// </summary>
        [Display(Name = "黃色警戒分布縣市")]
        public int AMBER_CITY_NAME { get; set; }

        /// <summary>
        /// Gets or sets 黃色警戒分布鄉鎮
        /// </summary>
        [Display(Name = "黃色警戒分布鄉鎮")]
        public int AMBER_TOWNSHIP { get; set; }

        /// <summary>
        /// Gets or sets 黃色警戒分布村里
        /// </summary>
        [Display(Name = "黃色警戒分布村里")]
        public int AMBER_VILLAGE { get; set; }
    }

    /// <summary>
    /// 通報表(A1)
    /// </summary>
    public class BOARD_A1Dto
    {
        /// <summary>
        /// Gets or sets 死亡人數
        /// </summary>
        [Display(Name = "死亡人數")]
        public int DEAD { get; set; }

        /// <summary>
        /// Gets or sets 失蹤人數
        /// </summary>
        [Display(Name = "失蹤人數")]
        public int MISSING { get; set; }

        /// <summary>
        /// Gets or sets 受傷人數
        /// </summary>
        [Display(Name = "受傷人數")]
        public int INJURED { get; set; }
    }

    /// <summary>
    /// 通報表(A4)
    /// </summary>
    public class BOARD_A4Dto
    {
        /// <summary>
        /// Gets or sets 累計疏散撤離人數
        /// </summary>
        [Display(Name = "累計疏散撤離人數")]
        public int EVACUATE_TOTAL { get; set; }
    }

    /// <summary>
    /// 通報表(D3)
    /// </summary>
    public class BOARD_D3Dto
    {
        /// <summary>
        /// Gets or sets 最高收容人數
        /// </summary>
        [Display(Name = "最高收容人數")]
        public int REFUGEE_TOTAL { get; set; }

        /// <summary>
        /// Gets or sets 目前收容人數
        /// </summary>
        [Display(Name = "目前收容人數")]
        public int REFUGEE { get; set; }
    }

    /// <summary>
    /// 通報表(E2)
    /// </summary>
    public class BOARD_E2Dto
    {
        /// <summary>
        /// Gets or sets 修護戶數
        /// </summary>
        [Display(Name = "修護戶數")]
        public int ELECTRO_DMG_USED { get; set; }

        /// <summary>
        /// Gets or sets 待修護戶數
        /// </summary>
        [Display(Name = "待修護戶數")]
        public int ELECTRO_DMG_NOW { get; set; }

        /// <summary>
        /// Gets or sets 停電戶數
        /// </summary>
        [Display(Name = "停電戶數")]
        public int POWER_FAILURE { get; set; }
    }

    /// <summary>
    /// 通報表(E4)
    /// </summary>
    public class BOARD_E4Dto
    {
        /// <summary>
        /// Gets or sets 停水
        /// </summary>
        [Display(Name = "停水")]
        public int WATER_DMG { get; set; }

        /// <summary>
        /// Gets or sets 修護
        /// </summary>
        [Display(Name = "修護")]
        public int WATER_RECVY { get; set; }

        /// <summary>
        /// Gets or sets 待修護
        /// </summary>
        [Display(Name = "待修護")]
        public int WATER_STOP { get; set; }
    }

    /// <summary>
    /// 通報表(J2)
    /// </summary>
    public class BOARD_J2Dto
    {
        /// <summary>
        /// Gets or sets 市話中斷戶數
        /// </summary>
        [Display(Name = "市話中斷戶數")]
        public int PHONE_DAMAGE_TOTAL { get; set; }

        /// <summary>
        /// Gets or sets 市話修復戶數
        /// </summary>
        [Display(Name = "市話修復戶數")]
        public int PHONE_FIXED { get; set; }

        /// <summary>
        /// Gets or sets 市話待修護戶數
        /// </summary>
        [Display(Name = "市話待修護戶數")]
        public int PHONE_DAMAGE { get; set; }
    }

    /// <summary>
    /// 通報表(J3)
    /// </summary>
    public class BOARD_J3Dto
    {
        /// <summary>
        /// Gets or sets 受損座數
        /// </summary>
        [Display(Name = "受損座數")]
        public int SITE_DAMAGE_TOTAL { get; set; }

        /// <summary>
        /// Gets or sets 修護座數
        /// </summary>
        [Display(Name = "修護座數")]
        public int SITE_FIXED { get; set; }

        /// <summary>
        /// Gets or sets 待修護座數
        /// </summary>
        [Display(Name = "待修護座數")]
        public int SITE_PENDING_REPAIR { get; set; }
    }

    /// <summary>
    /// 水利設施 (KEY IN)、淹水、已退、處理中 (處置報告)
    /// </summary>
    public class Water_FacilitiesDto
    {
        /// <summary>
        /// Gets or sets 淹水幾處
        /// </summary>
        [Display(Name = "淹水幾處")]
        public int START_FLOOD { get; set; }

        /// <summary>
        /// Gets or sets 已退幾處
        /// </summary>
        [Display(Name = "已退幾處")]
        public int END_FLOOD { get; set; }

        /// <summary>
        /// Gets or sets 處理中
        /// </summary>
        [Display(Name = "處理中")]
        public int PROCESSING { get; set; }   
    }

    /// <summary>
    /// 通報表(F1-1)通報表(F1-2)通報表(F1-3)
    /// </summary>
    public class BOARD_F1_1Dto
    {
        /// <summary>
        /// Gets or sets 中斷數
        /// </summary>
        [Display(Name = "中斷數")]
        public int BLOCK_COUNT { get; set; }

        /// <summary>
        /// Gets or sets 搶通數
        /// </summary>
        [Display(Name = "搶通數")]
        public int UNBLOCK_COUNT { get; set; }

        /// <summary>
        /// Gets or sets 預警封閉數
        /// </summary>
        [Display(Name = "預警封閉數")]
        public int STOP_COUNT { get; set; }
    }

    ///// <summary>
    ///// 通報表(F1-2)
    ///// </summary>
    //public class BOARD_F1_2Dto
    //{
    //    /// <summary>
    //    /// Gets or sets 中斷數
    //    /// </summary>
    //    [Display(Name = "中斷數")]
    //    public int BLOCK_COUNT { get; set; }

    //    /// <summary>
    //    /// Gets or sets 搶通數
    //    /// </summary>
    //    [Display(Name = "搶通數")]
    //    public int UNBLOCK_COUNT { get; set; }

    //    /// <summary>
    //    /// Gets or sets 預警封閉數
    //    /// </summary>
    //    [Display(Name = "預警封閉數")]
    //    public int STOP_COUNT { get; set; }
    //}

    ///// <summary>
    ///// 通報表(F1-3)
    ///// </summary>
    //public class BOARD_F1_3Dto
    //{
    //    /// <summary>
    //    /// Gets or sets 中斷數
    //    /// </summary>
    //    [Display(Name = "中斷數")]
    //    public int BLOCK_COUNT { get; set; }

    //    /// <summary>
    //    /// Gets or sets 搶通數
    //    /// </summary>
    //    [Display(Name = "搶通數")]
    //    public int UNBLOCK_COUNT { get; set; }

    //    /// <summary>
    //    /// Gets or sets 預警封閉數
    //    /// </summary>
    //    [Display(Name = "預警封閉數")]
    //    public int STOP_COUNT { get; set; }
    //}

    /// <summary>
    /// 通報表(F2)
    /// 通報表(F2)-依縣市別
    /// </summary>
    public class BOARD_F2Dto
    {
        /// <summary>
        /// Gets or sets 項目
        /// </summary>
        [Display(Name = "項目")]
        public string ITEM { get; set; }

        /// <summary>
        /// Gets or sets 停駛數
        /// </summary>
        [Display(Name = "停駛數")]
        public int NUMBER_STOP { get; set; }
    }

    /// <summary>
    /// 海運：通報表(F4)
    /// </summary>
    public class BOARD_F4Dto
    {
        /// <summary>
        /// Gets or sets 死亡人數
        /// </summary>
        [Display(Name = "死亡人數")]
        public int DEAD { get; set; }
    }

    /// <summary>
    /// 處置報告(農損)
    /// Disposal Report (Agricultural damage)
    /// </summary>
    public class DRADDto
    {
        /// <summary>
        /// Gets or sets 農業損失
        /// </summary>
        [Display(Name = "農業損失")]
        public int CROP { get; set; }
    }

    /// <summary>
    /// 通報表(G1)
    /// </summary>
    public class BOARD_G1Dto
    {
        /// <summary>
        /// Gets or sets 學校受損金額
        /// </summary>
        [Display(Name = "學校受損金額")]
        public int DAMAGE_AMOUNT { get; set; }
    }

    /// <summary>
    /// 處置報告(國軍)
    /// Disposal Report
    /// </summary>
    public class DRGuojunDto
    {
        /// <summary>
        /// Gets or sets 國軍支援兵力
        /// </summary>
        [Display(Name = "國軍支援兵力")]
        public int PEOPLE { get; set; }

        /// <summary>
        /// Gets or sets 國軍支援機具
        /// SUM(I.CAR+I.SHIP+I.MACHINE)
        /// </summary>
        [Display(Name = "國軍支援機具")]
        public int CSM { get; set; }

        /// <summary>
        /// Gets or sets 國軍支援航空器
        /// SUM(I.HELICOPTER+I.AIR_CAMERA) 
        /// </summary>
        [Display(Name = "國軍支援航空器")]
        public int HA_C { get; set; }
    }

    /// <summary>
    /// 處置報告(消防)
    /// Disposal Report
    /// </summary>
    public class DRFireDto
    {
        /// <summary>
        /// Gets or sets 消防支援兵力
        /// </summary>
        [Display(Name = "消防支援兵力")]
        public int PEOPLE { get; set; }

        /// <summary>
        /// Gets or sets 消防支援機具
        /// </summary>
        [Display(Name = "消防支援機具")]
        public int CARMACHINE { get; set; }

        /// <summary>
        /// Gets or sets 消防支援船艇
        /// </summary>
        [Display(Name = "消防支援船艇")]
        public int SHIP { get; set; }


        /// <summary>
        /// Gets or sets 消防支援航空器
        /// </summary>
        [Display(Name = "消防支援航空器")]
        public int HA_C { get; set; }
    }
}
