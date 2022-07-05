namespace Interfases
{
    public interface IproductCatalog : ICatalog<IProduct>
    {
        public bool ContainsProduct ( IProduct product );

    }
}
