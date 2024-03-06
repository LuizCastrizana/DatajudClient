using DatajudClient.Domain.Enum;

namespace DatajudClient.Domain.DTO.Shared
{
    public class RetornoApi<T>
    {
        public T? dados { get; set; }
        public string mensagem { get; set; }
        public List<string>? erros { get; set; }
        public int status { get; set; }

        public RetornoApi()
        {
            dados = default(T);
            mensagem = string.Empty;
            erros = new List<string>();
        }
    }
}
