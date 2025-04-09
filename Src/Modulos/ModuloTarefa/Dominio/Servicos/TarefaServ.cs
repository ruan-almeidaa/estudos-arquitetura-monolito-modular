using Microsoft.AspNetCore.Http;
using ModuloTarefa.Auxiliares;
using ModuloTarefa.Dominio.Interfaces.Repositorios;
using ModuloTarefa.Dominio.Interfaces.Servicos;
using ModuloTarefa.Dtos.Entrada;
using ModuloTarefa.Entidades;
using ModuloTarefa.Enumeradores;
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

        public async Task<Tarefa> AtualizarStatustarefa(TarefaAtualizarStatusDto tarefaAtualizarStatusDto)
        {
            Tarefa tarefa = await BuscarTarefaPorId(tarefaAtualizarStatusDto.Id);
            if (tarefa.Status == StatusTarefa.Concluida) throw new BadHttpRequestException(Mensagens.Tarefa.TarefaJaConcluida);
            if (tarefa == null) throw new KeyNotFoundException(Mensagens.Tarefa.TarefaNaoEncontrada);

            tarefa.Status = (StatusTarefa)tarefaAtualizarStatusDto.Status;
            if (tarefa.Status == StatusTarefa.Concluida)
            {
                tarefa = await ConcluirTarefa(tarefa);
            }

            return await _tarefaRepo.AtualizarStatusTarefa(tarefa);
        }

        public async Task<Tarefa> BuscarTarefaPorId(int id)
        {
            return await _tarefaRepo.BuscarTarefaPorId(id);
        }

        public async Task<List<Tarefa>> BuscarTodasTarefas(int numeroPagina, int totalItens)
        {
            return await _tarefaRepo.BuscarTodasTarefas(numeroPagina, totalItens);
        }

        public async Task<Tarefa> ConcluirTarefa(Tarefa tarefa)
        {
            tarefa.DataConclusao = DateTime.Now;
            return await EditarTarefa(tarefa);
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
