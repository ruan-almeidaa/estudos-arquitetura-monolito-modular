using Microsoft.EntityFrameworkCore;
using ModuloUsuario.Dominio.Interfaces.Repositorios;
using ModuloUsuario.Infra.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Infra.Repositorios
{
    public class CredenciaisRepo : ICredenciaisRepo
    {
        private readonly ConfiguracaoContextoBancoModuloUsuario _contexto;

        public CredenciaisRepo(ConfiguracaoContextoBancoModuloUsuario contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> VerificaEmailExiste(string email)
        {
            return await _contexto.Credenciais
                .AsNoTracking()
                .AnyAsync(c => c.Email == email);
        }
    }
}
