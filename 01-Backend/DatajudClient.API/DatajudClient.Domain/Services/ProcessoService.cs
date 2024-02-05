using AutoMapper;
using DatajudClient.Domain.DTO.Datajud;
using DatajudClient.Domain.DTO.Processos;
using DatajudClient.Domain.DTO.Shared;
using DatajudClient.Domain.Enum;
using DatajudClient.Domain.Interfaces.Repositories;
using DatajudClient.Domain.Interfaces.Services;
using DatajudClient.Domain.Models.Processos;
using DatajudClient.Domain.Models.Tribunais;
using System.Collections.Concurrent;

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

        public async Task<RetornoServico<List<ReadProcessoDTO>>> IncluirProcessosAsync(IEnumerable<CreateProcessoDTO> dtos)
        {
            var retorno = new RetornoServico<List<ReadProcessoDTO>>();

            try
            {
                var processos = new ConcurrentBag<Processo>(_mapper.Map<IEnumerable<Processo>>(dtos));
                var listProcessoErro = new ConcurrentBag<Processo>();

                await Parallel.ForEachAsync(processos, async (processo, ct) =>
                {
                    if (await _processoRepository.AdicionarAsync(processo) == 0)
                        listProcessoErro.Add(processo);
                });

                var processosSucesso = new ConcurrentBag<Processo>(processos.Except(listProcessoErro));

                var listProcessosErroDatajud = new List<Processo>();

                await Parallel.ForEachAsync(processosSucesso, async (processo, ct) =>
                {
                    try
                    {
                        var retornoDatajud = await _datajudService.ObterDadosProcessoAsync(processo);

                        if (retornoDatajud != null)
                        {
                            await ProcessarRetornoDatajud(retornoDatajud, processo);
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

        public async Task<RetornoServico<List<ReadProcessoDTO>>> AtualizarProcessosAsync(UpdateProcessoDTO dto)
        {
            var retorno = new RetornoServico<List<ReadProcessoDTO>>();
            try
            {
                var listProcessoErro = new ConcurrentBag<string>();

                await Parallel.ForEachAsync(dto.numeros, async (numero, ct) =>
                {
                    var processo = _processoRepository.Obter(x => x.NumeroProcesso == numero).FirstOrDefault();

                    if (processo != null)
                    {
                        var retornoDatajud = await _datajudService.ObterDadosProcessoAsync(processo);
                        if (retornoDatajud != null)
                        {
                            await ProcessarRetornoDatajud(retornoDatajud, processo);
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
                });

                if (listProcessoErro.Count > 0)
                {
                    retorno.Erros = listProcessoErro.ToList();
                    retorno.Status = StatusRetornoEnum.ERRO;
                    retorno.Mensagem = "Alguns processos não foram atualizados corretamente.";
                }
                else
                {
                    retorno.Dados = _mapper.Map<IEnumerable<ReadProcessoDTO>>(_processoRepository.Obter(x => dto.numeros.Contains(x.NumeroProcesso))).ToList();
                    retorno.Status = StatusRetornoEnum.SUCESSO;
                    retorno.Mensagem = "Processos atualizados com sucesso";
                }
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

        private async Task ProcessarRetornoDatajud(ResponseDatajudDTO respostaDatajud, Processo processo)
        {
            await MapearAndamentosDoProcesso(respostaDatajud, processo);
            await _processoRepository.SalvarAlteracoesAsync();
        }

        private async Task MapearAndamentosDoProcesso(ResponseDatajudDTO respostaDatajud, Processo processo)
        {
            var datajudAndamentos = new ConcurrentBag<Movimento>(respostaDatajud.hits.hits.Select(x => x._source.movimentos).First());

            await Parallel.ForEachAsync(datajudAndamentos, async (andamento, ct) =>
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
                    await Parallel.ForEachAsync(andamento.complementosTabelados, async (complemento, ct) =>
                    {
                        if (andamentoProcesso.Complementos == null)
                            andamentoProcesso.Complementos = new List<ComplementoAndamento>();
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
