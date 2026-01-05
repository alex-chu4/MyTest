///////////////////////////////////////////////////////////////////////////////////////
// 程式檔名：
//  ERA2_QRY_MAX_F1.cs
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
    public class ERA2_QRY_MAX_F1 : BaseModelDto
    {
        public string TRFSTATUS { get; set; }

        public string ROADTYPE_ID { get; set; }

        public string ROAD_ID { get; set; }

        public int? STARTMILE { get; set; }

        public int? STARTPLUS { get; set; }

        public int? ENDMILE { get; set; }

        public int? ENDPLUS { get; set; }

        public string LINECODE { get; set; }

        public string CITY_ID { get; set; }

        public string TOWN_ID { get; set; }

        public string ADDNAME { get; set; }

        public string WGS84_X { get; set; }

        public string WGS84_Y { get; set; }

        public string WGS84_X_SCTL { get; set; }

        public string WGS84_Y_SCTL { get; set; }

        public string WGS84_X_ECTL { get; set; }

        public string WGS84_Y_ECTL { get; set; }

        public string CLOSE_TYPE { get; set; }

        public string CLOSE_TSUB { get; set; }

        public DateTime? CLOSE_DATETIME { get; set; }

        public int? REPAIRE_DAY { get; set; }

        public string CLOSE_TEXT { get; set; }

        public string REPAIRE_TEXT { get; set; }

        public string ALT_ROUTE { get; set; }

        public int? BUILD_COST { get; set; }

        public string OPEN_TEXT { get; set; }

        public DateTime? OPEN_DATETIME { get; set; }

        public string REMARK { get; set; }

        public string BRG_CLOSE { get; set; }
    }
}
