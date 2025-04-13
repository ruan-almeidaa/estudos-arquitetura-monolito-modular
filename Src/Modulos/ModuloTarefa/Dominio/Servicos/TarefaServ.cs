using AutoMapper;
using Extensoes;
using Microsoft.AspNetCore.Http;
using ModuloTarefa.Auxiliares;
using ModuloTarefa.Auxiliares.Integracoes.ModuloUsuario;
using ModuloTarefa.Auxiliares.Integracoes.ModuloUsuario.Dtos.Entrada;
using ModuloTarefa.Dominio.Interfaces.Repositorios;
using ModuloTarefa.Dominio.Interfaces.Servicos;
using ModuloTarefa.Dtos.Entrada;
using ModuloTarefa.Dtos.Saida;
using ModuloTarefa.Entidades;
using ModuloTarefa.Enumeradores;

namespace ModuloTarefa.Dominio.Servicos
{
    public class TarefaServ : ITarefaServ
    {
        private readonly ITarefaRepo _tarefaRepo;
        private readonly UsuarioHttpClient _usuarioHttpClient;
        private readonly IMapper _mapper;
        public TarefaServ(ITarefaRepo tarefaRepo,UsuarioHttpClient usuarioHttpClient, IMapper mapper)
        {
            _tarefaRepo = tarefaRepo;
            _usuarioHttpClient = usuarioHttpClient;
            _mapper = mapper;
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

        public async Task<List<Tarefa>> BuscarTarefasPorUsuarioId(int usuarioId, int numeroPagina, int totalItens)
        {
            return await _tarefaRepo.BuscarTarefasPorUsuarioId(usuarioId, numeroPagina, totalItens);
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

        public async Task<TarefaDetalhadaDto> ConverteParaDetalhada(Tarefa tarefa)
        {
            
            //Busca o administrador responsável pela tarefa
            UsuarioDetalhadoDto adminTarefa = await _usuarioHttpClient.BuscarUsuarioPorId(tarefa.AdminId);
            //Busca o usuário responsável pela tarefa
            UsuarioDetalhadoDto usuarioTarefa = null;
            if (tarefa.UsuarioId.HasValue)
            {
                try
                {
                    usuarioTarefa = await _usuarioHttpClient.BuscarUsuarioPorId(tarefa.UsuarioId.Value);

                }
                catch (Exception)
                {

                    usuarioTarefa = null;
                }
            }
            //Converte a tarefa para o DTO
            TarefaDetalhadaDto tarefaDetalhadaDto = _mapper.Map<TarefaDetalhadaDto>(tarefa);

            //Preenche os dados do DTO com os que foram buscados
            tarefaDetalhadaDto.Usuario = usuarioTarefa;
            tarefaDetalhadaDto.Administrador = adminTarefa;
            tarefaDetalhadaDto.StatusDescricao = ExtensoesEnum.BuscaDescricao(tarefa.Status);

            return tarefaDetalhadaDto;
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
