///////////////////////////////////////////////////////////////////////////////////////
// 程式檔名：
//  ERA2_QRY_MAX_F1_S.cs
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
    public class ERA2_QRY_MAX_F1_S : BaseModelDto
    {
        public string ROADTYPE_ID { get; set; }

        public int? BLOCK_TOTAL { get; set; }

        public int? UNBLOCK_COUNT { get; set; }

        public int? BLOCK_COUNT { get; set; }

        public int? STOP_TOTAL { get; set; }

        public int? UNSTOP_COUNT { get; set; }

        public int? STOP_COUNT { get; set; }

        public string ORG_CHECK { get; set; }

        public string COMMENT { get; set; }
    }
}
