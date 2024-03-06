using DatajudClient.Domain.DTO.Enderecos;
using DatajudClient.Domain.DTO.Tribunais;

namespace DatajudClient.Domain.DTO.Processos
{
    public class UpdateDadosProcessoDTO
    {
        public string? NumeroProcesso { get; set; }
        public string? NomeCaso { get; set; }
        public string? Vara { get; set; }
        public string? Comarca { get; set; }
        public string? Observacao { get; set; }
        public int? EstadoId { get; set; }
        public int? TribunalId { get; set; }
    }
}
