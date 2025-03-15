using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Enumeradores
{
    public enum NivelUsuario
    {
        [Description("Normal")]
        Normal = 1,
        [Description("Administrador ")]
        Administrador = 2
    }
}
