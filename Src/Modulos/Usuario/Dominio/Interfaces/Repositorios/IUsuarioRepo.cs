using ModuloUsuario.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Dominio.Interfaces.Repositorios
{
    public interface IUsuarioRepo
    {
        Task<Usuario> CriarUsuario(Usuario usuario);
        Task<Usuario> BuscarUsuarioPorId(int id);
        Task<Usuario> EditarUsuario(Usuario usuario);
        Task<bool> ExcluirUsuario(Usuario usuario);
        Task<List<Usuario>> BuscarTodosUsuarios(int numeroPagina, int totalItens);
        Task<int> ContarUsuarios();
    }
}
