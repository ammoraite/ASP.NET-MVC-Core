using AmmoraiteCollections;

namespace Interfases
{
    public interface ICatalog<T>
    {
        public ConcurrentList<T> Catergories { get; set; }

    }
}
