using DatajudClient.Domain.Enum;

namespace DatajudClient.Domain.DTO.Shared
{
    public class RetornoServico<T>
    {
        public T? Dados { get; set; }
        public StatusRetornoEnum Status { get; set; }
        public string Mensagem { get; set; }
        public List<string>? Erros { get; set; }

        public RetornoServico()
        {
            Status = StatusRetornoEnum.SUCESSO;
            Mensagem = string.Empty;
        }
    }
}
