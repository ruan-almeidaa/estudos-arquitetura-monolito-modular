using Extensoes;
using Microsoft.AspNetCore.Mvc;
using ModuloUsuario.Dominio.Interfaces.Servicos;
using ModuloUsuario.Dtos.Entrada.Usuario;
using ModuloUsuario.Dtos.Saida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Api
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IOrquestrador _orquestrador;
        public UsuarioController(IOrquestrador orquestrador)
        {
            _orquestrador = orquestrador;
        }

        [HttpPost]
        public async Task<ActionResult<PadraoRespostasApi<UsuarioAutenticadoDto>>> CriarUsuario([FromBody] UsuarioCriarDto usuarioCriarDto)
        {
            try
            {
                return await _orquestrador.CriarUsuario(usuarioCriarDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, PadraoRespostasApi<UsuarioAutenticadoDto>
                    .CriarResposta<UsuarioAutenticadoDto>(null, $"Ocorreu um erro: {ex.Message}", System.Net.HttpStatusCode.InternalServerError));
            }
        }

    }
}
