using Interfases;

namespace RazorApp1.Models.Entityes
{
    public class Product : IProduct
    {
        public int ProductId { get; init; }
        public string? ProductName { get; init; }
        public decimal Prise { get; set; }
        
    }
}
