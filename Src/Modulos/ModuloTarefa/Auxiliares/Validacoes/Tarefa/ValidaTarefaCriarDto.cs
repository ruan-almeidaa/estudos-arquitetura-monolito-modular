using FluentValidation;
using ModuloTarefa.Dtos.Entrada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Auxiliares.Validacoes.Tarefa
{
    public class ValidaTarefaCriarDto : AbstractValidator<TarefaCriarDto>
    {
        public ValidaTarefaCriarDto()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage(Mensagens.Tarefa.TituloObrigatorio)
                .Length(4, 50).WithMessage(Mensagens.Tarefa.TituloTamanho);
            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage(Mensagens.Tarefa.DescricaoObrigatoria)
                .Length(4, 200).WithMessage(Mensagens.Tarefa.DescricaoTamanho);
            RuleFor(x => x.AdminId)
                .GreaterThan(0).WithMessage(Mensagens.Tarefa.AdminNecessario);
            RuleFor(x => x.UsuarioId)
                .Must(usuarioId => !usuarioId.HasValue || usuarioId.Value > 0).WithMessage(Mensagens.Usuario.IdInformadoMaiorQueZero);

        }
    }
}
