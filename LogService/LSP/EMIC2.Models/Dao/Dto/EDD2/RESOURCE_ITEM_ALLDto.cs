///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  RESOURCE_ITEM_ALL.cs
//  程式名稱：
//  資源項目細項資料表
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本       備註
//  timan              2019-09-20      1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  資源項目細項資料表
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Dao.Dto.EDD2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RESOURCE_ITEM_ALLDto : BaseModelDto
    {
        public RESOURCE_ITEM_ALLDto()
        {
            this.LENGTH = 0;
            this.WIDTH = 0;
            this.HEIGHT = 0;
            this.WEIGHT = 0;
            this.ENERGY_VOLUMN = 0;
            this.ENERGY_UOM = 0;
            this.EFFICACY = 0;
            this.EFFICACY_UOM = 0;
            this.BEARING_NO = 0;
            this.OPERATOR_NO = 0;
            this.STANDARD_UOM = 0;

        }

        /// <summary>
        /// 資源主分類流水號
        /// </summary>
        [DisplayName("資源主分類流水號")]
        public int MASTER_TYPE_ID { get; set; }

        /// <summary>
        /// 主分類名稱
        /// </summary>
        [DisplayName("主分類名稱")]
        public string MASTER_TYPE_NAME { get; set; }

        /// <summary>
        /// 資源次分類流水號
        /// </summary>
        [DisplayName("資源次分類流水號")]
        public int SECONDARY_TYPE_ID { get; set; }

        /// <summary>
        /// 次分類名稱
        /// </summary>
        [DisplayName("次分類名稱")]
        public string SECONDARY_TYPE_NAME { get; set; }

        /// <summary>
        /// 資源細分類流水號
        /// </summary>
        [DisplayName("資源細分類流水號")]
        public int DETAIL_TYPE_ID { get; set; }

        /// <summary>
        /// 細分類名稱
        /// </summary>
        [DisplayName("細分類名稱")]
        public string DETAIL_TYPE_NAME { get; set; }

        /// <summary>
        /// 資源項目流水號
        /// </summary>
        [DisplayName("資源細分類流水號")]
        public int RESOURCE_ID { get; set; }

        /// <summary>
        /// 資源項目名稱
        /// </summary>
        [DisplayName("細分類名稱")]
        public string RESOURCE_NAME { get; set; }

        /// <summary>
        /// 外觀長度
        /// </summary>
        [DisplayName("外觀長度")]
        public double? LENGTH { get; set; }

        /// <summary>
        /// 外觀寛度
        /// </summary>
        [DisplayName("外觀寛度")]
        public double? WIDTH { get; set; }

        /// <summary>
        /// 外觀高度
        /// </summary>
        [DisplayName("外觀高度")]
        public double? HEIGHT { get; set; }

        /// <summary>
        /// 外觀尺寸單位代碼
        /// </summary>
        [DisplayName("外觀尺寸單位代碼")]
        public int? SIZE_UOM { get; set; }

        /// <summary>
        /// 外觀尺寸單位
        /// </summary>
        [DisplayName("外觀尺寸單位")]
        public string SIZE_UOM_NAME { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        [DisplayName("重量")]
        public double? WEIGHT { get; set; }

        /// <summary>
        /// 重量單位代碼
        /// </summary>
        [DisplayName("重量單位代碼")]
        public int? WEIGHT_UOM { get; set; }

        /// <summary>
        /// 重量單位
        /// </summary>
        [DisplayName("重量單位")]
        public string WEIGHT_UOM_NAME { get; set; }

        /// <summary>
        /// 功率能量
        /// </summary>
        [DisplayName("功率能量")]
        public double? ENERGY_VOLUMN { get; set; }

        /// <summary>
        /// 功率能量單位代碼
        /// </summary>
        [DisplayName("功率能量單位代碼")]
        public int? ENERGY_UOM { get; set; }

        /// <summary>
        /// 功率能量單位
        /// </summary>
        [DisplayName("功率能量單位")]
        public string ENERGY_UOM_NAME { get; set; }

        /// <summary>
        /// 效能
        /// </summary>
        [DisplayName("效能")]
        public double? EFFICACY { get; set; }

        /// <summary>
        /// 效能單位
        /// </summary>
        [DisplayName("效能單位")]
        public int? EFFICACY_UOM { get; set; }

        /// <summary>
        /// 效能單位
        /// </summary>
        [DisplayName("效能單位")]
        public string EFFICACY_UOM_NAME { get; set; }

        /// <summary>
        /// 承載人員數量
        /// </summary>
        [DisplayName("承載人員數量")]
        public int? BEARING_NO { get; set; }

        /// <summary>
        /// 操作人員數量
        /// </summary>
        [DisplayName("操作人員數量")]
        public int? OPERATOR_NO { get; set; }

        /// <summary>
        /// 其他備註說明
        /// </summary>
        [DisplayName("其他備註說明")]
        public string RESOURCE_SPEC { get; set; }

        /// <summary>
        /// 標準計量單位
        /// </summary>
        [DisplayName("標準計量單位")]
        public int? STANDARD_UOM { get; set; }

        /// <summary>
        /// 標準計量單位
        /// </summary>
        [DisplayName("標準計量單位")]
        public string STANDARD_UOM_NAME { get; set; }

        /// <summary>
        /// 需回收資源
        /// </summary>
        [DisplayName("需回收資源")]
        public string RETURN_CODE { get; set; }

        /// <summary>
        /// 共用資源
        /// </summary>
        [DisplayName("共用資源")]
        public string SHARE_FLAG { get; set; }

        /// <summary>
        /// 消防資源
        /// </summary>
        [DisplayName("消防資源")]
        public string FIREFIGHTING_RESOURCE { get; set; }

        /// <summary>
        /// 種類全名
        /// </summary>
        [DisplayName("種類全名")]
        public string FULL_TYPE_NAME { get; set; }
    }
}
