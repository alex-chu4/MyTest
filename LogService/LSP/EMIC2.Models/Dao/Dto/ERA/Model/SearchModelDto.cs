///////////////////////////////////////////////////////////////////////////////////////
// 程式檔名：
//  SearchModelDto.cs
//  程式名稱：
//  用於查詢Model
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-10-24       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  用於查詢Model
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.ERA.Model
{
    public class SearchModelDto
    {
        public SearchModelDto()
        {
            EOC_ID = string.Empty;
            PRJ_NO = string.Empty;
            RPT_MAIN_ID = 0;
        }

        public string RPT_CODE { get; set; }

        public string EOC_ID { get; set; }

        public string PRJ_NO { get; set; }

        public int RPT_MAIN_ID { get; set; }

        public string format { get; set; }
    }
}
