using AmmoraiteCollections;

namespace Interfases
{
    public interface ICatalog<T>
    {
        private ConcurrentList<T> Products { get => Products; set => Products=value; }
    }
}
