using Extensoes;
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
        public Task<PadraoRespostasApi<UsuarioAutenticadoDto>> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            throw new NotImplementedException();
        }
    }
}
