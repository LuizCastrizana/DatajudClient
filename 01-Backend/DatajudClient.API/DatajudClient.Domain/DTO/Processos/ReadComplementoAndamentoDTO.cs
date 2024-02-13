using DatajudClient.Domain.Models.Processos;

namespace DatajudClient.Domain.DTO.Processos
{
    public class ReadComplementoAndamentoDTO
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public int Valor { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
