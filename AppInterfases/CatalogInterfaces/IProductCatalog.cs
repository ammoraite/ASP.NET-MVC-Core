namespace Interfases.CatalogInterfaces
{
    public interface IproductCatalog : ICatalog<IProduct>
    {
        public bool ContainsProductInCatalog ( IProduct product );
        public bool AddProductInCatalog ( IProduct product );
        public bool RemoveProductInCatalog ( IProduct product );
    }
}
