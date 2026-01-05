///////////////////////////////////////////////////////////////////////////////////////
// 程式檔名：
//  ERA2_QRY_MAX_E3.cs
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
    public class ERA2_QRY_MAX_E3 : BaseModelDto
    {
        public int? FIX_BUDGET { get; set; }

        public int? RECVY_BUDGET { get; set; }

        public int? BUSINESS_LOST { get; set; }

        public int? OTHERS_LOST { get; set; }

        public int? SUBTOTAL { get; set; }

        public int? TOTAL { get; set; }

        public int? DEADS { get; set; }

        public int? MISSING { get; set; }

        public int? INJURED { get; set; }
    }
}
