using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Infrastructure.Data;
using TDSTecnologia.Site.Infrastructure.Integrations.Email;
using TDSTecnologia.Site.Infrastructure.Repository;
using TDSTecnologia.Site.Infrastructure.Services;

namespace TDSTecnologia.Site.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
            _logger.LogInformation("ARQUIVO: " + Configuration.GetValue<string>("Arquivo"));
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<AppContexto>(options => options.UseNpgsql(Databases.Instance.Conexao));
            services.AddIdentity<Usuario, Permissao>()
                            .AddDefaultUI(UIFramework.Bootstrap4)
                            .AddEntityFrameworkStores<AppContexto>();
            services.AddScoped<CursoRespository, CursoRespository>();
            services.AddScoped<CursoService, CursoService>();
            services.AddScoped<PermissaoService, PermissaoService>();
            services.AddScoped<UsuarioService, UsuarioService>();
            services.Configure<ConfiguracoesEmail>(Configuration.GetSection("ConfiguracoesEmail"));
            services.AddScoped<IEmail, Email>();
            services.AddLogging();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
        }

        public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env)
        {
            _logger.LogInformation("AMBIENTE: " + env.EnvironmentName);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseAuthentication();
            app.UseStatusCodePages();
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();

        }

        public void ConfigureStagingServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<AppContexto>(options => options.UseNpgsql(Databases.Instance.Conexao));
            services.AddIdentity<Usuario, Permissao>()
                            .AddDefaultUI(UIFramework.Bootstrap4)
                            .AddEntityFrameworkStores<AppContexto>();
            services.AddScoped<CursoRespository, CursoRespository>();
            services.AddScoped<CursoService, CursoService>();
            services.AddScoped<PermissaoService, PermissaoService>();
            services.AddScoped<UsuarioService, UsuarioService>();
            services.Configure<ConfiguracoesEmail>(Configuration.GetSection("ConfiguracoesEmail"));
            services.AddScoped<IEmail, Email>();
            services.AddLogging();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
        }

        public void ConfigureStaging(IApplicationBuilder app, IHostingEnvironment env)
        {
            _logger.LogInformation("AMBIENTE: " + env.EnvironmentName);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseAuthentication();
            app.UseStatusCodePages();
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();

        }


        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<AppContexto>(options => options.UseNpgsql(Databases.Instance.Conexao));
            services.AddIdentity<Usuario, Permissao>()
                            .AddDefaultUI(UIFramework.Bootstrap4)
                            .AddEntityFrameworkStores<AppContexto>();
            services.AddScoped<CursoRespository, CursoRespository>();
            services.AddScoped<CursoService, CursoService>();
            services.AddScoped<PermissaoService, PermissaoService>();
            services.AddScoped<UsuarioService, UsuarioService>();
            services.Configure<ConfiguracoesEmail>(Configuration.GetSection("ConfiguracoesEmail"));
            services.AddScoped<IEmail, Email>();
            services.AddLogging();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
        }

        public void ConfigureProduction(IApplicationBuilder app, IHostingEnvironment env)
        {
            _logger.LogInformation("AMBIENTE: " + env.EnvironmentName);

            app.UseStatusCodePagesWithReExecute("/Erros/{0}");
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();

        }
    }
}
