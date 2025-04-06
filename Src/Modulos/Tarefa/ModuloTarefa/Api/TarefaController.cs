using Extensoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModuloTarefa.Dominio.Interfaces.Servicos;
using ModuloTarefa.Dtos.Entrada;
using ModuloTarefa.Dtos.Saida;

namespace ModuloTarefa.Api
{
    [ApiController]
    [Route("api/[controller]")]
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
    }
}
