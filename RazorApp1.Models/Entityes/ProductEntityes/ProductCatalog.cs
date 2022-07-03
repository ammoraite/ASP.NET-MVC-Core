using AmmoraiteCollections;

using Interfases;
using Interfases.CatalogInterfaces;

namespace RazorApp1.Models
{
    public class ProductCatalog : IproductCatalog
    {
        public ConcurrentList<IProductCategory> Catergories { get; set; } = new ( );
        public bool ContainsCategory ( IProductCategory category ) =>
            Catergories.Contains (x =>
                 x!=null&&
                 x.ProductCatergoryId==category.ProductCatergoryId&&
                 x.ProductCatergoryName==category.ProductCatergoryName);
    }
}
