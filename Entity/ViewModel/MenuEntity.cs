/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：MenuEntity
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/28/2018 9:33:21 AM
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
    public class MenuEntity
    {
        public int MenuID { get; set; }

        public string MenuName { get; set; }

        public string URL { get; set; }

        public int Status { get; set; }

        public string Remark { get; set; }

        public string PreFlag { get; set; }

        public string SufFlag { get; set; }

        public string GroupCode { get; set; }

        public BaseDataEntity BaseData { get; set; }

        public bool IsShow { get; set; }
    }
}
