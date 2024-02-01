using DatajudClient.Domain.Enum;

namespace DatajudClient.Domain.DTO.Shared
{
    public class RetornoApi<T> where T : class
    {
        public T? Dados { get; set; }
        public string Mensagem { get; set; }
        public List<string>? Erros { get; set; }

        public RetornoApi()
        {
            Dados = null;
            Mensagem = string.Empty;
            Erros = new List<string>();
        }
    }
}
