using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Infra.Banco
{
    public class ConfiguracaoContextoBancoModuloUsuario : DbContext
    {
        public ConfiguracaoContextoBancoModuloUsuario(DbContextOptions<ConfiguracaoContextoBancoModuloUsuario> options) : base(options) { }
    }
}
