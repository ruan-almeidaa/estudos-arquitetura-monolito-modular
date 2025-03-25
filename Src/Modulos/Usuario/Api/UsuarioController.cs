using Extensoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModuloUsuario.Dominio.Interfaces.Servicos;
using ModuloUsuario.Dtos.Entrada.Usuario;
using ModuloUsuario.Dtos.Saida.Usuario;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.ConstrainedExecution;
using ModuloUsuario.Auxiliares;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ModuloUsuario.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IOrquestrador _orquestrador;
        public UsuarioController(IOrquestrador orquestrador)
        {
            _orquestrador = orquestrador;
        }

        [HttpPost("Criar")]
        public async Task<ActionResult<PadraoRespostasApi<UsuarioAutenticadoDto>>> CriarUsuario([FromBody] UsuarioCriarDto usuarioCriarDto)
        {
            return await _orquestrador.CriarUsuario(usuarioCriarDto);
        }
        [HttpPost("Autenticar")]
        public async Task<ActionResult<PadraoRespostasApi<UsuarioAutenticadoDto>>> AutenticarUsuario([FromBody] UsuarioAutenticarDto usuarioAutenticarDto)
        {
            return await _orquestrador.AutenticarUsuario(usuarioAutenticarDto);
        }

        [HttpPut("Editar")]
        [Authorize]
        public async Task<ActionResult<PadraoRespostasApi<UsuarioAutenticadoDto>>> EditarUsuario([FromBody] UsuarioEditarDto usuarioEditarDto)
        {
            int idUsuarioToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            string direitoUsuarioToken = User.FindFirst(ClaimTypes.Role)?.Value;
            ValidaAcessoRota.ValidarAcessoRota(idUsuarioToken, usuarioEditarDto.Id, direitoUsuarioToken, true);


            return await _orquestrador.EditarUsuario(usuarioEditarDto);
        }
        [HttpDelete("Excluir/{idUsuario}")]
        [Authorize]
        public async Task<ActionResult<PadraoRespostasApi<bool>>> ExcluirUsuario([FromRoute] int idUsuario)
        {
            int idUsuarioToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            string direitoUsuarioToken = User.FindFirst(ClaimTypes.Role)?.Value;
            ValidaAcessoRota.ValidarAcessoRota(idUsuarioToken, idUsuario, direitoUsuarioToken, true);
            return await _orquestrador.ExcluirUsuario(idUsuario);
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet("BuscarTodos")]
        public async Task<ActionResult<PadraoRespostasApi<Paginacao<UsuarioDetalhadoDto>>>> BuscarTodosUsuarios([FromQuery] int numeroPagina, [FromQuery] int totalItens)
        {
            return await _orquestrador.BuscarTodosUsuarios(numeroPagina, totalItens);
        }


    }
}
