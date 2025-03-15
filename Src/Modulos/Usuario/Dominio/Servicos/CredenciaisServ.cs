using ModuloUsuario.Dominio.Interfaces.Repositorios;
using ModuloUsuario.Dominio.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Dominio.Servicos
{
    public class CredenciaisServ : ICredenciaisServ
    {
        private readonly ICredenciaisRepo _credenciaisRepo;
        public CredenciaisServ(ICredenciaisRepo credenciaisRepo)
        {
            _credenciaisRepo = credenciaisRepo;
        }
        public async Task<bool> VerificaEmailExiste(string email)
        {
            return await _credenciaisRepo.VerificaEmailExiste(email);
        }
    }
}
