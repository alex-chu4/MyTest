///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030107SearchModelDto.cs
//  程式名稱：
//  查詢使用Model
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本       備註
//  timan             2019-08-27      1.0.0.0     初始版本
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

namespace EMIC2.Models.Dao.Dto.ERA.ERA2030107
{
    public class ERA2030107SearchModelDto
    {
        /// <summary>
        /// Gets or sets 通報表主檔序號
        /// </summary>
        [Display(Name = "通報表主檔序號")]
        public int? RPT_MAIN_ID { get; set; } = null;

        /// <summary>
        /// Gets or sets 應變中心代碼
        /// </summary>
        [Display(Name = "應變中心代碼")]
        public string EOC_ID { get; set; }

        /// <summary>
        /// Gets or sets 專案代號
        /// </summary>
        [Display(Name = "專案代號")]
        public long PRJ_NO { get; set; }

        public string ORG_ID { get; set; }

        public string DISP_MAIN_ID { get; set; }

        public string DISP_DETAIL_ID { get; set; }

        public string DISP_STYLE_ID { get; set; }

        public string EOC_LEVEL { get; set; }
    }
}
