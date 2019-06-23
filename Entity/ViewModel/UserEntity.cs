/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：UserView
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/27/2018 10:38:22 AM
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
    public class UserEntity
    {
        public long UserID { get; set; }

        public string UserName { get; set; }

        public string NickName { get; set; }

        public int Status { get; set; }

        public List<RoleEntity> Roles { get; set; }

        public List<GroupEntity> Groups { get; set; }

        public List<MenuEntity> Menus { get; set; }

    }
}
