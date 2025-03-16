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
    }
}
