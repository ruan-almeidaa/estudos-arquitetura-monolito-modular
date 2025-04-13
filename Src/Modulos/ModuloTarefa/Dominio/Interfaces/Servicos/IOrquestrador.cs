using Extensoes;
using ModuloTarefa.Dtos.Entrada;
using ModuloTarefa.Dtos.Saida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Dominio.Interfaces.Servicos
{
    public interface IOrquestrador
    {
        Task <PadraoRespostasApi<TarefaDetalhadaDto>> CriarTarefa(TarefaCriarDto tarefaCriarDto);
        Task<PadraoRespostasApi<TarefaDetalhadaDto>> EditarTarefa(TarefaEditarDto tarefaEditarDto);
        Task<PadraoRespostasApi<TarefaDetalhadaDto>> AtualizarStatusTarefa(TarefaAtualizarStatusDto tarefaAtualizarStatusDto);
        Task<PadraoRespostasApi<bool>> ExcluirTarefa(int idTarefa);
        Task<PadraoRespostasApi<Paginacao<TarefaDetalhadaDto>>> BuscarTodasTarefas(int numeroPagina, int totalItens);
        Task<PadraoRespostasApi<TarefaDetalhadaDto>> BuscarTarefaPorId(int idTarefa);
        Task<PadraoRespostasApi<Paginacao<TarefaDetalhadaDto>>> BuscarTarefasPorUsuarioId(int usuarioId, int numeroPagina, int totalItens);
    }
}
