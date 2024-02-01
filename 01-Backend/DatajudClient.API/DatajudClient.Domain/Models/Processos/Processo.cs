namespace DatajudClient.Domain.Models.Processos
{
    public class Processo : ModelBase
    {
        public string NumeroProcesso { get; set; }
        public string NomeCaso{ get; set; }
        public string Vara { get; set; }
        public string Comarca { get; set; }
        public string Estado { get; set; }
        public string Observacao { get; set; }
        public virtual List<AndamentoProcesso>? Andamentos { get; set; }

        public Processo()
        {
            NumeroProcesso = string.Empty;
            NomeCaso = string.Empty;
            Vara = string.Empty;
            Comarca = string.Empty;
            Estado = string.Empty;
            Observacao = string.Empty;
            Andamentos = new List<AndamentoProcesso>();
        }

        /// <summary>
        /// Retorna o número do processo formatado. Caso o número do processo não tenha 20 caracteres, retorna o número do processo sem formatação.
        /// </summary>
        /// <returns></returns>
        public string GetNumeroFormatado()
        {
            if(NumeroProcesso.Length == 20)
            {
                return $"{NumeroProcesso.Substring(0, 7)}.{NumeroProcesso.Substring(7, 2)}.{NumeroProcesso.Substring(9, 4)}.{NumeroProcesso.Substring(13, 4)}.{NumeroProcesso.Substring(17, 2)}";
            }
            else
            {
                return NumeroProcesso;
            }
        }
    }
}
