using Interfases;

using RazorApp1.Models.Entityes;

namespace RazorApp1.Models
{
    public interface IProductCatalog : ICatalog<int, Product>
    {
        //public bool ContainsProductInCatalog ( Product product );
        public Task AddProductInCatalog ( Product product, CancellationToken cancellationToken );
        public Task RemoveProductInCatalog ( Product product, CancellationToken cancellationToken );
    }
}
