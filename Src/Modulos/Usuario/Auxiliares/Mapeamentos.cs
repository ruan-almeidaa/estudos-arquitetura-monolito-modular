using AutoMapper;
using ModuloUsuario.Dtos.Entrada.Usuario;
using ModuloUsuario.Dtos.Saida;
using ModuloUsuario.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Auxiliares
{
    public class Mapeamentos : Profile
    {
        public Mapeamentos() 
        {
            CreateMap<Usuario, UsuarioAutenticadoDto>();
            CreateMap<UsuarioCriarDto, Usuario>();
        }
    }
}
