using ClothingShop.Server.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;

namespace ClothingShop.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            /* CORS */
            services.AddCors(options=>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            /* CORS */
            services.AddControllersWithViews();
            services.AddRazorPages();
            string sqlConnectionString = "Host=ec2-52-23-40-80.compute-1.amazonaws.com;Port=5432;Database=d4oin506rkdvi7;Username=ziuhkiiercncxk;Password=2154d97a12617fb2d993f53496f2f5b56e0d7c08c668d23c498dfe2b3e2e69c6;sslmode=Prefer;Trust Server Certificate=true;";
            services.AddDbContext<ShirtDbContext>(options=>options.UseNpgsql(sqlConnectionString));
            services.AddScoped<ShirtProvider>();
            services.AddDbContext<PantsDbContext>(options=>options.UseNpgsql(sqlConnectionString));
            services.AddScoped<PantsProvider>();
            services.AddSwaggerGen(c=> 
            {
                c.SwaggerDoc("a1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API Title is",
                    Version = "a1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            /* CORS-POLICY */
            app.UseCors("CorsPolicy");
            /* CORS-POLICY */

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => {c.SwaggerEndpoint("/swagger/a1/swagger.json","a1 api test");});
        }
    }
}
