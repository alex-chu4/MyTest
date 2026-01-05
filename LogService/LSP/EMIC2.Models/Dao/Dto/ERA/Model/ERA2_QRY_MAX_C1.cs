///////////////////////////////////////////////////////////////////////////////////////
// 程式檔名：
//  ERA2_QRY_MAX_C1.cs
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
    public class ERA2_QRY_MAX_C1 : BaseModelDto
    {
        public decimal? TOTAL { get; set; }

        public decimal? SUBTOTAL_LOST { get; set; }

        public decimal? FARM_LOST { get; set; }

        public decimal? LIVESTOCK_LOST { get; set; }

        public decimal? FISH_LOST { get; set; }

        public decimal? FOREST_LOST { get; set; }

        public decimal? SUBTOTAL_FACTY { get; set; }

        public decimal? FARM_LOST_FACTY { get; set; }

        public decimal? LIVESTOCK_LOST_FACTY { get; set; }

        public decimal? FISH_LOST_FACTY { get; set; }
    }
}
