using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieApp.Web.Data;
using Microsoft.EntityFrameworkCore.Design;

namespace MovieApp.Web
{
    public class Startup
    {
       
        public IConfiguration Configuration { get; } // eklendi
        
        public Startup(IConfiguration configuration) // eklendi
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MovieContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("MsSQLConnection")));
                //options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=movies2;Integrated Security=True;"));
            // eklendi
            //services.AddDbContext<MovieContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));


            services.AddControllersWithViews();
            services.AddControllersWithViews().AddViewOptions(options => options.HtmlHelperOptions.ClientValidationEnabled = true);
        }

        // Mssql için gerekli
        public class YourDbContextFactory : IDesignTimeDbContextFactory<MovieContext>
        {
            public MovieContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<MovieContext>();
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=movies2;Integrated Security=True;");

                return new MovieContext(optionsBuilder.Options);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // uygulama geliþtirme aþamasýndadýr
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DataSeeding.Seed(app);
            }

            app.UseStaticFiles(); // wwwroot

            // MiddleWare
            app.UseRouting();
            // localhost:500
            // localhost: 5000/ movies


            //uygulamaya gelen ana dizie gelen bir istek sonucunda kullanýcýya bir istek döndürüyorsuz ve response içerisinde Hello wirold
            app.UseEndpoints(endpoints =>
            {
                //// localost: 5000 / controller / action
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

                //// localost: 5000
                //endpoints.MapControllerRoute(
                //    name: "homeIndex",
                //    pattern: "",
                //    defaults: new { controller = "Home", action = "Index" }
                //);
                //// localost: 5000 / about
                //endpoints.MapControllerRoute(
                //    name: "homeAbout",
                //    pattern: "about",
                //    defaults: new { controller = "Home", action = "About" }
                //);

                //endpoints.MapControllerRoute(
                //    name: "movieList",
                //    pattern: "movies/list",
                //    defaults: new {controller = "Movies", action = "List" }
                //);

                //endpoints.MapControllerRoute(
                //    name: "movieDetails",
                //    pattern: "movies/details",
                //    defaults: new { controller = "Movies", action = "Details" }
                //);
            });
        }
    }
}
