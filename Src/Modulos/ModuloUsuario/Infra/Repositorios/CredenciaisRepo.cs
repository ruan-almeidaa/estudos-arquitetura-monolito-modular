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

        public async Task<int> BuscarIdUsuarioPorEmail(string email)
        {
           return await _contexto.Credenciais
                .AsNoTracking()
                .Where(c => c.Email == email)
                .Select(c => c.UsuarioId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExisteEmailSenha(string email, string senha)
        {
            return await _contexto.Credenciais
                .AsNoTracking()
                .AnyAsync(c => c.Email == email && c.Senha == senha);
        }

        public async Task<bool> VerificaEmailExiste(string email)
        {
            return await _contexto.Credenciais
                .AsNoTracking()
                .AnyAsync(c => c.Email == email);
        }
    }
}
