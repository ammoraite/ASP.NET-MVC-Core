//using Interfases;
//using RazorApp1.Models;

//var builder = WebApplication.CreateBuilder (args);

//// Add services to the container.

//var app = builder.Build ( );

//// Configure the HTTP request pipeline.

//var httpClient = new HttpClient ( )
//{
//    BaseAddress=new Uri ("https://localhost:7182/")
//};
//await httpClient.GetAsync ("/");
//await Parallel.ForEachAsync (GenerateProducts (1000), async)

//app.Run ( );


//static IEnumerable<ICategory<IProduct>> GenerateProducts ( int count )
//{
//    for (int i = 0; i<count; i++)
//    {
//        var product = new Product ( ) { ProductName=$"Product{count}", ProductId=count };

//        var category = new Catergory() {CatergoryId = count / 10, CatergoryName = $"Category{count / 10}",};

//        category.Products.Add(product);

//        yield return category;
//    }
//}
