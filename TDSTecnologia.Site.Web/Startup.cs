using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Infrastructure.Data;
using TDSTecnologia.Site.Infrastructure.Repository;
using TDSTecnologia.Site.Infrastructure.Services;

namespace TDSTecnologia.Site.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<AppContexto>(options => options.UseNpgsql(Configuration.GetConnectionString("AppConnection")));
            services.AddIdentity<Usuario, Permissao>()
                            .AddDefaultUI(UIFramework.Bootstrap4)
                            .AddEntityFrameworkStores<AppContexto>();
            services.AddScoped<CursoRespository, CursoRespository>();
            services.AddScoped<CursoService, CursoService>();
            services.AddScoped<PermissaoService, PermissaoService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseAuthentication();
            app.UseStatusCodePages();
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();

        }
    }
}
