///////////////////////////////////////////////////////////////////////////////////////
// 程式檔名：
//  ERA2_QRY_MAX_C3.cs
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
    public class ERA2_QRY_MAX_C3 : BaseModelDto
    {
        public string REFUGE_CITY { get; set; }

        public string LOCATION { get; set; }

        public int? BOATS { get; set; }

        public int? SAILORS { get; set; }

        public int? SAILORS_ONBOAT { get; set; }

        public int? SAILORS_ONSHORE { get; set; }

        public string MEMO { get; set; }
    }
}
