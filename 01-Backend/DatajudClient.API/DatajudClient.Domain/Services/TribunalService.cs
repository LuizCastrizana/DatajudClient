using AutoMapper;
using DatajudClient.Domain.DTO.Processos;
using DatajudClient.Domain.DTO.Shared;
using DatajudClient.Domain.DTO.Tribunais;
using DatajudClient.Domain.Enum;
using DatajudClient.Domain.Interfaces.Repositories;
using DatajudClient.Domain.Interfaces.Services;

namespace DatajudClient.Domain.Services
{
    public class TribunalService : ITribunalService
    {
        private readonly ITribunalRepository _tribunalRepository;
        private readonly IMapper _mapper;

        public TribunalService(ITribunalRepository tribunalRepository, IMapper mapper)
        {
            _tribunalRepository = tribunalRepository;
            _mapper = mapper;
        }

        public async Task<RetornoServico<List<ReadTribunalDTO>>> ObterTribunaisAsync(string busca = "")
        {
            var retorno = new RetornoServico<List<ReadTribunalDTO>>();
            try
            {
                busca = busca.ToUpper();

                var tribunais = await _tribunalRepository.ObterAsync(x =>
                x.Nome.ToUpper().Contains(busca)
                ||
                x.Sigla.ToUpper().Contains(busca)
                ||
                x.Numero.ToUpper().Contains(busca));

                retorno.Dados = _mapper.Map<IEnumerable<ReadTribunalDTO>>(tribunais).ToList();
                retorno.Status = StatusRetornoEnum.SUCESSO;
                retorno.Mensagem = "Tribunais obtidos com sucesso.";
            }
            catch (Exception ex)
            {
                retorno.Erros = new List<string>() { ex.Message };
                retorno.Status = StatusRetornoEnum.ERRO;
                retorno.Mensagem = "Erro ao obter tribunais.";
            }
            return retorno;
        }

        public async Task<RetornoServico<ReadTribunalDTO>> ObterTribunalPorIdAsync(int id)
        {
            var retorno = new RetornoServico<ReadTribunalDTO>();

            try
            {
                var tribunal = await _tribunalRepository.ObterAsync(x => x.Id == id);
                retorno.Dados = _mapper.Map<ReadTribunalDTO>(tribunal);
                retorno.Status = StatusRetornoEnum.SUCESSO;
                retorno.Mensagem = "Tribunal obtido com sucesso.";
            }
            catch (Exception ex)
            {
                retorno.Erros = new List<string>() { ex.Message };
                retorno.Status = StatusRetornoEnum.ERRO;
                retorno.Mensagem = "Erro ao obter tribunal.";
            }

            return retorno;
        }
    }
}
