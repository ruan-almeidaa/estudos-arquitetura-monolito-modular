using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Dominio.Interfaces.Repositorios
{
    public interface ICredenciaisRepo
    {
        Task<bool> VerificaEmailExiste(string email);
    }
}
