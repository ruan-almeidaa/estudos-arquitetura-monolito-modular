using Extensoes;
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

        public async Task<List<Usuario>> BuscarTodosUsuarios()
        {
            return await _usuarioRepo.BuscarTodosUsuarios();
        }

        public async Task<Usuario> BuscarUsuarioPorId(int id)
        {
           return await _usuarioRepo.BuscarUsuarioPorId(id);
        }

        public async Task<Usuario> CriarUsuario(Usuario usuario)
        {
            usuario.DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            usuario.Credenciais.Senha = Criptografia.GerarHash(usuario.Credenciais.Senha);
            return await _usuarioRepo.CriarUsuario(usuario);
        }

        public Task<Usuario> EditarUsuario(Usuario usuario)
        {
            return _usuarioRepo.EditarUsuario(usuario);
        }

        public async Task<bool> ExcluirUsuario(int idUsuario)
        {
            Usuario usuario = await _usuarioRepo.BuscarUsuarioPorId(idUsuario);

            return usuario != null ? await _usuarioRepo.ExcluirUsuario(usuario) : false;
        }
    }
}
