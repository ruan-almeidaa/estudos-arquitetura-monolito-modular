using Extensoes;
using ModuloUsuario.Auxiliares;
using ModuloUsuario.Dominio.Interfaces.Servicos;
using ModuloUsuario.Dtos.Entrada;
using ModuloUsuario.Dtos.Saida;
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
        public Orquestrador(ICredenciaisServ credenciaisServ)
        {
            _credenciaisServ = credenciaisServ;
        }
        public async Task<PadraoRespostasApi<UsuarioAutenticadoDto>> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            bool emailExiste = await _credenciaisServ.VerificaEmailExiste(usuarioCriarDto.Email);
            if(emailExiste) return PadraoRespostasApi<UsuarioAutenticadoDto>.CriarResposta<UsuarioAutenticadoDto>(null, Mensagens.Credenciais.EmailJaCadastrado, System.Net.HttpStatusCode.BadRequest);

            throw new NotImplementedException();
        }
    }
}
