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
    public class ERA2_QRY_MAX_F22 : BaseModelDto
    {
        public string RAILTYPE { get; set; }

        public string RAILLINE { get; set; }

        public string CITY_NAME { get; set; }

        public string TOWN_NAME { get; set; }

        public string ADDNAME { get; set; }

        public string CLOSE_TEXT { get; set; }

        public string OPEN_TEXT { get; set; }

        public DateTime? CLOSE_DATETIME { get; set; }

        public DateTime? OPEN_DATETIME { get; set; }

        public DateTime? INPUT_DATETIME { get; set; }
    }
}
