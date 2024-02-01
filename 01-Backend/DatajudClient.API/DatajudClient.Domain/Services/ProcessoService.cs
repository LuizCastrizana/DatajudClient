using AutoMapper;
using DatajudClient.Domain.DTO.Processos;
using DatajudClient.Domain.DTO.Shared;
using DatajudClient.Domain.Enum;
using DatajudClient.Domain.Interfaces.Repositories.Processos;
using DatajudClient.Domain.Interfaces.Services.Processos;
using DatajudClient.Domain.Models.Processos;

namespace DatajudClient.Domain.Services
{
    public class ProcessoService : IProcessoService
    {
        private readonly IProcessoRepository _processoRepository;
        private readonly IMapper _mapper;

        public ProcessoService(IProcessoRepository processoRepository, IMapper mapper)
        {
            _processoRepository = processoRepository;
            _mapper = mapper;
        }

        public RetornoServico<List<ReadProcessoDTO>> IncluirProcessos(IEnumerable<CreateProcessoDTO> DTOs)
        {
            var retorno = new RetornoServico<List<ReadProcessoDTO>>();

            try
            {
                var listProcessoErro = new List<CreateProcessoDTO>();

                Parallel.ForEach(DTOs, (processo) =>
                {
                    if (_processoRepository.Adicionar(_mapper.Map<Processo>(processo)) == 0)
                        listProcessoErro.Add(processo);
                });

                if (listProcessoErro.Count > 0)
                {
                    retorno.Erros = listProcessoErro.Select(x => x.NumeroProcesso).ToList();
                    retorno.Status = StatusRetornoEnum.ERRO;
                    retorno.Mensagem = "Alguns processos não foram incluídos.";
                }
                
                retorno.Dados = _mapper.Map<IEnumerable<ReadProcessoDTO>>(DTOs).ToList();
                retorno.Status = StatusRetornoEnum.SUCESSO;
                retorno.Mensagem = "Processos incluídos com sucesso";
            }
            catch (Exception ex)
            {
                retorno.Erros = new List<string>() { ex.Message };
                retorno.Status = StatusRetornoEnum.ERRO;
                retorno.Mensagem = "Erro ao incluir processos.";
            }

            return retorno;
        }
    }
}
