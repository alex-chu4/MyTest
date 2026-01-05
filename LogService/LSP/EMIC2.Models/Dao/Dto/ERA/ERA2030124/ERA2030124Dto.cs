///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2030124Dto.cs
//  程式名稱：
//  ERA2030124Dto
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-08-28       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2030124Dto
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.ERA
{
    public class ERA2030124Dto : ERA2Dto
    {
        public ERA2030124Dto()
        {
            this.CROP = 0;
            this.CRITTERBIRDS = 0;
            this.FISHERY = 0;
            this.FORESTRY = 0;
            this.CROPEST = 0;
            this.CRITTERBIRDSEST = 0;
            this.FISHERYEST = 0;
        }

        /// <summary>
        /// Gets or sets 農產
        /// </summary>
        [Display(Name = "農產")]
        public decimal? CROP { get; set; }

        /// <summary>
        /// Gets or sets 畜禽
        /// </summary>
        [Display(Name = "畜禽")]
        public decimal? CRITTERBIRDS { get; set; }

        /// <summary>
        /// Gets or sets 漁產
        /// </summary>
        [Display(Name = "漁產")]
        public decimal? FISHERY { get; set; }

        /// <summary>
        /// Gets or sets 林產
        /// </summary>
        [Display(Name = "林產")]
        public decimal? FORESTRY { get; set; }

        /// <summary>
        /// Gets or sets 農田及農業設施
        /// </summary>
        [Display(Name = "農田及農業設施")]
        public decimal? CROPEST { get; set; }

        /// <summary>
        /// Gets or sets 畜禽設施
        /// </summary>
        [Display(Name = "畜禽設施")]
        public decimal? CRITTERBIRDSEST { get; set; }

        /// <summary>
        /// Gets or sets 漁民漁業設施
        /// </summary>
        [Display(Name = "漁民漁業設施")]
        public decimal? FISHERYEST { get; set; }
    }
}
