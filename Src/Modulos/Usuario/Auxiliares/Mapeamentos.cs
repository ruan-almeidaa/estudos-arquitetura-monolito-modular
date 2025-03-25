using AutoMapper;
using ModuloUsuario.Dtos.Entrada.Credenciais;
using ModuloUsuario.Dtos.Entrada.Usuario;
using ModuloUsuario.Dtos.Saida.Credenciais;
using ModuloUsuario.Dtos.Saida.Usuario;
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
            CreateMap<Usuario, UsuarioDetalhadoDto>();

            CreateMap<CredenciaisCriarDto,  Credenciais>();
            CreateMap<Credenciais, CredenciaisDto>();
        }
    }
}
