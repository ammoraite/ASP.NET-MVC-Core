
using EmailSenderWebApi.Domain.DomainEvents.EventConsumers;
using EmailSenderWebApi.Models.EmailModels;
using EmailSenderWebApi.Models.EmailModels.EmailDataEvent;
using EmailSenderWebApi.Services.CountersServices.ClickCouterService;

using Microsoft.AspNetCore.HttpLogging;

using RazorApp1.Controllers;
using RazorApp1.Services.EmailService;
using RazorApp1.Services.EmailService.ServiseIntefaces;
using RazorApp1.Services.HostedServices;

using Serilog;
using Serilog.Events;

#pragma warning disable U2U1001 // Stateless classes can be static
Log.Logger=new LoggerConfiguration ( )

   .CreateBootstrapLogger ( ); //означает, что глобальный логер будет заменен на вариант из Host.UseSerilog
#pragma warning restore U2U1001 // Stateless classes can be static
Log.Information ("Starting up");

try
{
    #region Builder
    var builder = WebApplication.CreateBuilder (args);


    builder.Host.UseSerilog (( ctx, conf ) =>
    {
        conf
            .MinimumLevel.Debug ( ) //<- ћинимальный уровень дл€ всех приемников
            .WriteTo.File ("log-.txt", rollingInterval: RollingInterval.Day)
            .ReadFrom.Configuration (builder.Configuration)
            .WriteTo.Console (restrictedToMinimumLevel: LogEventLevel.Information);
    });
    builder.Services.AddHttpLogging (opt =>
    {
        opt.LoggingFields=HttpLoggingFields.RequestHeaders|
                          HttpLoggingFields.ResponseHeaders|
                          HttpLoggingFields.RequestBody|
                          HttpLoggingFields.ResponseBody;
    });

    builder.Services.Configure<SmtpCredentions> (
        builder.Configuration.GetSection ("SmtpCredentions"));
    builder.Services.Configure<EmailCredentions> (
        builder.Configuration.GetSection ("EmailCredentions"));

    builder.Services.Configure<OptionsEmailSender> (builder.Configuration.GetSection ("OptionsEmailDataEvent"));

    // Add services to the container.
    builder.Services.AddSingleton<IClickCounter, ClickCounter> ( );
    builder.Services.AddTransient<IEmailSender, EmailSenderService> ( );

    builder.Services.AddHostedService<SenderMailAppWorkService> ( );

    builder.Services.AddHostedService<SenderProductChangedEvent> ( );

    builder.Services.AddControllersWithViews ( );

    var app = builder.Build ( );
    #endregion

    #region Configure

    //провека браузера на соответствие Microsoft Edge
    app.Use (async ( context, next ) =>
    {
        var userAgent = context.Request.Headers.UserAgent/*["sec-ch-ua"]*/.ToString ( );
        if (!userAgent.ToLower ( ).Contains ("edg"))
        {
            context.Response.ContentType="text/plain; charset=UTF-8";
            //await context.Response.WriteAsync ("This browser not supported yet, only Microsoft Edge suppored");
            await context.Response.WriteAsync ("Ётот браузер еще не поддерживаетс€, поддерживаетс€ только Microsoft Edge");
            return;
        }
        await next ( );
    });

    app.UseStaticFiles ( );
    app.UseHttpLogging ( );
    app.UseSerilogRequestLogging ( );

    // Configure the HTTP request pipeline.
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
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.UseSerilogRequestLogging ( );

    ///сбор метрик по заросам
    app.Use (async ( context, next ) =>
    {
        ///сбор метрик по всем запросам
#if true
        var path = context.Request.Path.Value;
        var count = app.Services.GetService<IClickCounter> ( );
        if (!count.Count.ContainsKey (path))
        {
            count.Count.TryAdd (path, 1);
        }
        else
        {
            count.Count[path]++;
        }
        await next ( );
#endif
        //—бор метрик только из запросов в контроллер CatalogController
#if false
        var path = context.Request.Path.Value;

        var checkPath = path!="/" ? path.Substring (path.LastIndexOf ('/')+1) : "";

        var metodsInController = typeof (CatalogController).GetMethods ( );

        foreach (var item in metodsInController)
        {
            if (checkPath!=""&&item.Name.ToLower ( ).Contains (checkPath.ToLower ( )))
            {
                var count = app.Services.GetService<IClickCounter> ( );
                if (!count.Count.ContainsKey (path))
                {
                    count.Count.TryAdd (path, 1);
                }
                else
                {
                    count.Count[path]++;
                }
            }
        }
        await next ( );
#endif
    });
    #endregion

    app.Run ( );
}
catch (Exception E)
{
    Log.Fatal (E, "—ервер рухнул!");
}
finally
{
    Log.Information ("Shut down complete");
    Log.CloseAndFlush ( );
}

