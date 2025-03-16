using FluentValidation;
using ModuloUsuario.Dtos.Entrada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Auxiliares.Validacoes
{
    public class ValidaUsuarioCriarDto: AbstractValidator<UsuarioCriarDto>
    {
        public ValidaUsuarioCriarDto()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(Mensagens.Usuario.NomeObrigatorio)
                .Length(4, 50).WithMessage(Mensagens.Usuario.NomeTamanho)
                .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage(Mensagens.Usuario.NomeFormato);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(Mensagens.Credenciais.EmailObrigatorio)
                .EmailAddress().WithMessage(Mensagens.Credenciais.EmailInvalido);


            RuleFor(x => x.Senha)
                .Length(10, 50).WithMessage(Mensagens.Credenciais.SenhaTamanho)
                .NotEmpty().WithMessage(Mensagens.Credenciais.SenhaObrigatoria);
        }
    }
}
