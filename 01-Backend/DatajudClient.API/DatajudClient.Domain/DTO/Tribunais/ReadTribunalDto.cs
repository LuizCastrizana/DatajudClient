using DatajudClient.Domain.DTO.Enderecos;

namespace DatajudClient.Domain.DTO.Tribunais
{
    public class ReadTribunalDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Numero { get; set; }
        public string EndpointConsultaNumero { get; set; }
        public ReadCategoriaTribunalDto Categoria { get; set; }
        public ReadEstadoDto Estado { get; set; }


    }
}
