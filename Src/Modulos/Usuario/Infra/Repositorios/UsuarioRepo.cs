using ModuloUsuario.Dominio.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Infra.Repositorios
{
    public class UsuarioRepo : IUsuarioRepo
    {
        public Task<bool> VerificaEmailExiste(string email)
        {
            throw new NotImplementedException();
        }
    }
}
