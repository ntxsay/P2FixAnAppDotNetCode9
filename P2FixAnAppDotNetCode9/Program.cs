using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using P2FixAnAppDotNetCode9.Models;
using P2FixAnAppDotNetCode9.Models.Repositories;
using P2FixAnAppDotNetCode9.Models.Services;

namespace P2FixAnAppDotNetCode9;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the container.
        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
        builder.Services.AddControllersWithViews();
        builder.Services.AddSingleton<ICart, Cart>();
        builder.Services.AddSingleton<ILanguageService, LanguageService>();
        builder.Services.AddTransient<IProductService, ProductService>();
        builder.Services.AddTransient<IProductRepository, ProductRepository>();
        builder.Services.AddTransient<IOrderService, OrderService>();
        builder.Services.AddTransient<IOrderRepository, OrderRepository>();
        builder.Services.AddMvc()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();
        
        builder.Services.Configure<RequestLocalizationOptions>(opts =>
        { 
            var supportedCultures = new List<CultureInfo>
            {
                new("en-GB"),
                new("en-US"),
                new("en"),
                new("fr-FR"),
                new("fr"),
            };

            opts.DefaultRequestCulture = new RequestCulture("en");
            // Formatting numbers, dates, etc.
            opts.SupportedCultures = supportedCultures;
            // UI strings that we have localized.
            opts.SupportedUICultures = supportedCultures;
        });
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        var options = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
        app.UseRequestLocalization(options.Value);
        
        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}