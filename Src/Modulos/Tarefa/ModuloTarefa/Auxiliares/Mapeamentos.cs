using AutoMapper;
using ModuloTarefa.Dtos.Entrada;
using ModuloTarefa.Dtos.Saida;
using ModuloTarefa.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Auxiliares
{
    public class Mapeamentos : Profile
    {
        public Mapeamentos()
        {
            CreateMap<Tarefa, TarefaDetalhadaDto>();
            CreateMap<TarefaCriarDto, Tarefa>();
            CreateMap<Tarefa, TarefaDetalhadaDto>();
        }
    }
}
