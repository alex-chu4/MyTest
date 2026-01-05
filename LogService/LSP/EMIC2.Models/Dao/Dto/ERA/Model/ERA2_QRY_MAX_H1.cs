///////////////////////////////////////////////////////////////////////////////////////
// 程式檔名：
//  ERA2_QRY_MAX_H1.cs
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
    public class ERA2_QRY_MAX_H1 : BaseModelDto
    {
        public int? MILITARY_CASES { get; set; }

        public int? AIRBORNE_CASES { get; set; }

        public int? MILITARY_SUPPLY { get; set; }

        public int? AIRBORNE_SUPPLY { get; set; }

        public int? MILITARY_TRANSPORT { get; set; }

        public int? AIRBORNE_TRANSPORT { get; set; }

        public int? MILITARY_INJURED { get; set; }

        public int? AIRBORNE_INJURED { get; set; }

        public int? MILITARY_SURVEY { get; set; }

        public int? AIRBORNE_SURVEY { get; set; }

        public int? MILITARY_DEAD { get; set; }

        public int? AIRBORNE_DEAD { get; set; }

        public int? MILITARY_WORKERS { get; set; }

        public int? AIRBORNE_WORKERS { get; set; }

        public int? MILITARY_FLIGHTS { get; set; }

        public int? AIRBORNE_FLIGHTS { get; set; }
    }
}
