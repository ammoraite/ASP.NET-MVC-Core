namespace Interfases
{
    public interface IProduct
    {
        public int ProductId { get; init; }
        public string? ProductName { get; init; }
        public decimal Prise { get; set; }
    }
}
