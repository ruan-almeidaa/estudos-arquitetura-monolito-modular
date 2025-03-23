using FluentValidation;
using ModuloUsuario.Dtos.Entrada.Credenciais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Auxiliares.Validacoes
{
    public class ValidaCredenciaisEditarDto : AbstractValidator<CredenciaisEditarDto>
    {
        public ValidaCredenciaisEditarDto()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(Mensagens.Credenciais.EmailObrigatorio)
                .EmailAddress().WithMessage(Mensagens.Credenciais.EmailInvalido);
        }
    }
}
