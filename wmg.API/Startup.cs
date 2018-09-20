using System;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using wmg.BusinessLayer.IManager;
using wmg.Common.Entites;
using wmg.DataAccess;
using wmg.Manager;
using wmg.DataAccess.dbContext;

namespace wmg.API
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

            services.AddScoped<IRoleManager, RoleManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddScoped<WmgDbContext>();
            services.AddScoped<IRepository<WmgDbContext>, WmgRepository>();

            services.AddMvc();
            services.AddCors();
            Mapper.Reset();
            services.AddAutoMapper();

            services.AddDbContext<WmgDbContext>(option =>
                option.UseSqlServer(Configuration.GetConnectionString("wmgDb")));

            services.AddIdentity<User, Role>()
                //.AddRoleManager<Role>()
                .AddEntityFrameworkStores<WmgDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {

                options.Cookie.Name = "wmgToken";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

                // ReturnUrlParameter requires `using Microsoft.AspNetCore.Authentication.Cookies;`
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();

          
        }
    }
}
