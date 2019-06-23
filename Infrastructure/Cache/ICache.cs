using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Cache
{
    public interface ICache
    {
        int ExpiredTimespan { get; set; }

        void Add<T>(string key, T value);

        void Remove(string key);

        void Set<T>(string key, T value);

        T Get<T>(string key);
    }
}
