using AutoMapper;
using Extensoes;
using ModuloTarefa.Auxiliares.Integracoes.ModuloUsuario;
using ModuloTarefa.Auxiliares.Integracoes.ModuloUsuario.Dtos.Entrada;
using ModuloTarefa.Dominio.Interfaces.Servicos;
using ModuloTarefa.Dtos.Entrada;
using ModuloTarefa.Dtos.Saida;
using ModuloTarefa.Entidades;
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
            Tarefa tarefaCriada = await _tarefaServ.CriarTarefa(_mapper.Map<Tarefa>(tarefaCriarDto));
            if (tarefaCriarDto.UsuarioId.HasValue)
            {
                UsuarioDetalhadoDto usuarioDetalhadoDto = await _usuarioHttpClient.BuscarUsuarioPorId(tarefaCriarDto.UsuarioId.Value);
            }

                 throw new NotImplementedException();
        }
    }
}
