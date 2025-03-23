using ModuloUsuario.Dtos.Entrada.Credenciais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Dtos.Entrada.Usuario
{
    public class UsuarioEditarDto
    {
        public required int Id { get; set; }
        public required string Nome { get; set; }
        public required CredenciaisEditarDto Credenciais { get; set; }
    }
}
