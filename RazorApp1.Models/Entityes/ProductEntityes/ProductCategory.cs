using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmmoraiteCollections;
using Interfases;

namespace RazorApp1.Models.Entityes.ProductEntityes
{
    public class ProductCategory : IProductCategory
    {
        public int ProductCatergoryId { get; set; }
        public string? ProductCatergoryName { get; set; }
        public ConcurrentList<IProduct> Products { get; set; } = new ( );
        public bool ContainsProduct ( IProduct product ) => Products.Contains (x =>
            x!=null&&
            x.ProductId==product.ProductId&&
            x.ProductName==product.ProductName);
    }
}
