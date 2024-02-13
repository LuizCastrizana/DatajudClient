namespace DatajudClient.Domain.DTO.Processos
{
    public class ReadAndamentoDTO
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHora { get; set; }
        public virtual List<ReadComplementoAndamentoDTO>? Complementos { get; set; }
    }
}
