
///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2020141Dto.cs
//  程式名稱：
//  A3通報表下層匯整
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-04-26       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  A3通報表下層匯整，時所使用Dto
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.ERA
{
    public class ERA2020141Dto
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
        public long PRJ_NO { get; set; }
    }
}
