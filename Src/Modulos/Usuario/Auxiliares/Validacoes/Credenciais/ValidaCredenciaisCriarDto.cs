using FluentValidation;
using ModuloUsuario.Dtos.Entrada.Credenciais;
using ModuloUsuario.Dtos.Entrada.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Auxiliares.Validacoes.Credenciais
{
    public class ValidaCredenciaisCriarDto : AbstractValidator<CredenciaisCriarDto>
    {
        public ValidaCredenciaisCriarDto()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(Mensagens.Credenciais.EmailObrigatorio)
                .EmailAddress().WithMessage(Mensagens.Credenciais.EmailInvalido);


            RuleFor(x => x.Senha)
                .Length(10, 50).WithMessage(Mensagens.Credenciais.SenhaTamanho)
                .NotEmpty().WithMessage(Mensagens.Credenciais.SenhaObrigatoria);
        }
    }
}
