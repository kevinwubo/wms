/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：GroupEntity
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/28/2018 9:33:34 AM
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
    [Serializable]
    public class GroupEntity
    {
        public int GroupID { get; set; }

        public string GroupName { get; set; }

        public List<MenuEntity> Menus { get; set; }

        public int Status { get; set; }
    }
}
