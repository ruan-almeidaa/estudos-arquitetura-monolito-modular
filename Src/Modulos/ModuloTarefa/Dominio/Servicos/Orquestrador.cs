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
        public async Task<PadraoRespostasApi<TarefaDetalhadaDto>> CriarTarefa(TarefaCriarDto tarefaCriarDto)
        {
            UsuarioDetalhadoDto usuarioTarefa = null;
            if (tarefaCriarDto.UsuarioId.HasValue)
            {
                // Caso Usuario não exista, vai lançar uma exceção
                usuarioTarefa = await _usuarioHttpClient.BuscarUsuarioPorId(tarefaCriarDto.UsuarioId.Value);
            }

            Tarefa tarefaCriada = await _tarefaServ.CriarTarefa(_mapper.Map<Tarefa>(tarefaCriarDto));

            //Busca o administrador responsável pela tarefa
            UsuarioDetalhadoDto adminTarefa = await _usuarioHttpClient.BuscarUsuarioPorId(tarefaCriarDto.AdminId);
            TarefaDetalhadaDto tarefaDetalhadaDto = _mapper.Map<TarefaDetalhadaDto>(tarefaCriada);

            tarefaDetalhadaDto.Usuario = usuarioTarefa;
            tarefaDetalhadaDto.Administrador = adminTarefa;
            tarefaDetalhadaDto.StatusDescricao = ExtensoesEnum.BuscaDescricao(tarefaCriada.Status);

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

            //Prepara o retorno detalhado
            TarefaDetalhadaDto tarefaDetalhadaDto = _mapper.Map<TarefaDetalhadaDto>(tarefaEditada);
            //Busca o administrador responsável pela tarefa
            UsuarioDetalhadoDto adminTarefa = await _usuarioHttpClient.BuscarUsuarioPorId(tarefaEditarDto.AdminId);
            tarefaDetalhadaDto.Usuario = usuarioTarefa;
            tarefaDetalhadaDto.Administrador = adminTarefa;
            tarefaDetalhadaDto.StatusDescricao = ExtensoesEnum.BuscaDescricao(tarefaEditada.Status);

            return PadraoRespostasApi<TarefaDetalhadaDto>
                .CriarResposta<TarefaDetalhadaDto>(tarefaDetalhadaDto, Mensagens.Tarefa.Editada, System.Net.HttpStatusCode.OK);
        }
    }
}
