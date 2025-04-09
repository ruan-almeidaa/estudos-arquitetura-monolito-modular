using Microsoft.EntityFrameworkCore;
using ModuloTarefa.Dominio.Interfaces.Repositorios;
using ModuloTarefa.Entidades;
using ModuloTarefa.Infra.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Infra.Repositorios
{
    public class TarefaRepo : ITarefaRepo
    {
        private readonly ConfiguracaoContextoBancoModuloTarefa _contexto;
        public TarefaRepo(ConfiguracaoContextoBancoModuloTarefa contexto)
        {
            _contexto = contexto;
        }

        public async Task<Tarefa> AtualizarStatusTarefa(Tarefa tarefa)
        {
            _contexto.ChangeTracker.Clear();
            _contexto.Entry(tarefa).Property(t => t.Status).IsModified = true;
            await _contexto.SaveChangesAsync();
            return await BuscarTarefaPorId(tarefa.Id);
        }

        public async Task<Tarefa> BuscarTarefaPorId(int id)
        {
            return await _contexto.Tarefas
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Tarefa>> BuscarTodasTarefas(int numeroPagina, int totalItens)
        {
            return await _contexto.Tarefas
                .AsNoTracking()
                .OrderBy(t => t.Id)
                .Skip((numeroPagina - 1) * totalItens) // Pula os registros das páginas anteriores
                .Take(totalItens) // Pega apenas os registros da página atual
                .ToListAsync();
        }

        public async Task<int> ContarTarefas()
        {
            return await _contexto.Tarefas.CountAsync();
        }

        public async Task<Tarefa> CriarTarefa(Tarefa tarefa)
        {
            await _contexto.Tarefas.AddAsync(tarefa);
            await _contexto.SaveChangesAsync();
            return await BuscarTarefaPorId(tarefa.Id);
        }

        public async Task<Tarefa> EditarTarefa(Tarefa tarefa)
        {
            _contexto.ChangeTracker.Clear();
            _contexto.Tarefas.Update(tarefa);
            await _contexto.SaveChangesAsync();
            return await BuscarTarefaPorId(tarefa.Id);
        }

        public async Task<bool> ExcluirTarefa(Tarefa tarefa)
        {
            _contexto.Tarefas.Remove(tarefa);
            int qtdRegistrosExcluidos = await _contexto.SaveChangesAsync();
            return qtdRegistrosExcluidos > 0;
        }
    }
}
