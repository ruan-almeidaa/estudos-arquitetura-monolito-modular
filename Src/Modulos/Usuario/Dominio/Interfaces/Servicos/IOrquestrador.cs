using Extensoes;
using ModuloUsuario.Dtos.Entrada.Usuario;
using ModuloUsuario.Dtos.Saida.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Dominio.Interfaces.Servicos
{
    public interface IOrquestrador
    {
        Task<PadraoRespostasApi<UsuarioAutenticadoDto>> CriarUsuario(UsuarioCriarDto usuarioCriarDto);
        Task<PadraoRespostasApi<UsuarioAutenticadoDto>> AutenticarUsuario(UsuarioAutenticarDto usuarioAutenticarDto);
        Task<PadraoRespostasApi<UsuarioAutenticadoDto>> EditarUsuario(UsuarioEditarDto usuarioEditarDto);
        Task<PadraoRespostasApi<bool>> ExcluirUsuario(int idUsuario);
        Task<PadraoRespostasApi<Paginacao<UsuarioDetalhadoDto>>> BuscarTodosUsuarios(int numeroPagina, int totalItens);

    }
}
