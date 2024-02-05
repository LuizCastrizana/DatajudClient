using DatajudClient.Domain.Models.Endereco;

namespace DatajudClient.Domain.Models.Tribunais
{
    public class Tribunal : ModelBase
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Numero { get; set; }
        public string EndpointConsultaNumero { get; set; }
        public int CategoriaId { get; set; }
        public virtual CategoriaTribunal Categoria { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; }

        public Tribunal()
        {
            Nome = string.Empty;
            Sigla = string.Empty;
            EndpointConsultaNumero = string.Empty;
            Categoria = new CategoriaTribunal();
            Estado = new Estado();
        }
    }
}
