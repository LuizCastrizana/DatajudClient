namespace DatajudClient.Domain.Models.Processos
{
    public class ComplementoAndamento : ModelBase
    {
        public int Codigo { get; set; }
        public int Valor { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int AndamentoProcessoId { get; set; }
        public virtual AndamentoProcesso AndamentoProcesso { get; set; }

        public ComplementoAndamento()
        {
            Codigo = 0;
            Valor = 0;
            Nome = string.Empty;
            Descricao = string.Empty;
            AndamentoProcessoId = 0;
            AndamentoProcesso = new AndamentoProcesso();
        }
    }
}
