using ModuloTarefa.Dtos.Entrada;
using ModuloTarefa.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Dominio.Interfaces.Servicos
{
    public interface ITarefaServ
    {
        Task<Tarefa> CriarTarefa(Tarefa tarefa);
        Task<Tarefa> BuscarTarefaPorId(int id);
        Task<Tarefa> EditarTarefa(Tarefa tarefa);
        Task<bool> ExcluirTarefa(int idTarefa);
        Task<List<Tarefa>> BuscarTodasTarefas(int numeroPagina, int totalItens);
        Task<int> ContarTarefas();
        Task<Tarefa> AtualizarStatustarefa(TarefaAtualizarStatusDto tarefaAtualizarStatusDto);
        Task<Tarefa> ConcluirTarefa(Tarefa tarefa);
    }
}
