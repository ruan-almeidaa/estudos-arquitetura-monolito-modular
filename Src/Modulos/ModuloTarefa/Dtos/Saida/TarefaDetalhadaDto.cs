using ModuloTarefa.Auxiliares.Integracoes.ModuloUsuario.Dtos.Entrada;
using ModuloTarefa.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Dtos.Saida
{
    public class TarefaDetalhadaDto
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public required string Descricao { get; set; }
        public required DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public required StatusTarefa Status { get; set; } = StatusTarefa.Pendente;
        public required UsuarioDetalhadoDto Administrador { get; set; }
        public UsuarioDetalhadoDto? Usuario { get; set; }
    }
}
