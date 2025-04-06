using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Infra.Banco
{
    public class ConfiguracaoContextoBancoModuloTarefa : DbContext
    {
        public ConfiguracaoContextoBancoModuloTarefa(DbContextOptions<ConfiguracaoContextoBancoModuloTarefa> options) : base(options)
        {
        }
        public DbSet<Entidades.Tarefa> Tarefas { get; set; }
    }
}
