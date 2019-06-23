/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：RoleInfo
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/27/2018 10:36:16 AM
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
    public class RoleInfo
    {
        [DataMapping("RoleID", DbType.Int32)]
        public int RoleID { get; set; }

        [DataMapping("RoleName", DbType.String)]
        public string RoleName { get; set; }

        [DataMapping("MenuIDS", DbType.String)]
        public string MenuIDs { get; set; }

        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }

        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }
    }
}
