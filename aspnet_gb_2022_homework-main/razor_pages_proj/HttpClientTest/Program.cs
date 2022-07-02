using RazorApp1.Models;

var builder = WebApplication.CreateBuilder (args);

// Add services to the container.

var app = builder.Build ( );

// Configure the HTTP request pipeline.


var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7182/")
};

await httpClient.GetAsync("/");

foreach (var httpRequest in Prod.Createpost(1000))
{
    await httpClient.GetAsync(httpRequest);
    Console.WriteLine("выполнен");
}
    


app.Run ( );

public record Prod
{
    public static IEnumerable<string>  Createpost ( int count )
    {
        for (int i = 0; i < count; i++)
        {
            yield return $"Catalog/AddProduct?ProductCatergoryId={i / 10}&ProductCatergoryName=Category{i / 10}&ProductId={i}&ProductName=Product_{i}&Prise={i + i}";
        }
        
    }
}
