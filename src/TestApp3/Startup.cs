﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Blogness.Models;
using Blogness.Models.Repository;
using Blogness.Models.Repository.Interfaces;
using Blogness.Models.Repository.Context;
using Blogness.Models.Repository.Users;
using Microsoft.Extensions.PlatformAbstractions;

namespace Blogness
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder().SetBasePath(appEnv.ApplicationBasePath)
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
                //routes.MapRoute(
                //    name: "leaveComment",
                //    template: "Blog/LeaveComment",
                //    defaults: new { controller = "Blog", action = "LeaveComment" });

                routes.MapRoute(
                    name: "findPostById",
                    template: "Blog/{id}",
                    defaults: new { controller = "Blog", action = "ViewPost" });

                routes.MapRoute(
                    name: "tagged",
                    template: "Blog/Tagged/{tag}",
                    defaults: new { controller = "Blog", action = "Tagged" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
