using DatajudClient.Domain.Models.Tribunais;

namespace DatajudClient.Domain.Models.Endereco
{
    public class Estado : ModelBase
    {
        public string Nome { get; set; }
        public string UF { get; set; }
        public string CodigoIbge { get; set; }
        public virtual List<Municipio>? Municipios { get; set; }
        public virtual List<Tribunal>? Tribunais { get; set; }

        public Estado()
        {
            Nome = string.Empty;
            UF = string.Empty;
            CodigoIbge = string.Empty;
        }
    }
}
