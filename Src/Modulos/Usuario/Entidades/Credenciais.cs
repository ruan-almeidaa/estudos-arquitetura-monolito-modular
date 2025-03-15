using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Entidades
{
    public class Credenciais
    {
        public int Id { get; set; }
        public required int UsuarioId { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required Usuario Usuario { get; set; }
    }
}
