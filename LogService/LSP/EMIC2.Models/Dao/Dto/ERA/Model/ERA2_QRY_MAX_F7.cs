///////////////////////////////////////////////////////////////////////////////////////
// 程式檔名：
//  ERA2_QRY_MAX_F7.cs
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
    public class ERA2_QRY_MAX_F7 : BaseModelDto
    {
        public string UNDER_ORGAN { get; set; }

        public string LOCATION { get; set; }

        public DateTime? DAMAGE_DATETIME { get; set; }

        public string DAMAGE_STATUS { get; set; }

        public string PROCESS { get; set; }

        public int? BUILD_COST { get; set; }

        public string MEMO { get; set; }

        public DateTime? INPUT_DATETIME { get; set; }
    }
}
