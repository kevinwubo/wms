using Infrastructure.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.BaseBiz
{
    public class BaseService
    {
        protected static CacheRuntime Cache
        {
            get
            {
                CacheRuntime cache = new CacheRuntime();
                cache.ExpiredTimespan = 60;//设置60分钟过期
                return cache;
            }
        }
    }
}
