///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  EEM2_QRY_EOC_PRJ_Dto.cs
//  程式名稱：
//  執行SQL User Defined Function EEM2_QRY_EOC_PRJ
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期              版本              備註
//  Enosh           2019-08-26       1.0.0.0           初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  執行後回傳的資料模型
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Dao.Dto.COMMON
{
    public class EEM2_QRY_EOC_PRJ_Dto
    {
        public string EOC_ID { get; set; }

        public string EOC_NAME { get; set; }

        public string PRJ_NO { get; set; }

        public string PRJ_NAME { get; set; }

        public string OPEN_LV { get; set; }
    }
}
