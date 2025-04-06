using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensoes
{
    public static class ValidaAcessoRota
    {
        public static void ValidarAcessoRota(int idTokenUsuario, int idUsuarioReq, string roleTokenUsuario, bool adminDeveAcessar)
        {
            if (idTokenUsuario != idUsuarioReq)
            {
                if((roleTokenUsuario != "Administrador") || (roleTokenUsuario == "Administrador" && !adminDeveAcessar))
                {
                    throw new UnauthorizedAccessException("Usuário não autorizado");
                }
            }
        }
    }
}
