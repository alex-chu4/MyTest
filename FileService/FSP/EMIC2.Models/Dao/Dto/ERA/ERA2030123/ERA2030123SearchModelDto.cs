///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030123SearchModelDto.cs
//  程式名稱：
//  查詢使用Model
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本       備註
//  timan             2019-08-26      1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  查詢使用Model
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.ERA.ERA2030123
{
    public class ERA2030123SearchModelDto
    {
        /// <summary>
        /// Gets or sets 主檔序號
        /// </summary>
        [Display(Name = "主檔序號")]
        public int DISP_MAIN_ID { get; set; }

        /// <summary>
        /// Gets or sets 項目明細檔序號
        /// </summary>
        [Display(Name = "項目明細檔序號")]
        public string DISP_DETAIL_ID { get; set; }

        /// <summary>
        /// Gets or sets 項目樣式序號
        /// </summary>
        [Display(Name = "項目樣式序號")]
        public long DISP_STYLE_ID { get; set; }

        /// <summary>
        /// Gets or sets 中央應變中心登入 EOC_LEVEL=1，縣市應變中心登入 EOC_LEVEL=2，公所應變中心登入 EOC_LEVEL=3
        /// </summary>
        [Display(Name = "應變中心等級")]
        public string EOC_LEVEL { get; set; }
    }
}
