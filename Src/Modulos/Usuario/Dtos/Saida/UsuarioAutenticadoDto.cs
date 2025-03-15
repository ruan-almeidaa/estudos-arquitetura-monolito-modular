using ModuloUsuario.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Dtos.Saida
{
    public class UsuarioAutenticadoDto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required NivelUsuario NivelUsuario { get; set; }
        public required DateOnly DataCadastro { get; set; }
        public required string Token { get; set; }
    }
}
