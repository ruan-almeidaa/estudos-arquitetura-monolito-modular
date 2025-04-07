using FluentValidation;
using ModuloTarefa.Dtos.Entrada;
using ModuloTarefa.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Auxiliares.Validacoes.Tarefa
{
    public class ValidaTarefaAtualizarStatusDto : AbstractValidator<TarefaAtualizarStatusDto>
    {
        public ValidaTarefaAtualizarStatusDto()
        {
            RuleFor(x => x.AdminId)
                .GreaterThan(0).WithMessage(Mensagens.Tarefa.AdminNecessario);
            RuleFor(x => x.UsuarioId)
                .Must(usuarioId => !usuarioId.HasValue || usuarioId.Value > 0).WithMessage(Mensagens.Usuario.IdInformadoMaiorQueZero);
            RuleFor(x => x.Status)
                .Must(valor => Enum.IsDefined(typeof(StatusTarefa), valor)).WithMessage("Status inválido");
        }
    }
}
