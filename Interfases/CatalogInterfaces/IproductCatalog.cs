namespace Interfases.CatalogInterfaces
{
    public interface IproductCatalog : ICatalog<IProductCategory>
    {
        public bool ContainsCategory ( IProductCategory category );

    }
}
