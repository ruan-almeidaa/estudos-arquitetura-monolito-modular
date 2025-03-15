using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuloUsuario.Infra.Banco;

namespace ModuloUsuario.Infra.Base
{
    public static class ConfiguracaoModuloUsuario
    {
        public static IServiceCollection AddModuloUsuario(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ConfiguracaoContextoBancoModuloUsuario>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConexaoBd")));

            return services;
        }

        public static void ExecutaMigrationsModuloUsuario(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<ConfiguracaoContextoBancoModuloUsuario>();
            context.Database.Migrate();

        }
    }
}
