using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Auxiliares
{
    public static class Mensagens
    {
        public static class Usuario
        {
            public const string NomeObrigatorio = "Nome é obrigatório.";
            public const string NomeTamanho = "O nome deve ter entre 4 e 50 caracteres.";
            public const string NomeFormato = "O nome deve conter apenas letras e espaços.";
            public const string UsuarioCriado = "Usuário criado com sucesso.";

        }

        public static class Credenciais
        {
            public const string CredenciaisObrigatorias = "Credenciais são obrigatórias.";

            public const string EmailObrigatorio = "E-mail é obrigatório.";
            public const string EmailNaoCadastrado = "E-mail não cadastrado.";
            public const string EmailJaCadastrado = "E-mail já cadastrado.";
            public const string EmailInvalido = "E-mail inválido.";


            public const string SenhaIncorreta = "Senha incorreta.";
            public const string SenhaObrigatoria = "Senha é obrigatória.";
            public const string SenhaTamanho = "A senha deve ter entre 6 e 20 caracteres.";
        }

    }
}
