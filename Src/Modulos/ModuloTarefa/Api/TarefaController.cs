using Extensoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModuloTarefa.Dominio.Interfaces.Servicos;
using ModuloTarefa.Dtos.Entrada;
using ModuloTarefa.Dtos.Saida;
using System.Security.Claims;

namespace ModuloTarefa.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TarefaController : ControllerBase
    {
        private readonly IOrquestrador _orquestrador;
        public TarefaController(IOrquestrador orquestrador)
        { 
            _orquestrador = orquestrador;
        }

        [HttpPost("Criar")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<PadraoRespostasApi<TarefaDetalhadaDto>>> CriarTarefa([FromBody] TarefaCriarDto tarefaCriarDto)
        {
            int idUsuarioToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            string direitoUsuarioToken = User.FindFirst(ClaimTypes.Role)?.Value;
            ValidaAcessoRota.ValidarAcessoRota(idUsuarioToken, tarefaCriarDto.AdminId, direitoUsuarioToken, true);

            return await _orquestrador.CriarTarefa(tarefaCriarDto);
        }
        [HttpPut("Editar")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<PadraoRespostasApi<TarefaDetalhadaDto>>> EditarTarefa([FromBody] TarefaEditarDto tarefaEditarDto)
        {
            int idUsuarioToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            string direitoUsuarioToken = User.FindFirst(ClaimTypes.Role)?.Value;
            ValidaAcessoRota.ValidarAcessoRota(idUsuarioToken, tarefaEditarDto.AdminId, direitoUsuarioToken, true);

            return await _orquestrador.EditarTarefa(tarefaEditarDto);
        }
        [HttpPut("AtualizarStatus")]
        public async Task<ActionResult<PadraoRespostasApi<TarefaDetalhadaDto>>> AtualizarStatusTarefa([FromBody] TarefaAtualizarStatusDto tarefaAtualizarStatusDto)
        {
            int idUsuarioToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            string direitoUsuarioToken = User.FindFirst(ClaimTypes.Role)?.Value;
            ValidaAcessoRota.ValidarAcessoRota(idUsuarioToken, tarefaAtualizarStatusDto.AdminId, direitoUsuarioToken, true);

            return await _orquestrador.AtualizarStatusTarefa(tarefaAtualizarStatusDto);
        }

    }
}
