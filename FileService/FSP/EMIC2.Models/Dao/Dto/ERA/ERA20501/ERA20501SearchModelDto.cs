///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  SearchModelDto.cs
//  程式名稱：
//  查詢使用Model
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本       備註
//  timan             2019-08-21      1.0.0.0     初始版本
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

namespace EMIC2.Models.Dao.Dto.ERA.ERA20501
{
    public class ERA20501SearchModelDto
    {
        /// <summary>
        /// Gets or sets 應變中心代碼
        /// </summary>
        [Display(Name = "應變中心代碼")]
        public string eoc_id { get; set; }

        /// <summary>
        /// Gets or sets 專案代號
        /// </summary>
        [Display(Name = "專案代號")]
        public string prj_no { get; set; }

        /// <summary>
        /// Gets or sets 登入機關
        /// </summary>
        [Display(Name = "登入機關")]
        public string org_id { get; set; }

        /// <summary>
        /// Gets or sets 回傳資料格式
        /// </summary>
        [Display(Name = "回傳資料格式")]
        public string format { get; set; }
    }
}
