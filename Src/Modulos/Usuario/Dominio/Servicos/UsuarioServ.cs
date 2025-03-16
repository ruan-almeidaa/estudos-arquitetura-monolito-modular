using ModuloUsuario.Dominio.Interfaces.Repositorios;
using ModuloUsuario.Dominio.Interfaces.Servicos;
using ModuloUsuario.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Dominio.Servicos
{
    public class UsuarioServ : IUsuarioServ
    {
        private readonly IUsuarioRepo _usuarioRepo;
        public UsuarioServ(IUsuarioRepo usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }
        public async Task<Usuario> BuscarUsuarioPorId(int id)
        {
           return await _usuarioRepo.BuscarUsuarioPorId(id);
        }

        public async Task<Usuario> CriarUsuario(Usuario usuario)
        {
            return await _usuarioRepo.CriarUsuario(usuario);
        }
    }
}
