using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Auxiliares
{
    public static class Mensagens
    {
        public static class Tarefa
        {
            public const string TituloObrigatorio = "Título é obrigatório";
            public const string TituloTamanho = "Título deve ter entre 4 e 50 caracteres";
            public const string DescricaoObrigatoria = "Descrição é obrigatória";
            public const string DescricaoTamanho = "Descrição deve ter entre 4 e 200 caracteres";
            public const string AdminNecessario = "É necessário vincular um administrador na tarefa.";
        }
        public static class Token
        {
            public const string TokenNaoEncontrado = "Token não encontrado";
        }
        public static class Usuario
        {
            public const string IdInformadoMaiorQueZero = "Se informado, o UsuarioId deve ser maior que zero.";
        }
    }
}
