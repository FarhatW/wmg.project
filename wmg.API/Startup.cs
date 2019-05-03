using System;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using wmg.BusinessLayer.IManager;
using wmg.Common.Entites;
using wmg.Common.Setting;
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
            services.AddScoped<IProjectManager, ProjectManager>();
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

            var appSettingsSection = Configuration.GetSection("AuthSetting");
            services.Configure<AuthSetting>(appSettingsSection);
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AuthSetting>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddMvc();
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
