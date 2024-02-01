using System.ComponentModel.DataAnnotations;

namespace DatajudClient.Domain.DTO.Processos
{
    public class ReadProcessoDTO
    {
        public int Id { get; set; }
        public string NumeroProcesso { get; set; }
        public string NomeCaso { get; set; }
        public string? Vara { get; set; }
        public string Comarca { get; set; }
        public string Estado { get; set; }
        public string? Observacao { get; set; }
    }
}
