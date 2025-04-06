using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Infra.Banco
{
    public class DesignContexto : IDesignTimeDbContextFactory<ConfiguracaoContextoBancoModuloUsuario>
    {
        public ConfiguracaoContextoBancoModuloUsuario CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../App"))
                .AddJsonFile("appsettings.json") // Carrega as configurações do banco
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ConfiguracaoContextoBancoModuloUsuario>();
            var connectionString = configuration.GetConnectionString("ConexaoBd");

            optionsBuilder.UseSqlServer(connectionString);

            return new ConfiguracaoContextoBancoModuloUsuario(optionsBuilder.Options);
        }
    }
}
