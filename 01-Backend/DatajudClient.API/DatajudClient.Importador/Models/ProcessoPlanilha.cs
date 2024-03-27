namespace DatajudClient.Importador.Models
{
    public class ProcessoPlanilha
    {
        public string Codigo { get; set; }
        public string Situacao { get; set; }
        public string Categoria { get; set; }
        public string TipoDeAcao { get; set; }
        public string Cliente { get; set; }
        public string CondicaoDoCliente { get; set; }
        public string Adverso { get; set; }
        public string CondicaoDoAdverso { get; set; }
        public string NumeroDoProcesso { get; set; }
        public DateTime DataDistribuicao { get; set; }
        public string Juizo { get; set; }
        public string Comarca { get; set; }
        public DateTime DataDoAndamento { get; set; }
        public string Andamento { get; set; }
        public string ObservacoesDoAndamento { get; set; }
        public DateTime CadastradoEm { get; set; }
        public string NomeDoCaso { get; set; }
    }
}
