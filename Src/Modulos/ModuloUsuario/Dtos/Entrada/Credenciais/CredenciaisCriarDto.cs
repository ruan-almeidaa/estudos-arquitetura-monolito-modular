using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Dtos.Entrada.Credenciais
{
    public class CredenciaisCriarDto
    {
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }
}
