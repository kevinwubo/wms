/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：ReferencedEntityAttribute
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/27/2018 10:23:29 AM
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
    public class ReferencedEntityAttribute : Attribute
    {
        #region Property

        private Type e_type;
        private string prefix;
        private string conditionalproperty;
        public Type Type
        {
            get { return e_type; }
        }

        public string Prefix
        {
            set { prefix = value; }
            get { return prefix; }
        }

        public string ConditionalProperty
        {
            set { conditionalproperty = value; }
            get { return conditionalproperty; }
        }

        #endregion

        public ReferencedEntityAttribute(Type type)
        {
            e_type = type;
        }
    }
}
