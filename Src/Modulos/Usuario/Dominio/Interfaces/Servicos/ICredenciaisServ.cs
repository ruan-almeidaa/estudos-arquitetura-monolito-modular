﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Dominio.Interfaces.Servicos
{
    public interface ICredenciaisServ
    {
        Task<bool> VerificaEmailExiste(string email);
        Task<bool> ExisteEmailSenha(string email, string senha);
        Task<int> BuscarIdUsuarioPorEmail(string email);
    }
}
