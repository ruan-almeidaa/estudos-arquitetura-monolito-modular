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

        }

        public static class Credenciais
        {
            public const string EmailObrigatorio = "E-mail é obrigatório.";
            public const string EmailNaoCadastrado = "E-mail não cadastrado.";
            public const string EmailJaCadastrado = "E-mail já cadastrado.";


            public const string SenhaIncorreta = "Senha incorreta.";
            public const string SenhaObrigatoria = "Senha é obrigatória.";
        }

    }
}
