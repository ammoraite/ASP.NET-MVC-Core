
using AmmoraiteCollections;

namespace Interfases
{
    public interface IProductCategory : ICategory<IProduct>
    {
        public int ProductCatergoryId { get; set; }
        public string? ProductCatergoryName { get; set; }
        public ConcurrentList<IProduct> Products { get; set; }
    }
}
