using Interfases;

namespace RazorApp1.Models.Entityes
{
    public interface IproductCatalog : ICatalog<int,Product>
    {
        public bool ContainsProductInCatalog ( Product product );
        public bool AddProductInCatalog ( Product product );
        public bool RemoveProductInCatalog ( Product product );
    }
}
