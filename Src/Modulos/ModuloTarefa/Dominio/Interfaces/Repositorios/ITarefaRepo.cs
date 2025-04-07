using ModuloTarefa.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Dominio.Interfaces.Repositorios
{
    public interface ITarefaRepo
    {
        Task<Tarefa> CriarTarefa(Tarefa tarefa);
        Task<Tarefa> BuscarTarefaPorId(int id);
        Task<Tarefa> EditarTarefa(Tarefa tarefa);
        Task<bool> ExcluirTarefa(Tarefa tarefa);
        Task<List<Tarefa>> BuscarTodasTarefas(int numeroPagina, int totalItens);
        Task<int> ContarTarefas();
        Task<Tarefa> AtualizarStatusTarefa(Tarefa tarefa);
    }
}
