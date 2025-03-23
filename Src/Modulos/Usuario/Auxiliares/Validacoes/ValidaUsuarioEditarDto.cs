using FluentValidation;
using ModuloUsuario.Dtos.Entrada.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Auxiliares.Validacoes
{
    public class ValidaUsuarioEditarDto : AbstractValidator<UsuarioEditarDto>
    {
        public ValidaUsuarioEditarDto()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(Mensagens.Usuario.NomeObrigatorio)
                .Length(4, 50).WithMessage(Mensagens.Usuario.NomeTamanho)
                .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage(Mensagens.Usuario.NomeFormato);
            RuleFor(x => x.Credenciais)
                .NotNull().WithMessage(Mensagens.Credenciais.CredenciaisObrigatorias)
                .SetValidator(new ValidaCredenciaisEditarDto());
        }
    }
}
