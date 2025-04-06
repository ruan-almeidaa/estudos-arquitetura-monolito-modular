using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Infra.Banco
{
    public class DesignContexto : IDesignTimeDbContextFactory<ConfiguracaoContextoBancoModuloTarefa>
    {
        public ConfiguracaoContextoBancoModuloTarefa CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../App"))
                .AddJsonFile("appsettings.json") // Carrega as configurações do banco
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ConfiguracaoContextoBancoModuloTarefa>();
            var connectionString = configuration.GetConnectionString("ConexaoBd");

            optionsBuilder.UseSqlServer(connectionString);

            return new ConfiguracaoContextoBancoModuloTarefa(optionsBuilder.Options);
        }
    }
}
