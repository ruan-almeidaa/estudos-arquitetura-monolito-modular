using AutoMapper;
using Extensoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using ModuloTarefa.Auxiliares;
using ModuloTarefa.Auxiliares.Integracoes.ModuloUsuario;
using ModuloTarefa.Auxiliares.Integracoes.ModuloUsuario.Dtos.Entrada;
using ModuloTarefa.Dominio.Interfaces.Servicos;
using ModuloTarefa.Dtos.Entrada;
using ModuloTarefa.Dtos.Saida;
using ModuloTarefa.Entidades;
using ModuloTarefa.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Dominio.Servicos
{
    public class Orquestrador : IOrquestrador
    {
        private readonly ITarefaServ _tarefaServ;
        private readonly UsuarioHttpClient _usuarioHttpClient;
        private readonly IMapper _mapper;
        public Orquestrador(ITarefaServ tarefaServ, UsuarioHttpClient usuarioHttpClient, IMapper mapper)
        {
            _tarefaServ = tarefaServ;
            _usuarioHttpClient = usuarioHttpClient;
            _mapper = mapper;
        }

        public async Task<PadraoRespostasApi<TarefaDetalhadaDto>> AtualizarStatusTarefa(TarefaAtualizarStatusDto tarefaAtualizarStatusDto)
        {
            UsuarioDetalhadoDto usuarioTarefa = null;
            if (tarefaAtualizarStatusDto.UsuarioId.HasValue)
            {
                // Caso Usuario não exista, vai lançar uma exceção
                usuarioTarefa = await _usuarioHttpClient.BuscarUsuarioPorId(tarefaAtualizarStatusDto.UsuarioId.Value);
            }

            Tarefa tarefaStatusAtualizado = await _tarefaServ.AtualizarStatustarefa(tarefaAtualizarStatusDto);
            TarefaDetalhadaDto tarefaDetalhadaDto = await _tarefaServ.ConverteParaDetalhada(tarefaStatusAtualizado);

            return PadraoRespostasApi<TarefaDetalhadaDto>
                .CriarResposta<TarefaDetalhadaDto>(tarefaDetalhadaDto, Mensagens.Tarefa.Concluida, System.Net.HttpStatusCode.OK);


        }

        public async Task<PadraoRespostasApi<TarefaDetalhadaDto>> BuscarTarefaPorId(int idTarefa)
        {
            Tarefa tarefa = await _tarefaServ.BuscarTarefaPorId(idTarefa);
            if (tarefa == null) throw new KeyNotFoundException(Mensagens.Tarefa.TarefaNaoEncontrada);

            TarefaDetalhadaDto tarefaDetalhadaDto = await _tarefaServ.ConverteParaDetalhada(tarefa);

            return PadraoRespostasApi<TarefaDetalhadaDto>
                .CriarResposta<TarefaDetalhadaDto>(tarefaDetalhadaDto, Mensagens.Tarefa.TarefasEncontradas, System.Net.HttpStatusCode.OK);
        }

        public async Task<PadraoRespostasApi<Paginacao<TarefaDetalhadaDto>>> BuscarTodasTarefas(int numeroPagina, int totalItens)
        {
            List<Tarefa> tarefas = await _tarefaServ.BuscarTodasTarefas(numeroPagina, totalItens);

            if (tarefas == null) throw new KeyNotFoundException(Mensagens.Tarefa.TarefaNaoEncontrada);
            int totalTarefas = await _tarefaServ.ContarTarefas();
            List<TarefaDetalhadaDto> tarefaDetalhadaDtos = tarefas.Select(t => _mapper.Map<TarefaDetalhadaDto>(t)).ToList();
            Paginacao<TarefaDetalhadaDto> paginacao = new Paginacao<TarefaDetalhadaDto>
            {
                Itens = tarefaDetalhadaDtos,
                TotalItensParaExibir = totalTarefas,
                NumeroPaginaAtual = numeroPagina,
                TotalPaginasParaExibir = (int)Math.Ceiling((double)totalTarefas / totalItens)
            };
            return PadraoRespostasApi<Paginacao<TarefaDetalhadaDto>>
                .CriarResposta<Paginacao<TarefaDetalhadaDto>>(paginacao, Mensagens.Tarefa.TarefasEncontradas, System.Net.HttpStatusCode.OK);
        }

        public async Task<PadraoRespostasApi<TarefaDetalhadaDto>> CriarTarefa(TarefaCriarDto tarefaCriarDto)
        {
            UsuarioDetalhadoDto usuarioTarefa = null;
            if (tarefaCriarDto.UsuarioId.HasValue)
            {
                // Caso Usuario não exista, vai lançar uma exceção
                usuarioTarefa = await _usuarioHttpClient.BuscarUsuarioPorId(tarefaCriarDto.UsuarioId.Value);
            }

            Tarefa tarefaCriada = await _tarefaServ.CriarTarefa(_mapper.Map<Tarefa>(tarefaCriarDto));
            TarefaDetalhadaDto tarefaDetalhadaDto = await _tarefaServ.ConverteParaDetalhada(tarefaCriada);

            return PadraoRespostasApi<TarefaDetalhadaDto>
                .CriarResposta<TarefaDetalhadaDto>(tarefaDetalhadaDto, Mensagens.Tarefa.Criada, System.Net.HttpStatusCode.Created);


        }

        public async Task<PadraoRespostasApi<TarefaDetalhadaDto>> EditarTarefa(TarefaEditarDto tarefaEditarDto)
        {
            Tarefa tarefaAntesEditar = await _tarefaServ.BuscarTarefaPorId(tarefaEditarDto.Id);
            if(tarefaAntesEditar.Status == StatusTarefa.Concluida) throw new BadHttpRequestException(Mensagens.Tarefa.TarefaJaConcluida);
            if (tarefaAntesEditar == null) throw new KeyNotFoundException(Mensagens.Tarefa.TarefaNaoEncontrada);

            UsuarioDetalhadoDto usuarioTarefa = null;
            if (tarefaEditarDto.UsuarioId.HasValue)
            {
                // Caso Usuario não exista, vai lançar uma exceção
                usuarioTarefa = await _usuarioHttpClient.BuscarUsuarioPorId(tarefaEditarDto.UsuarioId.Value);
            }

            //Mapeia o DTO para entidade e ajusta os campos que não estão do DTO
            Tarefa tarefaParaEditar = _mapper.Map<Tarefa>(tarefaEditarDto);
            tarefaParaEditar.DataCriacao = tarefaAntesEditar.DataCriacao;
            tarefaParaEditar.Status = tarefaAntesEditar.Status;

            Tarefa tarefaEditada = await _tarefaServ.EditarTarefa(tarefaParaEditar);

            TarefaDetalhadaDto tarefaDetalhadaDto = await _tarefaServ.ConverteParaDetalhada(tarefaEditada);

            return PadraoRespostasApi<TarefaDetalhadaDto>
                .CriarResposta<TarefaDetalhadaDto>(tarefaDetalhadaDto, Mensagens.Tarefa.Editada, System.Net.HttpStatusCode.OK);
        }

        public async Task<PadraoRespostasApi<bool>> ExcluirTarefa(int idTarefa)
        {
            bool excluiuTarefa = await _tarefaServ.ExcluirTarefa(idTarefa);
            if (excluiuTarefa) return PadraoRespostasApi<bool>.CriarResposta<bool>(true, Mensagens.Tarefa.TarefaExcluida, System.Net.HttpStatusCode.OK);
            throw new InvalidOperationException(Mensagens.Tarefa.TarefaNaoExcluida);
        }
    }
}
