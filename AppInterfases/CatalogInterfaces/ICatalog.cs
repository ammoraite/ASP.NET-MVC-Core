
using System.Collections.Concurrent;

namespace Interfases
{
    public interface ICatalog<K, V>
    {
        private ConcurrentDictionary<K, V> Products { get => Products; set => Products=value; }
    }
}
