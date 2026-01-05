///////////////////////////////////////////////////////////////////////////////////////
// 程式檔名：
//  ERA2_QRY_MAX_G1.cs
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
    public class ERA2_QRY_MAX_G1 : BaseModelDto
    {
        public int? SCHOOL_PRE { get; set; }

        public int? SCHOOL_PRIV { get; set; }

        public int? SCHOOL_J_HIGH { get; set; }

        public int? SCHOOL_S_HIGH { get; set; }

        public int? SCHOOL_UNIV { get; set; }

        public int? SCHOOL_EDU { get; set; }

        public int? TOTAL { get; set; }

        public int? DAMAGE_AMOUNT { get; set; }

        public int? FIX_BUDGET { get; set; }

        public int? INJURED { get; set; }

        public string SUSPEND_CLASSES { get; set; }

        public string MEMO { get; set; }
    }
}
