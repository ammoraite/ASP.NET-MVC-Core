using Interfases;

namespace RazorApp1.Models.Entityes
{
    public interface IProductCatalog : ICatalog<int,Product>
    {
        public bool ContainsProductInCatalog ( Product product );
        public bool AddProductInCatalog ( Product product, CancellationToken cancellationToken );
        public bool RemoveProductInCatalog ( Product product );
    }
}
