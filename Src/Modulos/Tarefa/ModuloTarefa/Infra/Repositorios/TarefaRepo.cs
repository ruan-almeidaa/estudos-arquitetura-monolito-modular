using ModuloTarefa.Dominio.Interfaces.Repositorios;
using ModuloTarefa.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Infra.Repositorios
{
    public class TarefaRepo : ITarefaRepo
    {
        public Task<Tarefa> BuscarTarefaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Tarefa>> BuscarTodasTarefas(int numeroPagina, int totalItens)
        {
            throw new NotImplementedException();
        }

        public Task<int> ContarTarefas()
        {
            throw new NotImplementedException();
        }

        public Task<Tarefa> CriarTarefa(Tarefa tarefa)
        {
            throw new NotImplementedException();
        }

        public Task<Tarefa> EditarTarefa(Tarefa tarefa)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExcluirTarefa(Tarefa tarefa)
        {
            throw new NotImplementedException();
        }
    }
}
