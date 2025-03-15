using System.Net;

namespace Extensoes
{
    public class PadraoRespostasApi<T>
    {
        public T? Dados { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;

        public static PadraoRespostasApi<T> CriarResposta<T>(T? dados, string mensagem = "Ok", HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return new PadraoRespostasApi<T>
            {
                Dados = dados,
                Mensagem = mensagem,
                HttpStatusCode = httpStatusCode

            };
        }
    }
}
