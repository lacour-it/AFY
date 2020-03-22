using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using AFYAPI.Data;
using AFYAPI.Models;

namespace AFYAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AFYContext>(opt =>
                opt.UseMySql("server=localhost;userid=testuser;password=1TestUser!;database=AFYAPI;"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
             .CreateScope())
            {
                var svc = serviceScope.ServiceProvider.GetService<AFYContext>();
                svc.Database.Migrate();
                //Basic Seeding Administrator
                if (!svc.Users.Any(u => u.Username == "admin"))
                {
                    AFYUser user = new AFYUser() { Username = "admin", Password = "1TestUser!" };
                    svc.Users.Add(user);
                    AFYRole role = new AFYRole() { RoleIndex = 0, RoleName = "Administrator" };
                    svc.Roles.Add(role);
                    AFYUserRole userRole = new AFYUserRole() { User = user, Role = role };
                    svc.UserRoles.Add(userRole);
                    svc.SaveChanges();
                    role = new AFYRole() { RoleIndex = 1, RoleName = "PowerUser" };
                    svc.Roles.Add(role);
                    svc.SaveChanges();
                    role = new AFYRole() { RoleIndex = 2, RoleName = "Employee" };
                    svc.Roles.Add(role);
                    svc.SaveChanges();
                }
            }
            app.UseMvc();
        }
    }
}
