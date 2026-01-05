///////////////////////////////////////////////////////////////////////////////////////
// 程式檔名：
//  ERA2_QRY_MAX_D3.cs
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
    public class ERA2_QRY_MAX_D3 : BaseModelDto
    {
        public string SHELTER { get; set; }

        public DateTime? OPEN_DATETIME { get; set; }

        public DateTime? CLOSE_DATETIME { get; set; }

        public int? REFUGEE { get; set; }

        public int? REFUGEE_M { get; set; }

        public int? REFUGEE_F { get; set; }

        public int? REFUGEE_TOTAL { get; set; }

        public int? REFUGEE_TOTAL_M { get; set; }

        public int? REFUGEE_TOTAL_F { get; set; }

        public int? LAST_REFUGEE_TOTAL { get; set; }

        public int? SUPPLY_PEOPLE { get; set; }

        public int? SUPPLY_DAY { get; set; }

        public string KEEP_SUPPLY { get; set; }

        public string CONTACT { get; set; }

        public string CONTACT_PHONE { get; set; }

        public int? PEOPLE_NO { get; set; }
    }
}
