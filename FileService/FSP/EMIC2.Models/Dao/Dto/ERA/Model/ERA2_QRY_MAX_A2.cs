///////////////////////////////////////////////////////////////////////////////////////
// 程式檔名：
//  ERA2_QRY_MAX_A2.cs
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
    public class ERA2_QRY_MAX_A2 : BaseModelDto
    {
        public int LOW_LEVEL { set; get; }

        public int MOUNTAIN { set; get; }

        public int SEASIDE { set; get; }

        public int RIVERS { set; get; }

        public int BUILDS { set; get; }

        public int OTHERS { set; get; }

        public int SUGGESTS_ALL { set; get; }

        public int SUGGESTS_NPA { set; get; }

        public int SUGGESTS_NFA { set; get; }

        public int SUGGESTS_CGA { set; get; }

        public int SUGGESTS_GOV { set; get; }

        public int SUGGESTS_OTR { set; get; }

        public int EXECUTES_ALL { set; get; }

        public int EXECUTES_NPA { set; get; }

        public int EXECUTES_NFA { set; get; }

        public int EXECUTES_CGA { set; get; }

        public int EXECUTES_GOV { set; get; }

        public int EXECUTES_OTR { set; get; }

        public int RELEIEVES { set; get; }

        public string MEMO { set; get; }
    }
}
