
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using WebApplication4.Models;
using WebApplication4.Repo;


namespace WebApplication4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
    
            builder.Services.AddControllersWithViews();
            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DbConnection1>(options => options.UseSqlServer(connection));

            builder.Services.AddSingleton<Genric>();
            builder.Services.AddTransient<BLClass>();
            builder.Services.AddSingleton<GTH>();   
         

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
                name: "default",
                pattern: "{controller=Home}/{action=List}/{id?}");

            app.Run();
        }
    }
}
