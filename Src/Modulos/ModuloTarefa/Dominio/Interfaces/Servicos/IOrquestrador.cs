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
    }
}
