/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：CommonHelper
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/26/2018 9:39:56 AM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Infrastructure.Helper
{
    public static class CommonHelper
    {
        public static bool IsEmpty(this string target)
        {
            return string.IsNullOrEmpty(target);
        }

        public static bool IsEmpty<T>(this ICollection<T> target)
        {
            return target == null || target.Count == 0;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {

            //创建属性的集合    
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口    

            Type type = typeof(T);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列    
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in list)
            {
                //创建一个DataRow实例    
                DataRow row = dt.NewRow();
                //给row 赋值    
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                //加入到DataTable    
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static List<T> Merge<T>(this IEnumerable<T> source, IEnumerable<T> target,IEqualityComparer<T> compare)
        {
            List<T> mergedList = new List<T>(source);
            if (compare != null)
            {
                mergedList.AddRange(target.Except(source, compare));
            }
            else
            {
                mergedList.AddRange(target.Except(source));
            }
           
            return mergedList;
        }

         public static bool IsDefault<T>(this T value) 
         {
             return EqualityComparer<T>.Default.Equals(value, default(T)); 
         }
    }
}
