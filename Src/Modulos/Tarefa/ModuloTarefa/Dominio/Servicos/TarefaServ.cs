using ModuloTarefa.Dominio.Interfaces.Repositorios;
using ModuloTarefa.Dominio.Interfaces.Servicos;
using ModuloTarefa.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Dominio.Servicos
{
    public class TarefaServ : ITarefaServ
    {
        private readonly ITarefaRepo _tarefaRepo;
        public TarefaServ(ITarefaRepo tarefaRepo)
        {
            _tarefaRepo = tarefaRepo;
        }
        public async Task<Tarefa> BuscarTarefaPorId(int id)
        {
            return await _tarefaRepo.BuscarTarefaPorId(id);
        }

        public async Task<List<Tarefa>> BuscarTodasTarefas(int numeroPagina, int totalItens)
        {
            return await _tarefaRepo.BuscarTodasTarefas(numeroPagina, totalItens);
        }

        public async Task<int> ContarTarefas()
        {
            return await _tarefaRepo.ContarTarefas();
        }

        public async Task<Tarefa> CriarTarefa(Tarefa tarefa)
        {
            return await _tarefaRepo.CriarTarefa(tarefa);
        }

        public async Task<Tarefa> EditarTarefa(Tarefa tarefa)
        {
            return await _tarefaRepo.EditarTarefa(tarefa);
        }

        public async Task<bool> ExcluirTarefa(int idTarefa)
        {
            return await _tarefaRepo.ExcluirTarefa(await BuscarTarefaPorId(idTarefa));
        }
    }
}
