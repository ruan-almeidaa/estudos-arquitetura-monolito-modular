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
            RuleFor(x => x.Nome).NotEmpty().WithMessage(Mensagens.Usuario.NomeObrigatorio);
            RuleFor(x => x.Email).NotEmpty().WithMessage(Mensagens.Credenciais.EmailObrigatorio);
            RuleFor(x => x.Senha).NotEmpty().WithMessage(Mensagens.Credenciais.SenhaObrigatoria);
        }
    }
}
