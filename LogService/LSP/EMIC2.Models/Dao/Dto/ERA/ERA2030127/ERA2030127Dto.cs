///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030108Dto.cs
//  程式名稱：
//  ERA2030108Dto
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-28       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2030108Dto
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.ERA
{
    public class ERA2030127Dto : ERA2Dto
    {
        public ERA2030127Dto()
        {
            this.TRAPPED = 0;
            this.RESCUES = 0;
            this.WATER_SUPPLIES = 0;
        }

        /// <summary>
        /// Gets or sets 縣市別
        /// </summary>
        public string CITY_NAME { get; set; }

        /// <summary>
        /// Gets or sets 受困人數(人)搶救災民人數(人)
        /// </summary>
        public int? TRAPPED { get; set; }

        /// <summary>
        /// Gets or sets 搶救災民人數(人)
        /// </summary>
        public int? RESCUES { get; set; }

        /// <summary>
        /// Gets or sets 支援送水勤務(次)
        /// </summary>
        public int? WATER_SUPPLIES { get; set; }

        /// <summary>
        /// Gets or sets 消防
        /// </summary>
        public string UNIT_NAME_1 { get; set; }

        /// <summary>
        /// Gets or sets 義消
        /// </summary>
        public string UNIT_NAME_2 { get; set; }

        /// <summary>
        /// Gets or sets 民間救難團體
        /// </summary>
        public string UNIT_NAME_3 { get; set; }

        /// <summary>
        /// Gets or sets 義勇特搜隊
        /// </summary>
        public string UNIT_NAME_4 { get; set; }

        /// <summary>
        /// Gets or sets 警察
        /// </summary>
        public string UNIT_NAME_5 { get; set; }

        /// <summary>
        /// Gets or sets 義警
        /// </summary>
        public string UNIT_NAME_6 { get; set; }

        /// <summary>
        /// Gets or sets 民防
        /// </summary>
        public string UNIT_NAME_7 { get; set; }

        /// <summary>
        /// Gets or sets 國軍
        /// </summary>
        public string UNIT_NAME_8 { get; set; }

        /// <summary>
        /// Gets or sets 其他
        /// </summary>
        public string UNIT_NAME_9 { get; set; }
    }
}
