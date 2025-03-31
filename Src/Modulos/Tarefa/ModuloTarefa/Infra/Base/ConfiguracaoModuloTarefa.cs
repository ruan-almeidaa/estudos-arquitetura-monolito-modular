using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuloTarefa.Auxiliares.Integracoes.ModuloUsuario;
using ModuloTarefa.Dominio.Interfaces.Repositorios;
using ModuloTarefa.Infra.Banco;
using ModuloTarefa.Infra.Repositorios;

namespace ModuloTarefa.Infra.Base
{
    public static class ConfiguracaoModuloTarefa
    {
        public static IServiceCollection AddModuloTarefa(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ConfiguracaoContextoBancoModuloTarefa>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ConexaoBd"));
            });

            //Repositórios que irão implementar as interfaces do módulo
            services.AddScoped<ITarefaRepo, TarefaRepo>();
            services.AddHttpClient<UsuarioHttpClient>(client => {
                client.BaseAddress = new Uri(configuration["Urls:UsuarioApi"]);
            });

            return services;
        }

        public static void ExecutaMigrationsModuloTarefa(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<ConfiguracaoContextoBancoModuloTarefa>();
            context.Database.Migrate();

        }
    }
}
