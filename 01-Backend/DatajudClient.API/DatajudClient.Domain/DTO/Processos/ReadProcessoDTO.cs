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
        public DateTime UltimoAndamento { get; set; }
        public string UltimoAndamentoDescricao { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public ReadEstadoDTO Estado { get; set; }
        public ReadTribunalDTO Tribunal { get; set; }
        public List<ReadAndamentoDTO> Andamentos { get; set; }
    }
}
