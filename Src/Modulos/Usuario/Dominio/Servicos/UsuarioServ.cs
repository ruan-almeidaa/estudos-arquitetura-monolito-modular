using Extensoes;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModuloUsuario.Auxiliares;
using ModuloUsuario.Dominio.Interfaces.Repositorios;
using ModuloUsuario.Dominio.Interfaces.Servicos;
using ModuloUsuario.Entidades;
using ModuloUsuario.Enumeradores;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ModuloUsuario.Dominio.Servicos
{
    public class UsuarioServ : IUsuarioServ
    {
        private readonly IUsuarioRepo _usuarioRepo;
        private readonly IConfiguration _config;
        public UsuarioServ(IUsuarioRepo usuarioRepo, IConfiguration configuration)
        {
            _usuarioRepo = usuarioRepo;
            _config = configuration;
        }

        public async Task<List<Usuario>> BuscarTodosUsuarios()
        {
            return await _usuarioRepo.BuscarTodosUsuarios();
        }

        public async Task<Usuario> BuscarUsuarioPorId(int id)
        {
           return await _usuarioRepo.BuscarUsuarioPorId(id);
        }

        public async Task<Usuario> CriarUsuario(Usuario usuario)
        {
            usuario.DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            usuario.Credenciais.Senha = Criptografia.GerarHash(usuario.Credenciais.Senha);
            return await _usuarioRepo.CriarUsuario(usuario);
        }

        public Task<Usuario> EditarUsuario(Usuario usuario)
        {
            return _usuarioRepo.EditarUsuario(usuario);
        }

        public async Task<bool> ExcluirUsuario(int idUsuario)
        {
            Usuario usuario = await _usuarioRepo.BuscarUsuarioPorId(idUsuario);

            return usuario != null ? await _usuarioRepo.ExcluirUsuario(usuario) : false;
        }

        public async Task<string> GerarToken(Usuario usuario)
        {
            String secret = _config["Jwt:Secret"];
            if (String.IsNullOrEmpty(secret)) throw new InvalidOperationException(Mensagens.Token.SecretObrigatorio);
            if(Encoding.UTF8.GetBytes(secret).Length < 32) throw new InvalidOperationException(Mensagens.Token.SecretTamanho);

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Credenciais.Email),
                new Claim(JwtRegisteredClaimNames.Name, usuario.Nome),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, ExtensoesEnum.BuscaDescricao(usuario.NivelUsuario))

            };

            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
