///////////////////////////////////////////////////////////////////////////////////////
// 程式檔名：
//  ERA2_QRY_MAX_C4.cs
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
    public class ERA2_QRY_MAX_C4 : BaseModelDto
    {
        public int? AMBER_RIVERS { get; set; }

        public int? AMBER_TOWNSHIP { get; set; }

        public int? AMBER_VILLAGE { get; set; }

        public int? RED_RIVERS { get; set; }

        public int? RED_TOWNSHIP { get; set; }

        public int? RED_VILLAGE { get; set; }

        public int? TOTAL { get; set; }

        public string STATUS { get; set; }
    }
}
