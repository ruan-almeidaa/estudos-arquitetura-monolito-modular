using ModuloUsuario.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required NivelUsuario NivelUsuario { get; set; }
        public required DateOnly DataCadastro { get; set; }

        public required Credenciais Credenciais { get; set; }
    }
}
