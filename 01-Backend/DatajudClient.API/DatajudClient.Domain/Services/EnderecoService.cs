using AutoMapper;
using DatajudClient.Domain.DTO.Enderecos;
using DatajudClient.Domain.DTO.Shared;
using DatajudClient.Domain.Enum;
using DatajudClient.Domain.Interfaces.Repositories;
using DatajudClient.Domain.Interfaces.Services;
using DatajudClient.Domain.Models.Endereco;

namespace DatajudClient.Domain.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IRepository<Estado> _estadoRepository;
        private readonly IMapper _mapper;

        public EnderecoService(IRepository<Estado> estadoRepository, IMapper mapper)
        {
            _estadoRepository = estadoRepository;
            _mapper = mapper;
        }
        public async Task<RetornoServico<List<ReadEstadoDTO>>> ObterEstadosAsync(string busca = "")
        {
            var retorno = new RetornoServico<List<ReadEstadoDTO>>();

            try
            {
                busca = busca.ToUpper();

                var estados = await _estadoRepository.ObterAsync(x =>
                x.Nome.ToUpper().Contains(busca)
                ||
                x.UF.ToUpper().Contains(busca));

                retorno.Dados = _mapper.Map<IEnumerable<ReadEstadoDTO>>(estados).OrderBy(x=>x.Nome).ToList();
                retorno.Status = StatusRetornoEnum.SUCESSO;
                retorno.Mensagem = "Estados obtidos com sucesso.";
            }
            catch (Exception ex)
            {
                retorno.Erros = new List<string>() { ex.Message };
                retorno.Status = StatusRetornoEnum.ERRO;
                retorno.Mensagem = "Erro ao obter estados.";
            }

            return retorno;
        }
    }
}
