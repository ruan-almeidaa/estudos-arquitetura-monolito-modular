using ModuloTarefa.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Entidades
{
    public class Tarefa
    {
        public int Id { get; set; }
        public required int AdminId { get; set; }
        public required string Titulo { get; set; }
        public required string Descricao { get; set; }
        public required DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public int? UsuarioId { get; set; }
        public required StatusTarefa Status { get; set; } = StatusTarefa.Pendente;
    }
}
