/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：DataMappingAttribute
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/27/2018 10:20:47 AM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFactory
{
    public class DataMappingAttribute : Attribute
    {
        private string columnname;
        private DbType dbtype;

        public DataMappingAttribute(string columnName, DbType dbType)
        {
            this.columnname = columnName;
            this.dbtype = dbType;
        }

        public string ColumnName
        {
            get { return columnname; }
        }

        public DbType DbType
        {
            get { return dbtype; }
        }
    }
}
