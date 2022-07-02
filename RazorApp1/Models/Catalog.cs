using System.Drawing;
using AmmoraiteCollections;
using Interfases;

namespace RazorApp1.Models
{
    public class ProductCatalog :ICatalog<ProductCategory>
    {
        public ConcurrentList<ProductCategory> Catergories { get; set; }=new();

        public bool ContainsCategory ( IProductCategory<Product> category)
        {
            foreach (var Category in Catergories)
            {
                if (Category.ProductCatergoryId==category.ProductCatergoryId&& 
                    Category.ProductCatergoryName==category.ProductCatergoryName)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class ProductCategory : IProductCategory<Product>
    {
        public int ProductCatergoryId { get; set; }
        public string? ProductCatergoryName { get; set; }
        public ConcurrentList<Product> Products { get; set; } = new();

        public bool ContainsProduct (IProduct product)
        {
            foreach (var Product in Products)
            {
                if (product.ProductId==product.ProductId&&product.ProductName==product.ProductName)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class Product:IProduct
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Prise { get; set; }
    }
}
