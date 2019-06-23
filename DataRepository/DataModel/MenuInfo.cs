/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：MenuInfo
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/27/2018 10:36:34 AM
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
    public class MenuInfo
    {
        [DataMapping("MenuID", DbType.Int32)]
        public int MenuID { get; set; }

        [DataMapping("MenuName", DbType.String)]
        public string MenuName { get; set; }

        [DataMapping("URL", DbType.String)]
        public string URL { get; set; }

        [DataMapping("Status", DbType.Int32)]
        public int Status { get; set; }

        [DataMapping("Remark", DbType.String)]
        public string Remark { get; set; }

        [DataMapping("PreFlag", DbType.String)]
        public string PreFlag { get; set; }


        [DataMapping("SufFlag", DbType.String)]
        public string SufFlag { get; set; }


        [DataMapping("GroupCode", DbType.String)]
        public string GroupCode { get; set; }


        [DataMapping("CreateDate", DbType.DateTime)]
        public DateTime CreateDate { get; set; }
    }
}
