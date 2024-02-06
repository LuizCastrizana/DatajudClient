using System.ComponentModel.DataAnnotations;

namespace DatajudClient.Domain.DTO.Processos
{
    public class CreateProcessoDTO
    {
        [Required]
        public string NumeroProcesso { get; set; }
        [Required]
        public string NomeCaso { get; set; }
        public string? Vara { get; set; }
        [Required]
        public string Comarca { get; set; }
        [Required]
        public int EstadoId { get; set; }
        [Required]
        public int TribunalId { get; set; }
        public string? Observacao { get; set; }
    }
}
