using Microsoft.EntityFrameworkCore;
using ModuloUsuario.Dominio.Interfaces.Repositorios;
using ModuloUsuario.Entidades;
using ModuloUsuario.Infra.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Infra.Repositorios
{
    public class UsuarioRepo : IUsuarioRepo
    {
        private readonly ConfiguracaoContextoBancoModuloUsuario _contexto;
        public UsuarioRepo(ConfiguracaoContextoBancoModuloUsuario contexto)
        {
            _contexto = contexto;
        }

        public async Task<Usuario> BuscarUsuarioPorId(int id)
        {
            return await _contexto.Usuarios
                .Include(c => c.Credenciais)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Usuario> CriarUsuario(Usuario usuario)
        {
            await _contexto.Usuarios.AddAsync(usuario);
            await _contexto.SaveChangesAsync();
            return await BuscarUsuarioPorId(usuario.Id);

        }
    }
}
