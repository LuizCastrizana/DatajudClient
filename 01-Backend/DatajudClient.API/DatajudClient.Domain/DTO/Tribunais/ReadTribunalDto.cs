using DatajudClient.Domain.DTO.Enderecos;

namespace DatajudClient.Domain.DTO.Tribunais
{
    public class ReadTribunalDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Numero { get; set; }
        public string EndpointConsultaNumero { get; set; }
        public ReadCategoriaTribunalDTO Categoria { get; set; }
        public ReadEstadoDTO Estado { get; set; }


    }
}
