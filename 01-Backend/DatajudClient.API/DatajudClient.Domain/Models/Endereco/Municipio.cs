namespace DatajudClient.Domain.Models.Endereco
{
    public class Municipio : ModelBase
    {
        public string Nome { get; set; }
        public string CodigoIbge { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; }

        public Municipio()
        {
            Nome = string.Empty;
            CodigoIbge = string.Empty;
            Estado = new Estado();
        }
    }
}
