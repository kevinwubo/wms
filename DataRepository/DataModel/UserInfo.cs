/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：UserInfo
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/27/2018 10:27:29 AM
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
    [Serializable]
    public class UserInfo
    {
        [DataMapping("UserID", DbType.Int64)]
        public long UserID { get; set; }

        [DataMapping("UserName", DbType.String)]
        public string UserName { get; set; }

        [DataMapping("NickName", DbType.String)]
        public string NickName { get; set; }

        [DataMapping("RoleIDs", DbType.String)]
        public string RoleIDs { get; set; }

        [DataMapping("GroupIDs", DbType.String)]
        public string GroupIDs { get; set; }

        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }

        [DataMapping("Password", DbType.String)]
        public string Password { get; set; }

        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }


    }
}
