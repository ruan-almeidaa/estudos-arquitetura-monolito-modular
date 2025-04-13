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
            return await _orquestrador.CriarTarefa(tarefaCriarDto);
        }
        [HttpPut("Editar")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<PadraoRespostasApi<TarefaDetalhadaDto>>> EditarTarefa([FromBody] TarefaEditarDto tarefaEditarDto)
        {
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
        [HttpDelete("Excluir/{idTarefa}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<PadraoRespostasApi<bool>>> ExcluirTarefa(int idTarefa)
        {
            return await _orquestrador.ExcluirTarefa(idTarefa);

        }
        [HttpGet("BuscarTodasTarefas")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<PadraoRespostasApi<Paginacao<TarefaDetalhadaDto>>>> BuscarTodasTarefas([FromQuery] int numeroPagina = 1, [FromQuery] int totalItens = 10)
        {
            return await _orquestrador.BuscarTodasTarefas(numeroPagina, totalItens);
        }
        [HttpGet("BuscarTarefaPorId/{idTarefa}")]
        public async Task<ActionResult<PadraoRespostasApi<TarefaDetalhadaDto>>> BuscarTarefaPorId([FromRoute] int idTarefa)
        {
            int idUsuarioToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            string direitoUsuarioToken = User.FindFirst(ClaimTypes.Role)?.Value;
            ValidaAcessoRota.ValidarAcessoRota(idUsuarioToken, idTarefa, direitoUsuarioToken, true);
            return await _orquestrador.BuscarTarefaPorId(idTarefa);
        }
        [HttpGet("BuscarTarefasPorUsuarioId/{usuarioId}")]
        public async Task<ActionResult<PadraoRespostasApi<Paginacao<TarefaDetalhadaDto>>>> BuscarTarefasPorUsuarioId(
            [FromRoute] int usuarioId, [FromQuery] int numeroPagina = 1, [FromQuery] int totalItens = 10)
        {
            int idUsuarioToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            string direitoUsuarioToken = User.FindFirst(ClaimTypes.Role)?.Value;
            ValidaAcessoRota.ValidarAcessoRota(idUsuarioToken, usuarioId, direitoUsuarioToken, true);

            var resposta = await _orquestrador.BuscarTarefasPorUsuarioId(usuarioId, numeroPagina, totalItens);
            if (resposta.Dados is null || !resposta.Dados.Itens.Any())
                return NoContent();

            return Ok(resposta);
        }
    }
}
