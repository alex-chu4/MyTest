///////////////////////////////////////////////////////////////////////////////////////
// 程式檔名：
//  BaseModelDto.cs
//  程式名稱：
//  用於繼承Model
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-10-24       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  用於繼承Model
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.ERA.Model
{
    public class BaseModelDto
    {
        public int RPT_DETAIL_ID { get; set; }

        public int RPT_MAIN_ID { get; set; }

        public string CITY_NAME { get; set; }

        public string TOWN_NAME { get; set; }

        public string VILLAGE_NAME { get; set; }

        public string SHOW_ORDER { get; set; }

        public string NO_DATA_MARK { get; set; }

        public string CREATED_USER { get; set; }

        public DateTime? CREATED_TIME { get; set; }

        public string MODIFIED_USER { get; set; }

        public DateTime? MODIFIED_TIME { get; set; }
    }
}
