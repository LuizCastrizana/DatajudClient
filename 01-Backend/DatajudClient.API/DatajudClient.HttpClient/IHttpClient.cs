namespace DatajudClient.HttpClient
{
    public interface IHttpClient
    {
        public Task<RetornoHttpClient> ExecutarRequisicaoAsync<T>(string uri, HttpMethod metodo, T Objeto);

        public Task<RetornoHttpClient> ExecutarRequisicaoAsync(string endpoint, HttpMethod metodo, int id = 0);

        public Task<RetornoHttpClient> ExecutarRequisicaoAsync(string uri, HttpMethod metodo, (string nome, string valor)[] Parametros);

        public Task<RetornoHttpClient> ExecutarRequisicaoAsync<T>(string uri, HttpMethod metodo, int id, T Objeto);
    }
}
