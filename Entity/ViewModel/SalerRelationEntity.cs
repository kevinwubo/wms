/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：SalerRelationEntity
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：7/16/2018 4:16:24 PM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class SalerRelationEntity
    {

        public long CustomerID { get; set; }

        public long SalerID { get; set; }

        public string CustomerCode { get; set; }

        public string SalerCode { get; set; }

        public int Status { get; set; }

        public DateTime CreateDate { get; set; }

        public CustomerEntity customer { get; set; }
        /// <summary>
        /// 销售源头：门店:Store 业务员:Saler
        /// </summary>
        public string SalerSource { get; set; }
    }
}
