/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：ProvinceInfo
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：5/2/2018 11:12:13 AM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using Infrastructure.EntityFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataModel
{
    public class ProvinceInfo
    {
        [DataMapping("ProvinceID", DbType.Int32)]
        public int ProvinceID { get; set; }


        [DataMapping("ProvinceName", DbType.String)]
        public string ProvinceName { get; set; }
    }
}
