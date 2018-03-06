using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PieShop.Models;

namespace PieShop
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            //whenever someone is asking for IPieRepository a new MockPieRepository will be returned (whenever an instance of IPieRepository is requested we get a new instance every time)
            services.AddTransient<IPieRepository, PieRepository>();

            services.AddTransient<IFeedbackRepository, FeedbackRepository>();

            //only one single instance is going to be created of the specified type and the same instance is always going to be returned        
            //services.AddSingleton

            //if the request goes out of the scope the instance is removed
            //services.AddScoped

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage(); // yellow error screen
            app.UseStatusCodePages(); // 400 500 200
            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute(); 

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name:"default",
                    template:"{controller=Home}/{action=Index}/{id?}");
            });

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
