using ModuloUsuario.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Dominio.Interfaces.Servicos
{
    public interface IUsuarioServ
    {
        Task<Usuario> CriarUsuario(Usuario usuario);
        Task<Usuario> BuscarUsuarioPorId(int id);
        Task<Usuario> EditarUsuario(Usuario usuario);
        Task<bool> ExcluirUsuario(int idUsuario);
        Task<List<Usuario>> BuscarTodosUsuarios(int numeroPagina, int totalItens);
        Task<string> GerarToken(Usuario usuario);
        Task<int> ContarUsuarios();
    }
}
