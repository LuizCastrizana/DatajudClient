namespace DatajudClient.Domain.Models.Processos
{
    public class AndamentoProcesso : ModelBase
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHora { get; set; }
        public int ProcessoId { get; set; }
        public virtual Processo Processo { get; set; }
        public virtual List<ComplementoAndamento>? Complementos { get; set; }

        public AndamentoProcesso()
        {
            Codigo = 0;
            Descricao = string.Empty;
            DataHora = DateTime.Now;
            ProcessoId = 0;
            Processo = new Processo();
            Complementos = new List<ComplementoAndamento>();
        }
    }
}
