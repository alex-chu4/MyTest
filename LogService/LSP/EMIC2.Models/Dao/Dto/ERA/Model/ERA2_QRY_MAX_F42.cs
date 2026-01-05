///////////////////////////////////////////////////////////////////////////////////////
// 程式檔名：
//  ERA2_QRY_MAX_F2.cs
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
    public class ERA2_QRY_MAX_F42 : BaseModelDto
    {
        public string PORT_AUT { get; set; }

        public string BEACH_LOCAL { get; set; }

        public DateTime? HAPPEN_DATETIME { get; set; }

        public string SHIP_NAME { get; set; }

        public string DAMAGE_STATUS { get; set; }

        public string REPAIRE_TEXT { get; set; }

        public string REMARK { get; set; }

        public DateTime? INPUT_DATETIME { get; set; }
    }
}
