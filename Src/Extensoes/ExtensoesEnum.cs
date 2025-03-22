using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Extensoes
{
    public static class ExtensoesEnum
    {
        public static string BuscaDescricao<TEnum>(TEnum value) where TEnum : Enum
        {
            /*Recebe um enum qualquer e retorna sua descrição.
            Se não houver "Description" configurado no enum, retorna o nome padrão do enum.*/
            var campo = value.GetType().GetField(value.ToString());
            var atributo = campo?.GetCustomAttribute<DescriptionAttribute>();

            return atributo?.Description ?? value.ToString();
        }
    }
}
