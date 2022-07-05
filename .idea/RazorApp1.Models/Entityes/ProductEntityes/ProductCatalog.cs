using System.Collections.Concurrent;

using AmmoraiteCollections;

using Interfases;

namespace RazorApp1.Models
{
    public class ProductCatalog : ICatalog<IProduct>, IproductCatalog
    {
        private ConcurrentDictionary<int, IProduct> Categories { get; set; } = new ( );
        private TasksQueue CatalogTasks { get; set; }

        public ProductCatalog ( int SiseQueueCatalogTasks )
        {
            CatalogTasks=new (SiseQueueCatalogTasks);
        }
        public ProductCatalog ( )
        {
            CatalogTasks=new ( );
        }
        public void AddCategory ( IProduct item ) => CatalogTasks.AddTask (new Task (( ) =>
                    Categories.AddOrUpdate (item.ProductId, item, ( key, oldValue ) => item=oldValue)));
        public bool ContainsProduct ( IProduct product )=> Categories.ContainsKey (product.ProductId);
        public IEnumerable<IProduct> GetCategories ( )
        {
            foreach (var item in Categories)
            {
                yield return item.Value;
            }
        }
        public void RemoveCategory ( IProduct item ) => CatalogTasks.AddTask (new Task (( ) => Categories.Remove(item.ProductId,out item)));
    }
}
