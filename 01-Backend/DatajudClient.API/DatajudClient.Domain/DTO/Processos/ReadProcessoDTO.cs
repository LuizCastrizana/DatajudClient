using DatajudClient.Domain.DTO.Enderecos;
using DatajudClient.Domain.DTO.Tribunais;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace DatajudClient.Domain.DTO.Processos
{
    public class ReadProcessoDTO
    {
        public int Id { get; set; }
        public string NumeroProcesso { get; set; }
        public string NomeCaso { get; set; }
        public string? Vara { get; set; }
        public string Comarca { get; set; }
        public string? Observacao { get; set; }
        public string UltimoAndamento { get; set; }
        public string UltimaAtualizacao { get; set; }
        public ReadEstadoDto Estado { get; set; }
        public ReadTribunalDto Tribunal { get; set; }
    }
}
