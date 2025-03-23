using AutoMapper;
using Extensoes;
using ModuloUsuario.Auxiliares;
using ModuloUsuario.Dominio.Interfaces.Servicos;
using ModuloUsuario.Dtos.Entrada.Usuario;
using ModuloUsuario.Dtos.Saida.Usuario;
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

        public async Task<PadraoRespostasApi<UsuarioAutenticadoDto>> AutenticarUsuario(UsuarioAutenticarDto usuarioAutenticarDto)
        {
            bool emailExiste = await _credenciaisServ.VerificaEmailExiste(usuarioAutenticarDto.Email);
            if (!emailExiste) return PadraoRespostasApi<UsuarioAutenticadoDto>
                    .CriarResposta<UsuarioAutenticadoDto>(null, Mensagens.Credenciais.EmailInvalido, System.Net.HttpStatusCode.BadRequest);

            bool credenciaisValidas = await _credenciaisServ.ExisteEmailSenha(usuarioAutenticarDto.Email, usuarioAutenticarDto.Senha);
            if (!credenciaisValidas) return PadraoRespostasApi<UsuarioAutenticadoDto>
                    .CriarResposta<UsuarioAutenticadoDto>(null, Mensagens.Credenciais.SenhaIncorreta, System.Net.HttpStatusCode.BadRequest);

            Usuario usuario = await _usuarioServ.BuscarUsuarioPorId(await _credenciaisServ.BuscarIdUsuarioPorEmail(usuarioAutenticarDto.Email));
            UsuarioAutenticadoDto usuarioAutenticadoDto = _mapper.Map<UsuarioAutenticadoDto>(usuario);

            usuarioAutenticadoDto.Token = await _usuarioServ.GerarToken(usuario);

            return PadraoRespostasApi<UsuarioAutenticadoDto>
                .CriarResposta<UsuarioAutenticadoDto>(usuarioAutenticadoDto, Mensagens.Credenciais.Autenticado, System.Net.HttpStatusCode.OK);

        }

        public async Task<PadraoRespostasApi<UsuarioAutenticadoDto>> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            bool emailExiste = await _credenciaisServ.VerificaEmailExiste(usuarioCriarDto.Credenciais.Email);
            if(emailExiste) throw new InvalidOperationException(Mensagens.Credenciais.EmailJaCadastrado);


            Usuario usuarioCriado = await _usuarioServ.CriarUsuario(_mapper.Map<Usuario>(usuarioCriarDto));
            UsuarioAutenticadoDto usuarioAutenticadoDto = _mapper.Map<UsuarioAutenticadoDto>(usuarioCriado);

            usuarioAutenticadoDto.Token = await _usuarioServ.GerarToken(usuarioCriado);

            return PadraoRespostasApi<UsuarioAutenticadoDto>
                .CriarResposta<UsuarioAutenticadoDto>(usuarioAutenticadoDto, Mensagens.Usuario.UsuarioCriado, System.Net.HttpStatusCode.Created);
        }

        public async Task<PadraoRespostasApi<UsuarioAutenticadoDto>> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
        {
            Usuario usuarioAntesDeEditar = await _usuarioServ.BuscarUsuarioPorId(usuarioEditarDto.Id);

            bool alterouEmail = usuarioAntesDeEditar.Credenciais.Email != usuarioEditarDto.Credenciais.Email;
            if (alterouEmail)
            {
                bool emailExiste = await _credenciaisServ.VerificaEmailExiste(usuarioEditarDto.Credenciais.Email);
                if (emailExiste) throw new InvalidOperationException(Mensagens.Credenciais.EmailJaCadastrado);

            }

            Credenciais credenciaisAntesDeEditar = new()
            {
                Id = usuarioAntesDeEditar.Credenciais.Id,
                Email = usuarioEditarDto.Credenciais.Email,
                Senha = usuarioAntesDeEditar.Credenciais.Senha,
                UsuarioId = usuarioAntesDeEditar.Id
            };

            Usuario usuarioEditado = new()
            {
                Id = usuarioEditarDto.Id,
                Nome = usuarioEditarDto.Nome,
                NivelUsuario = usuarioAntesDeEditar.NivelUsuario,
                DataCadastro = usuarioAntesDeEditar.DataCadastro,
                Credenciais = credenciaisAntesDeEditar
            };

            await _usuarioServ.EditarUsuario(usuarioEditado);

            UsuarioAutenticadoDto usuarioAutenticadoDto = _mapper.Map<UsuarioAutenticadoDto>(usuarioEditado);
            usuarioAutenticadoDto.Token = await _usuarioServ.GerarToken(usuarioEditado);

            return PadraoRespostasApi<UsuarioAutenticadoDto>
                .CriarResposta<UsuarioAutenticadoDto>(usuarioAutenticadoDto, Mensagens.Usuario.UsuarioEditado, System.Net.HttpStatusCode.OK);

        }
    }
}
