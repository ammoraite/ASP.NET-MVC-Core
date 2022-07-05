<<<<<<< HEAD
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
=======
var builder = WebApplication.CreateBuilder (args);
>>>>>>> Update_Collection

#region ConfigureServices

// Add services to the container.
<<<<<<< HEAD
builder.Services.AddControllersWithViews();

#endregion

var app = builder.Build();
=======
builder.Services.AddControllersWithViews ( );

#endregion

var app = builder.Build ( );
>>>>>>> Update_Collection

#region Configure


// Configure the HTTP request pipeline.
<<<<<<< HEAD
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
=======
if (!app.Environment.IsDevelopment ( ))
{
    app.UseExceptionHandler ("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts ( );
}

app.UseHttpsRedirection ( );
app.UseStaticFiles ( );

app.UseRouting ( );

app.UseAuthorization ( );

app.MapControllerRoute (
>>>>>>> Update_Collection
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

#endregion

app.Run ( );
