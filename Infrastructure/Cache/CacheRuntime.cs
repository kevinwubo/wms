/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：CacheRuntime
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/26/2018 9:38:47 AM
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
using System.Web;

namespace Infrastructure.Cache
{
    public class CacheRuntime : ICache
    {
        public int ExpiredTimespan
        {
            get;
            set;
        }

        public void Add<T>(string key, T value)
        {
            if (value != null)
            {
                string realKey = key;
                DateTime expires = DateTime.MaxValue;

                if (ExpiredTimespan > 0)
                {
                    expires = DateTime.Now.AddMinutes(ExpiredTimespan);
                }

                HttpRuntime.Cache.Insert(realKey, value, null, expires, System.Web.Caching.Cache.NoSlidingExpiration);
            }
        }

        public void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        public void Set<T>(string key, T value)
        {
            this.Add(key, value);
        }

        public T Get<T>(string key)
        {
            return (T)HttpRuntime.Cache.Get(key);
        }


    }
}
