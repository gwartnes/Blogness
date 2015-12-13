using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Runtime;
using Microsoft.Framework.Configuration;
using MongoDB.Driver;
using TestApp3.Models;
using TestApp3.Models.Repository;
using TestApp3.Models.Repository.Interfaces;
using TestApp3.Models.Repository.Context;
using TestApp3.Models.Repository.Users;

namespace TestApp3
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json");

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration);

            services.AddScoped<IContext, MongoContext>();
            services.AddScoped<IRepository<Post>, MongoRepository<Post>>();
            services.AddScoped<IUserStore<User>, UserStore<User>>();
            services.AddScoped<IRoleStore<Role>, RoleStore<Role>>();
            services.AddIdentity<User, Role>();

            services.AddCaching();
            services.AddSession();
            

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseSession();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "tagged",
                    template: "Blog/Tagged/{tag}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
