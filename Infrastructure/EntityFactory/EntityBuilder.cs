/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：EntityBuilder
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/27/2018 10:22:12 AM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Infrastructure.EntityFactory
{
    public static class EntityBuilder
    {
        /// <summary>
        /// 处理绑定复杂类型的特性
        /// </summary>
        private class ReferencedTypeBindingInfo
        {
            private ReferencedEntityAttribute e_ReferencedEntityAttribute;
            private PropertyInfo e_PropertyInfo;

            public ReferencedTypeBindingInfo(ReferencedEntityAttribute attri, PropertyInfo propertyInfo)
            {
                e_ReferencedEntityAttribute = attri;
                e_PropertyInfo = propertyInfo;
            }

            /// <summary>
            /// 获取属性类型
            /// </summary>
            public Type Type
            {
                get { return e_ReferencedEntityAttribute.Type; }
            }

            /// <summary>
            /// 获取前缀
            /// </summary>
            public string Prefix
            {
                get { return e_ReferencedEntityAttribute.Prefix; }
            }

            /// <summary>
            /// 获取条件属性
            /// </summary>
            public string ConditionalProperty
            {
                get { return e_ReferencedEntityAttribute.ConditionalProperty; }
            }

            /// <summary>
            /// 获取属性信息
            /// </summary>
            public PropertyInfo PropertyInfo
            {
                get { return e_PropertyInfo; }
            }
        }

        /// <summary>
        /// 处理绑定简单类型的特性
        /// </summary>
        private class PropertyDataBindingInfo
        {
            public DataMappingAttribute DataMapping;
            public PropertyInfo PropertyInfo;
            public PropertyDataBindingInfo(DataMappingAttribute mapping, PropertyInfo propertyInfo)
            {
                DataMapping = mapping;
                PropertyInfo = propertyInfo;
            }
        }


        private static readonly Type s_RootType = typeof(Object);
        private static object s_SyncMappingInfo = new object();
        private static Dictionary<Type, Dictionary<string, PropertyDataBindingInfo>> s_TypeMappingInfo = new Dictionary<Type, Dictionary<string, PropertyDataBindingInfo>>();
        private static Dictionary<Type, List<ReferencedTypeBindingInfo>> s_TypeReferencedList = new Dictionary<Type, List<ReferencedTypeBindingInfo>>();
        private static Dictionary<Type, Dictionary<string, DataMappingAttribute>> s_TypePropertyInfo = new Dictionary<Type, Dictionary<string, DataMappingAttribute>>();


        internal static T BuildEntity<T>(IDataReader dr) where T : class, new()
        {
            return BuildEntity<T>(new DataReaderEntitySource(dr), string.Empty);
        }

        public static T BuildEntity<T>(DataRow dr) where T : class, new()
        {
            return BuildEntity<T>(new DataRowEntitySource(dr), string.Empty);
        }

        public static List<T> BuildEntityList<T>(DataRow[] rows) where T : class, new()
        {
            if (rows == null)
            {
                return new List<T>(0);
            }
            List<T> list = new List<T>(rows.Length);
            foreach (DataRow row in rows)
            {
                list.Add(BuildEntity<T>(row));
            }
            return list;
        }

        public static List<T> BuildEntityList<T>(DataTable table) where T : class, new()
        {
            if (table == null)
            {
                return new List<T>(0);
            }
            List<T> list = new List<T>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
            {
                list.Add(BuildEntity<T>(row));
            }
            return list;
        }

        private static T BuildEntity<T>(IEntityDataSource ds, string prefix) where T : class, new()
        {
            T obj = new T();
            FillEntity(ds, obj, typeof(T), prefix);
            return obj;
        }

        internal static Object BuildEntity(IEntityDataSource ds, Type type, string prefix)
        {
            Object obj = Activator.CreateInstance(type);
            FillEntity(ds, obj, type, prefix);
            return obj;
        }

        internal static Object BuildEntityList(IEntityDataSource ds, Type type, string prefix)
        {
            Object obj = Activator.CreateInstance(type);
            foreach (var item in ds)
            {
                FillEntity(ds, obj, type, prefix);
            }
            return obj;
        }

        private static void FillEntity(IEntityDataSource ds, Object obj, Type type, string prefix)
        {
            Type baseType = type == null ? typeof(Object) : type.BaseType;
            if (!s_RootType.Equals(baseType))
            {
                FillEntity(ds, obj, baseType, prefix);
            }
            DoFillEntity(ds, obj, type, prefix);
        }

        private static void DoFillEntity(IEntityDataSource ds, Object obj, Type type, string prefix)
        {
            //装载属性的值——简单类型BaseType的
            foreach (string columnName in ds)
            {
                string mappingName = columnName.ToUpper();

                if (!String.IsNullOrEmpty(prefix))
                {
                    if (mappingName.StartsWith(prefix.ToUpper()))
                    {
                        mappingName = mappingName.Substring(prefix.Length);
                    }
                }

                if (String.IsNullOrEmpty(prefix))
                {
                    prefix = string.Empty;
                }

                PropertyDataBindingInfo propertyBindingInfo = GetPropertyInfo(type, mappingName);

                if (propertyBindingInfo == null || columnName.ToUpper() != (prefix.ToUpper() + mappingName))
                    continue;

                if (ds[columnName] != DBNull.Value && ValidateData(propertyBindingInfo, ds[columnName]))
                {
                    Object val = ds[columnName];
                    if (propertyBindingInfo.PropertyInfo.PropertyType == typeof(string))
                    {
                        val = val.ToString().Trim();
                    }
                    propertyBindingInfo.PropertyInfo.SetValue(obj, val, null);
                }
                else if (ds[columnName] == DBNull.Value && ValidateData(propertyBindingInfo, ds[columnName]) && propertyBindingInfo.PropertyInfo.PropertyType == typeof(string))
                {
                    //这个是对String类型的特殊处理，防止为null，其他的都以默认值展示
                    propertyBindingInfo.PropertyInfo.SetValue(obj, string.Empty, null);
                }
            }

            // fill referenced objects
            List<ReferencedTypeBindingInfo> refList = GetReferenceObjects(type);
            foreach (ReferencedTypeBindingInfo refObj in refList)
            {
                if (refObj.Type is IList)
                {
                    refObj.PropertyInfo.SetValue(obj, BuildEntityList(ds, refObj.Type, refObj.Prefix), null);
                }
                else
                {
                    refObj.PropertyInfo.SetValue(obj, BuildEntity(ds, refObj.Type, refObj.Prefix), null);
                }

            }
        }

        private static bool TryFill(IEntityDataSource ds, ReferencedTypeBindingInfo refObj)
        {
            if (string.IsNullOrEmpty(refObj.ConditionalProperty))
            {
                return true;
            }
            string columnName = GetBindingColumnName(refObj.Type, refObj.ConditionalProperty, refObj.Prefix);
            if (columnName == null)
            {
                return false;
            }
            return ds.ContainsColumn(columnName);
        }

        private static string GetBindingColumnName(Type type, string propertyName, string prefix)
        {
            Dictionary<string, DataMappingAttribute> propertyInfos;
            string name = null;
            try
            {
                s_TypePropertyInfo.TryGetValue(type, out propertyInfos);
                if (propertyInfos == null)
                {
                    lock (s_SyncMappingInfo)
                    {
                        s_TypePropertyInfo.TryGetValue(type, out propertyInfos);
                        if (propertyInfos == null)
                        {
                            AddTypeInfo(type);
                            propertyInfos = s_TypePropertyInfo[type];
                        }
                    }
                }
                DataMappingAttribute mapping;
                propertyInfos.TryGetValue(propertyName, out mapping);
                if (mapping == null)
                {
                    name = null;
                }
                else
                {
                    name = mapping.ColumnName;
                }
            }
            catch
            {
                name = null;
            }

            if (name == null)
            {
                if (!s_RootType.Equals(type.BaseType) && !s_RootType.Equals(type))
                {
                    return GetBindingColumnName(type.BaseType, propertyName, prefix);
                }
                else
                {
                    return null;
                }
            }
            return prefix + name;
        }

        private static PropertyDataBindingInfo GetPropertyInfo(Type type, string columnName)
        {
            Dictionary<string, PropertyDataBindingInfo> propertyInfoList;
            try
            {
                s_TypeMappingInfo.TryGetValue(type, out propertyInfoList);
                if (propertyInfoList == null)
                {
                    lock (s_SyncMappingInfo)
                    {
                        s_TypeMappingInfo.TryGetValue(type, out propertyInfoList);
                        if (propertyInfoList == null)
                        {
                            AddTypeInfo(type);
                            propertyInfoList = s_TypeMappingInfo[type];
                        }
                    }
                }
            }
            catch
            {
                return null;
            }

            PropertyDataBindingInfo info;
            propertyInfoList.TryGetValue(columnName, out info);
            return info;
        }

        private static List<ReferencedTypeBindingInfo> GetReferenceObjects(Type type)
        {
            List<ReferencedTypeBindingInfo> list;

            if (!s_TypeReferencedList.TryGetValue(type, out list))
            {
                lock (s_SyncMappingInfo)
                {
                    s_TypeReferencedList.TryGetValue(type, out list);
                    if (list == null)
                    {
                        AddTypeInfo(type);
                        list = s_TypeReferencedList[type];
                    }
                }
            }
            return list;
        }

        private static void GetTypeInfo(Type type, out Dictionary<string, PropertyDataBindingInfo> dataMappingInfos,
                                        out List<ReferencedTypeBindingInfo> referObjs,
                                        out Dictionary<string, DataMappingAttribute> propertyInfos)
        {
            PropertyInfo[] propertyList = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            dataMappingInfos = new Dictionary<string, PropertyDataBindingInfo>();
            referObjs = new List<ReferencedTypeBindingInfo>();
            propertyInfos = new Dictionary<string, DataMappingAttribute>(new CaseInsensitiveStringEqualityComparer());

            foreach (PropertyInfo propertyInfo in propertyList)
            {
                //获取所有的特性
                Object[] attributes = propertyInfo.GetCustomAttributes(false);
                foreach (Object attribute in attributes)
                {
                    // 处理简单类型
                    if (attribute is DataMappingAttribute)
                    {
                        DataMappingAttribute obj = attribute as DataMappingAttribute;
                        dataMappingInfos[obj.ColumnName.ToUpper()] = new PropertyDataBindingInfo(obj, propertyInfo);

                        propertyInfos.Add(propertyInfo.Name, obj);
                        continue;
                    }

                    // 处理复杂类型
                    if (attribute is ReferencedEntityAttribute)
                    {
                        ReferencedEntityAttribute obj = attribute as ReferencedEntityAttribute;
                        referObjs.Add(new ReferencedTypeBindingInfo(obj, propertyInfo));
                    }
                }
            }
        }

        private static void AddTypeInfo(Type type)
        {

            Dictionary<Type, Dictionary<string, PropertyDataBindingInfo>> newMappingList =
                new Dictionary<Type, Dictionary<string, PropertyDataBindingInfo>>(s_TypeMappingInfo);
            Dictionary<Type, List<ReferencedTypeBindingInfo>> newReferencedObjects =
                new Dictionary<Type, List<ReferencedTypeBindingInfo>>(s_TypeReferencedList);
            Dictionary<Type, Dictionary<string, DataMappingAttribute>> newPropertyList =
                new Dictionary<Type, Dictionary<string, DataMappingAttribute>>(s_TypePropertyInfo);

            Dictionary<string, PropertyDataBindingInfo> mappingInfos;
            List<ReferencedTypeBindingInfo> referObjs;
            Dictionary<string, DataMappingAttribute> propertyInfos;

            GetTypeInfo(type, out mappingInfos, out referObjs, out propertyInfos);

            newMappingList[type] = mappingInfos;
            newReferencedObjects[type] = referObjs;
            newPropertyList[type] = propertyInfos;

            s_TypeMappingInfo = newMappingList;
            s_TypeReferencedList = newReferencedObjects;
            s_TypePropertyInfo = newPropertyList;
        }

        private static bool ValidateData(PropertyDataBindingInfo bindingInfo, Object dbValue)
        {
            //todo: implements in AOP?
            return true;
        }
    }
}
