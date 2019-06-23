/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：City
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：5/2/2018 11:16:24 AM
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
    public class City
    {
        public int CityID { get; set; }

        public string CityName { get; set; }

        public int ProvinceID { get; set; }

        public Province ProvinceInfo { get; set; }
    }
}
