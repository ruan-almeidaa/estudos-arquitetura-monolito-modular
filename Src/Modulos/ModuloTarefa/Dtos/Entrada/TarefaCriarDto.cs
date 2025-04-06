using ModuloTarefa.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Dtos.Entrada
{
    public class TarefaCriarDto
    {
        public required int AdminId { get; set; }
        public required string Titulo { get; set; }
        public required string Descricao { get; set; }
        public int? UsuarioId { get; set; }
        public required int Status { get; set; }
    }
}
