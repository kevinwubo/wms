/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：IEntityDataSource
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/27/2018 10:23:11 AM
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

namespace Infrastructure.EntityFactory
{
    internal interface IEntityDataSource : IEnumerable<string>, IDisposable
    {
        Object this[string columnName]
        {
            get;
        }

        Object this[int iIndex]
        {
            get;
        }

        bool ContainsColumn(string columnName);

    }
}
