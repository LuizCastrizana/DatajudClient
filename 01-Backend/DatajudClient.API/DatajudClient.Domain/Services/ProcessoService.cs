using AutoMapper;
using DatajudClient.Domain.DTO.Datajud;
using DatajudClient.Domain.DTO.Processos;
using DatajudClient.Domain.DTO.Shared;
using DatajudClient.Domain.Enum;
using DatajudClient.Domain.Interfaces.Repositories;
using DatajudClient.Domain.Interfaces.Services;
using DatajudClient.Domain.Models.Processos;
using DatajudClient.Domain.Models.Tribunais;

namespace DatajudClient.Domain.Services
{
    public class ProcessoService : IProcessoService
    {
        private readonly IProcessoRepository _processoRepository;
        private readonly ITribunalRepository _tribunalRepository;
        private readonly IDatajudService _datajudService;
        private readonly IMapper _mapper;

        public ProcessoService(IProcessoRepository processoRepository,ITribunalRepository tribunalRepository, IDatajudService datajudService, IMapper mapper)
        {
            _processoRepository = processoRepository;
            _tribunalRepository = tribunalRepository;
            _datajudService = datajudService;
            _mapper = mapper;
        }

        public RetornoServico<List<ReadProcessoDTO>> IncluirProcessos(IEnumerable<CreateProcessoDTO> dtos)
        {
            var retorno = new RetornoServico<List<ReadProcessoDTO>>();

            try
            {
                var processos = _mapper.Map<IEnumerable<Processo>>(dtos);
                var listProcessoErro = new List<Processo>();

                Parallel.ForEach(processos, (processo) =>
                {
                    if (_processoRepository.Adicionar(processo) == 0)
                        listProcessoErro.Add(processo);
                });

                var processosSucesso = processos.Except(listProcessoErro);

                var listProcessosErroDatajud = new List<Processo>();

                Parallel.ForEach(processosSucesso, (processo) =>
                {
                    try
                    {
                        var retornoDatajud = _datajudService.ObterDadosProcesso(processo);

                        if (retornoDatajud != null)
                        {
                            ProcessarRetornoDatajud(retornoDatajud, processo);
                        }
                        else
                        {
                            listProcessosErroDatajud.Add(processo);
                        }
                    }
                    catch (Exception)
                    {
                        listProcessosErroDatajud.Add(processo);
                    }
                });

                if (listProcessoErro.Count == 0 && listProcessosErroDatajud.Count == 0)
                {
                    retorno.Dados = _mapper.Map<IEnumerable<ReadProcessoDTO>>(processos).ToList();
                    retorno.Status = StatusRetornoEnum.SUCESSO;
                    retorno.Mensagem = "Processos incluídos com sucesso";

                    
                }
                else
                {
                    retorno.Erros = new List<string>();
                    if (listProcessoErro.Count > 0)
                        retorno.Erros.AddRange(listProcessoErro.Select(x => x.NumeroProcesso).ToList());
                    if (listProcessosErroDatajud.Count > 0)
                        retorno.Erros.AddRange(listProcessosErroDatajud.Select(x => x.NumeroProcesso).ToList());
                    retorno.Status = StatusRetornoEnum.ERRO;
                    retorno.Mensagem = "Alguns processos não foram incluídos corretamente.";
                }
            }
            catch (Exception ex)
            {
                retorno.Erros = new List<string>() { ex.Message };
                retorno.Status = StatusRetornoEnum.ERRO;
                retorno.Mensagem = "Erro ao incluir processos.";
            }

            return retorno;
        }

        public RetornoServico<List<ReadProcessoDTO>> AtualizarProcessos(UpdateProcessoDTO dto)
        {
            var retorno = new RetornoServico<List<ReadProcessoDTO>>();
            try
            {
                Parallel.ForEach(dto.numeros, (numero) =>
                {
                    var listProcessoErro = new List<string>();

                    var processo = _processoRepository.Obter(x => x.NumeroProcesso == numero).FirstOrDefault();

                    if (processo != null)
                    {
                        var retornoDatajud = _datajudService.ObterDadosProcesso(processo);
                        if (retornoDatajud != null)
                        {
                            ProcessarRetornoDatajud(retornoDatajud, processo);
                        }
                        else
                        {
                            listProcessoErro.Add(processo.NumeroProcesso);
                        }
                    }
                    else
                    {
                        listProcessoErro.Add(numero);
                    }

                    if (listProcessoErro.Count > 0)
                    {
                        retorno.Erros = listProcessoErro;
                        retorno.Status = StatusRetornoEnum.ERRO;
                        retorno.Mensagem = "Alguns processos não foram atualizados corretamente.";
                    }
                    else
                    {
                        retorno.Dados = _mapper.Map<IEnumerable<ReadProcessoDTO>>(_processoRepository.Obter(x => dto.numeros.Contains(x.NumeroProcesso))).ToList();
                        retorno.Status = StatusRetornoEnum.SUCESSO;
                        retorno.Mensagem = "Processos atualizados com sucesso";
                    }
                });
            }
            catch (Exception ex)
            {
                retorno.Erros = new List<string>() { ex.Message };
                retorno.Status = StatusRetornoEnum.ERRO;
                retorno.Mensagem = "Erro ao atualizar processos.";
            }
            return retorno;
        }

        #region Métodos Privados

        private void ProcessarRetornoDatajud(ResponseDatajudDTO respostaDatajud, Processo processo)
        {
            MapearAndamentosDoProcesso(respostaDatajud, processo);
            _processoRepository.SalvarAlteracoes();
        }

        private void MapearAndamentosDoProcesso(ResponseDatajudDTO respostaDatajud, Processo processo)
        {
            var datajudAndamentos = respostaDatajud.hits.hits.Select(x => x._source.movimentos).FirstOrDefault();

            Parallel.ForEach(datajudAndamentos, (andamento) =>
            {
                var andamentoExistente = processo.Andamentos != null && processo.Andamentos.Select(x => x.DataHora).Contains(andamento.dataHora);

                if (!andamentoExistente)
                {
                    var andamentoProcesso = new AndamentoProcesso
                    {
                        Codigo = andamento.codigo,
                        Descricao = andamento.nome,
                        DataHora = andamento.dataHora,
                    };
                    Parallel.ForEach(andamento.complementosTabelados, (complemento) =>
                    {
                        andamentoProcesso.Complementos.Add(new ComplementoAndamento
                        {
                            Codigo = complemento.codigo,
                            Valor = complemento.valor,
                            Nome = complemento.nome,
                            Descricao = complemento.descricao,
                        });
                    });
                    if (processo.Andamentos == null)
                        processo.Andamentos = new List<AndamentoProcesso>();
                    processo.Andamentos.Add(andamentoProcesso);
                }
            });
        }

        #endregion
    }
}
