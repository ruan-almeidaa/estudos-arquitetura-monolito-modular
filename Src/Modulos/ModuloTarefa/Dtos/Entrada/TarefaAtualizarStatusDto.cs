using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Dtos.Entrada
{
    public class TarefaAtualizarStatusDto
    {
        public required int Id { get; set; }
        public required int AdminId { get; set; }
        public int? UsuarioId { get; set; }
        public required int Status { get; set; }
    }
}
