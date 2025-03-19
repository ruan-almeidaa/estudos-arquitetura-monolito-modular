using AutoMapper;
using Extensoes;
using ModuloUsuario.Auxiliares;
using ModuloUsuario.Dominio.Interfaces.Servicos;
using ModuloUsuario.Dtos.Entrada.Usuario;
using ModuloUsuario.Dtos.Saida;
using ModuloUsuario.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Dominio.Servicos
{
    public class Orquestrador : IOrquestrador
    {
        private readonly ICredenciaisServ _credenciaisServ;
        private readonly IUsuarioServ _usuarioServ;
        private readonly IMapper _mapper;
        public Orquestrador(ICredenciaisServ credenciaisServ, IUsuarioServ usuarioServ, IMapper mapper)
        {
            _credenciaisServ = credenciaisServ;
            _usuarioServ = usuarioServ;
            _mapper = mapper;
        }
        public async Task<PadraoRespostasApi<UsuarioAutenticadoDto>> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            bool emailExiste = await _credenciaisServ.VerificaEmailExiste(usuarioCriarDto.Credenciais.Email);
            if(emailExiste) return PadraoRespostasApi<UsuarioAutenticadoDto>.CriarResposta<UsuarioAutenticadoDto>(null, Mensagens.Credenciais.EmailJaCadastrado, System.Net.HttpStatusCode.BadRequest);

            Usuario usuarioParaCriar = _mapper.Map<Usuario>(usuarioCriarDto);
            usuarioParaCriar.DataCadastro = DateOnly.FromDateTime(DateTime.Now);


            Usuario usuarioCriado = await _usuarioServ.CriarUsuario(usuarioParaCriar);
            UsuarioAutenticadoDto usuarioAutenticadoDto = _mapper.Map<UsuarioAutenticadoDto>(usuarioCriado);

            return PadraoRespostasApi<UsuarioAutenticadoDto>
                .CriarResposta<UsuarioAutenticadoDto>(usuarioAutenticadoDto, Mensagens.Usuario.UsuarioCriado, System.Net.HttpStatusCode.Created);
        }
    }
}
