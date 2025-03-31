using Azure;
using Extensoes;
using Microsoft.AspNetCore.Http;
using ModuloTarefa.Auxiliares.Integracoes.ModuloUsuario.Dtos.Entrada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ModuloTarefa.Auxiliares.Integracoes.ModuloUsuario
{
    public class UsuarioHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioHttpClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UsuarioDetalhadoDto?> BuscarUsuarioPorId(int idUsuario)
        {
            // Obtendo o token JWT do contexto da requisição atual
            var token = _httpContextAccessor.HttpContext?.User.FindFirst("token")?.Value;
            if (string.IsNullOrEmpty(token)) throw new UnauthorizedAccessException(Mensagens.Token.TokenNaoEncontrado);

            // Adicionando o token JWT ao cabeçalho da requisição
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var resposta = await _httpClient.GetAsync($"/{idUsuario}");

            if (resposta.IsSuccessStatusCode)
            {
                return await resposta.Content.ReadFromJsonAsync<UsuarioDetalhadoDto>();
            }
            else
            {
                // Captura a resposta de erro e retorna a mensagem personalizada
                var erro = await resposta.Content.ReadFromJsonAsync<PadraoRespostasApi<UsuarioDetalhadoDto>>();
                throw new Exception(erro?.Mensagem ?? "Erro ao buscar usuário.");
            }
        }
    }
}
