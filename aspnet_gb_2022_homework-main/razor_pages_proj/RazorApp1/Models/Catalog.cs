namespace RazorApp1.Models
{
    public class Catalog
    {
        public List<Catergory> Catergories { get; set; } = new();
    }

    public class Catergory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
