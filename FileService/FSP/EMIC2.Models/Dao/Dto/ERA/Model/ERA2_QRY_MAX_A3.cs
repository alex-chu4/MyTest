///////////////////////////////////////////////////////////////////////////////////////
// 程式檔名：
//  ERA2_QRY_MAX_A3.cs
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
    public class ERA2_QRY_MAX_A3 : BaseModelDto
    {
        public int? TRAPPED { get; set; }

        public int? RESCUES { get; set; }

        public int? WATER_SUPPLIES { get; set; }

        public int? FIRE_STAFF { get; set; }

        public int? FIRE_VEHICLES { get; set; }

        public int? FIRE_BOAT { get; set; }

        public int? FIRE_HELICOPTERS { get; set; }

        public int? FIRE_VOL_STAFF { get; set; }

        public int? FIRE_VOL_VEHICLES { get; set; }

        public int? FIRE_VOL_BOAT { get; set; }

        public int? FOLK_STAFF { get; set; }

        public int? FOLK_VEHICLES { get; set; }

        public int? FOLK_BOAT { get; set; }

        public int? SEARCH_STAFF { get; set; }

        public int? SEARCH_VEHICLES { get; set; }

        public int? SEARCH_BOAT { get; set; }

        public int? POLICE_STAFF { get; set; }

        public int? POLICE_VEHICLES { get; set; }

        public int? POLICE_BOAT { get; set; }

        public int? POLICE_VOL_STAFF { get; set; }

        public int? POLICE_VOL_VEHICLES { get; set; }

        public int? MILITARY_VOL_STAFF { get; set; }

        public int? MILITARY_VOL_VEHICLES { get; set; }

        public int? MILITARY_STAFF { get; set; }

        public int? MILITARY_VEHICLES { get; set; }

        public int? MILITARY_BOAT { get; set; }

        public int? MILITARY_HELICOPTERS { get; set; }

        public int? OTHERS { get; set; }
    }
}
