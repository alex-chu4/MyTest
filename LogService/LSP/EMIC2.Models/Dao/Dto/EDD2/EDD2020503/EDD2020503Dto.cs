///////////////////////////////////////////////////////////////////////////////////////
//  程式名稱：EDD2020504Service.cs
//  資源項目分類
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員      日期       方法名稱      版本      功能說明
//  Joe        2019-10-23  資源項目分類    1.0.0   資源項目分類
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020503
{
    public class EDD2020503Dto : BaseModelDto
    {
        /// <summary>
        /// Gets or sets 資源項目(大類) -1=全部 OR 指定的MASTER_TYPE_ID
        /// </summary>
        public string MASTER_TYPE_ID { get; set; }

        /// <summary>
        /// Gets or sets 資源項目(中類) -1=全部 OR 指定的SECONDARY_TYPE_ID
        /// </summary>
        public string SECONDARY_TYPE_ID { get; set; }

        /// <summary>
        /// Gets or sets 資源項目(細類) -1=全部 OR 指定的DETAIL_TYPE_ID
        /// </summary>
        public string DETAIL_TYPE_ID { get; set; }

        /// <summary>
        /// Gets or sets 資源項目
        /// </summary>
        public string RESOURCE_ID { get; set; }

        /// <summary>
        /// Gets or sets 資源項目名稱
        /// </summary>
        public string RESOURCE_NAME { get; set; }
    }
}
