namespace DatajudClient.Domain.Models.Endereco
{
    public class Estado : ModelBase
    {
        public string Nome { get; set; }
        public string UF { get; set; }
        public string CodigoIbge { get; set; }

        public Estado()
        {
            Nome = string.Empty;
            UF = string.Empty;
            CodigoIbge = string.Empty;
        }
    }
}
