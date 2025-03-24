using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ModuloUsuario.Auxiliares;
using ModuloUsuario.Auxiliares.Validacoes.Usuario;
using ModuloUsuario.Dominio.Interfaces.Repositorios;
using ModuloUsuario.Dominio.Interfaces.Servicos;
using ModuloUsuario.Dominio.Servicos;
using ModuloUsuario.Infra.Banco;
using ModuloUsuario.Infra.Repositorios;
using System.Text;

namespace ModuloUsuario.Infra.Base
{
    public static class ConfiguracaoModuloUsuario
    {
        public static IServiceCollection AddModuloUsuario(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ConfiguracaoContextoBancoModuloUsuario>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConexaoBd")));

            //Serviços que irão implementar as interfaces do módulo
            services.AddScoped<IOrquestrador, Orquestrador>();
            services.AddScoped<ICredenciaisServ, CredenciaisServ>();
            services.AddScoped<IUsuarioServ, UsuarioServ>();

            //Repositórios que irão implementar as interfaces do módulo
            services.AddScoped<ICredenciaisRepo, CredenciaisRepo>();
            services.AddScoped<IUsuarioRepo, UsuarioRepo>();

            //Validações automáticas do módulo, através do FluentValidation
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<ValidaUsuarioCriarDto>();

            //Mapemanto automático com AutoMapper
            services.AddAutoMapper(typeof(Mapeamentos));

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
