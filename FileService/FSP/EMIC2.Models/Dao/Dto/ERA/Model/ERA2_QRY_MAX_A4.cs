///////////////////////////////////////////////////////////////////////////////////////
// 程式檔名：
//  ERA2_QRY_MAX_A4.cs
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
    public class ERA2_QRY_MAX_A4 : BaseModelDto
    {
        public string LOCATION { get; set; }

        public int? EVACUATE_EST { get; set; }

        public int? EVACUATE_REAL { get; set; }

        public int? EVACUATE_TOTAL { get; set; }

        public int? LAST_EVACUATE_TOTAL { get; set; }

        public DateTime? EVACUATE_TIME { get; set; }

        public string SHELTER { get; set; }

        public string MEMO { get; set; }

        public DateTime? KEYIN_TIME { get; set; }

        public string SHOW_ORDER { get; set; }

        public string NO_DATA_MARK { get; set; }

        public string LESS_MARK { get; set; }

        public string LESS_DATA { get; set; }
    }
}
