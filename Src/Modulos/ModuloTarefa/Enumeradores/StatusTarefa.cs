using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Enumeradores
{
    public enum StatusTarefa
    {
        [Description("Pendente")]
        Pendente = 0,
        [Description("Em andamento")]
        EmAndamento = 1,
        [Description("Concluída")]
        Concluida = 2
    }
}
