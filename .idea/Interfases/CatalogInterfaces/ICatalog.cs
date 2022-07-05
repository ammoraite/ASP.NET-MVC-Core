using AmmoraiteCollections;

namespace Interfases
{
    public interface ICatalog<T>
    {
        private ConcurrentList<T> Catergories { get => Catergories; set => Catergories=value; }
        private TasksQueue CatalogTasks { get => CatalogTasks; set => CatalogTasks=value; }
        public void AddCategory ( T item);
        public void RemoveCategory ( T item );
        public IEnumerable<T> GetCategories ();
    }
}
