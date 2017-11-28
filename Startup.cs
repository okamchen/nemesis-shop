using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaNemesis.Auth;
using LojaNemesis.Helpers;
using LojaNemesis.Infra;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LojaTrabalhoFinal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = JwtSecurityKey.Create()
            };
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("DataSource=loja.db"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
              options =>
              {
                  options.TokenValidationParameters = tokenValidationParameters;
              }
            );

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseMiddleware(typeof(ExceptionHandling));

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}