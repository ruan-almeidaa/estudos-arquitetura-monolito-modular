using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuloTarefa.Auxiliares.Integracoes.ModuloUsuario;
using ModuloTarefa.Dominio.Interfaces.Repositorios;
using ModuloTarefa.Dominio.Interfaces.Servicos;
using ModuloTarefa.Dominio.Servicos;
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

            services.AddScoped<ITarefaRepo, TarefaRepo>();

            services.AddScoped<IOrquestrador, Orquestrador>();
            services.AddScoped<ITarefaServ, TarefaServ>();


            services.AddHttpClient<UsuarioHttpClient>(client => {
                var usuarioApiUrl = configuration["Urls:UsuarioApi"];
                if (string.IsNullOrEmpty(usuarioApiUrl))
                {
                    throw new Exception("A URL da API de Usuário não foi configurada corretamente.");
                }
                client.BaseAddress = new Uri(usuarioApiUrl);
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
