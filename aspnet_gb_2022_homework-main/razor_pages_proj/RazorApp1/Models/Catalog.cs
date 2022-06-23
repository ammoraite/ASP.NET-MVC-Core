using System.Drawing;

namespace RazorApp1.Models
{
    public class Catalog
    {
        public List<Catergory> Catergories { get; set; } = new();
    }

    public class Catergory
    {
        public int CatergoryId { get; set; }
        public string? CatergoryName { get; set; }
        public List<Product> Products { get; set; } = new();
    }
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
    }
}
