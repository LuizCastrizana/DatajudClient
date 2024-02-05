namespace DatajudClient.Domain.Models.Endereco
{
    public class Endereco : ModelBase
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public int MunicipioId { get; set; }
        public virtual Municipio Municipio { get; set; }

        public Endereco()
        {
            Logradouro = string.Empty;
            Numero = string.Empty;
            Complemento = string.Empty;
            Bairro = string.Empty;
            CEP = string.Empty;
            Municipio = new Municipio();
        }
    }
}
