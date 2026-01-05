///////////////////////////////////////////////////////////////////////////////////////
// 程式檔名：
//  ERA2_QRY_MAX_E1.cs
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
    public class ERA2_QRY_MAX_E1 : BaseModelDto
    {
        public int? GAS_DAMAGE_USED { get; set; }

        public int? GAS_DAMAGE_NOW { get; set; }

        public string GAS_DAMAGE_REASON { get; set; }

        public string FIX_DATETIME_EST { get; set; }

        public string GAS_DAMAGE_AREA { get; set; }

        public string MEMO { get; set; }
    }
}
