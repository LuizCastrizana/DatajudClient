using AutoMapper;
using DatajudClient.Domain.DTO.Datajud;
using DatajudClient.Domain.DTO.Processos;
using DatajudClient.Domain.DTO.Shared;
using DatajudClient.Domain.Enum;
using DatajudClient.Domain.Interfaces.Repositories;
using DatajudClient.Domain.Interfaces.Services;
using DatajudClient.Domain.Models.Processos;
using System.Collections.Concurrent;

namespace DatajudClient.Domain.Services
{
    public class ProcessoService : IProcessoService
    {
        private readonly IProcessoRepository _processoRepository;
        private readonly IDatajudService _datajudService;
        private readonly IMapper _mapper;

        public ProcessoService(IProcessoRepository processoRepository, IDatajudService datajudService, IMapper mapper)
        {
            _processoRepository = processoRepository;
            _datajudService = datajudService;
            _mapper = mapper;
        }

        public async Task<RetornoServico<List<ReadProcessoDTO>>> IncluirProcessosAsync(IEnumerable<CreateProcessoDTO> dtos)
        {
            var retorno = new RetornoServico<List<ReadProcessoDTO>>();

            try
            {
                var processos = new ConcurrentBag<Processo>(_mapper.Map<IEnumerable<Processo>>(dtos));
                var listProcessoErro = new ConcurrentBag<(Processo processo, string mensagem)>();
                var listProcessosErroDatajud = new ConcurrentBag<(Processo processo, string mensagem)>();

                await Parallel.ForEachAsync(processos, async (processo, ct) =>
                {
                    if (await _processoRepository.AdicionarAsync(processo) == 0)
                        listProcessoErro.Add((processo, "Nenhum registro alterado no banco de dados."));
                });

                var processosSucesso = new ConcurrentBag<Processo>(processos.Except(listProcessoErro.Select(x => x.processo)));

                await Parallel.ForEachAsync(processosSucesso, async (processo, ct) =>
                {
                    try
                    {
                        var retornoDatajud = await _datajudService.ObterDadosProcessoAsync(processo);

                        if (retornoDatajud != null)
                        {
                            if (retornoDatajud.Status == StatusRetornoEnum.SUCESSO)
                                await ProcessarRetornoDatajud(retornoDatajud.Dados, processo);
                            else
                                listProcessoErro.Add((processo, string.Join(", ", retornoDatajud.Erros != null ? retornoDatajud.Erros : string.Empty)));
                        }
                        else
                        {
                            listProcessosErroDatajud.Add((processo, "Erro ao obter dados do Datajud."));
                        }
                    }
                    catch (Exception ex)
                    {
                        listProcessosErroDatajud.Add((processo, ex.Message));
                    }
                });

                if (listProcessoErro.Count == 0 && listProcessosErroDatajud.Count == 0)
                {
                    retorno.Dados = _mapper.Map<IEnumerable<ReadProcessoDTO>>(processos).ToList();
                    retorno.Status = StatusRetornoEnum.SUCESSO;
                    retorno.Mensagem = "Processo(s) incluído(s) com sucesso.";                    
                }
                else
                {
                    retorno.Erros = new List<string>();

                    if (listProcessoErro.Count > 0)
                        retorno.Erros.AddRange(listProcessoErro.Select(x => x.processo.NumeroProcesso + ": " + x.mensagem).ToList());

                    if (listProcessosErroDatajud.Count > 0)
                        retorno.Erros.AddRange(listProcessosErroDatajud.Select(x => x.processo.NumeroProcesso + ": " + x.mensagem).ToList());

                    retorno.Status = StatusRetornoEnum.ERRO;
                    retorno.Mensagem = "Erro ao incluir ou obter atualizações do(s) processo(s).";
                }
            }
            catch (Exception ex)
            {
                retorno.Erros = new List<string>() { ex.Message };
                retorno.Status = StatusRetornoEnum.ERRO;
                retorno.Mensagem = "Erro ao incluir o(s) processo(s).";
            }

            return retorno;
        }

        public async Task<RetornoServico<List<ReadProcessoDTO>>> AtualizarProcessosAsync(UpdateProcessoDTO dto)
        {
            var retorno = new RetornoServico<List<ReadProcessoDTO>>();
            try
            {
                var listProcessoErro = new ConcurrentBag<(string processo, string msg)>(Enumerable.Empty<(string processo, string msg)>());

                await Parallel.ForEachAsync(dto.Numeros, async (numero, ct) =>
                {
                    var processo = _processoRepository.Obter(x => x.NumeroProcesso == numero).FirstOrDefault();

                    if (processo != null)
                    {
                        var retornoDatajud = await _datajudService.ObterDadosProcessoAsync(processo);

                        if (retornoDatajud != null)
                        {
                            if (retornoDatajud.Status == StatusRetornoEnum.SUCESSO)
                                await ProcessarRetornoDatajud(retornoDatajud.Dados, processo);
                            else
                                listProcessoErro.Add((processo.NumeroProcesso, String.Join(',', retornoDatajud.Erros)));
                        }
                        else
                        {
                            listProcessoErro.Add((processo.NumeroProcesso, "Não houve retorno do Datajud."));
                        }
                    }
                    else
                    {
                        listProcessoErro.Add((numero, "Processo não encontrado."));
                    }
                });

                if (listProcessoErro.Count > 0)
                {
                    retorno.Erros = listProcessoErro.Select(x => { return string.Concat(x.processo, ": ", x.msg); }).ToList();
                    retorno.Status = StatusRetornoEnum.ERRO;
                    retorno.Mensagem = "Não foi possível atualizar todos os processos informados.";
                }
                else
                {
                    retorno.Dados = _mapper.Map<IEnumerable<ReadProcessoDTO>>(_processoRepository.Obter(x => dto.Numeros.Contains(x.NumeroProcesso))).ToList();
                    retorno.Status = StatusRetornoEnum.SUCESSO;
                    retorno.Mensagem = "Processo(s) atualizado(s) com sucesso.";
                }
            }
            catch (Exception ex)
            {
                retorno.Erros = new List<string>() { ex.Message };
                retorno.Status = StatusRetornoEnum.ERRO;
                retorno.Mensagem = "Erro ao atualizar o(s) processo(s).";
            }
            return retorno;
        }

        public async Task<RetornoServico<ReadProcessoDTO>> AtualizarDadosProcessoAsync(int id, UpdateDadosProcessoDTO dto)
        {
            var retorno = new RetornoServico<ReadProcessoDTO>();

            try
            {
                var processo = _processoRepository.Obter(x => x.Id == id).FirstOrDefault();

                if (processo != null)
                {
                    _mapper.Map(dto, processo);
                    if (await _processoRepository.SalvarAlteracoesAsync() == 0)
                        throw new Exception("Nenhum registro alterado no banco de dados.");

                    retorno.Dados = _mapper.Map<ReadProcessoDTO>(processo);
                    retorno.Status = StatusRetornoEnum.SUCESSO;
                    retorno.Mensagem = "Dados do processo atualizados com sucesso.";
                }
            }
            catch (Exception ex)
            {
                retorno.Erros = new List<string>() { ex.Message };
                retorno.Status = StatusRetornoEnum.ERRO;
                retorno.Mensagem = "Erro ao atualizar dados do processo.";
            }
            return retorno;
        }

        public async Task<RetornoServico<List<ReadProcessoDTO>>> ObterProcessosAsync(string busca = "")
        {
            var retorno = new RetornoServico<List<ReadProcessoDTO>>();
            try
            {
                busca = busca.ToUpper();

                var processos = await _processoRepository.ObterAsync(x => 
                x.NumeroProcesso.ToUpper().Contains(busca) 
                ||
                x.NomeCaso.ToUpper().Contains(busca) 
                ||
                x.Tribunal.Nome.ToUpper().Contains(busca) 
                ||
                x.Tribunal.Sigla.ToUpper().Contains(busca));

                retorno.Dados = _mapper.Map<IEnumerable<ReadProcessoDTO>>(processos).ToList();
                retorno.Status = StatusRetornoEnum.SUCESSO;
                retorno.Mensagem = "Processos obtidos com sucesso.";
            }
            catch (Exception ex)
            {
                retorno.Erros = new List<string>() { ex.Message };
                retorno.Status = StatusRetornoEnum.ERRO;
                retorno.Mensagem = "Erro ao obter processos.";
            }
            return retorno;
        }

        public async Task<RetornoServico<int>> ExcluirProcessos(IEnumerable<int> ids)
        {
            var retorno = new RetornoServico<int>();

            try
            {
                var processosExcluidos = 0;
                var processosErro = new ConcurrentBag<(Processo Processo, string Mensagem)>();

                foreach (var id in ids)
                {
                    var processo = (await _processoRepository.ObterAsync(x => x.Id == id)).FirstOrDefault();

                    if (processo == null)
                    {
                        processosErro.Add((new Processo() { Id = id }, "Processo não encontrado."));
                    }
                    else
                    {
                        if (await _processoRepository.ExcluirAsync(processo, true) == 0)
                            processosErro.Add((processo, "Nenhum registro alterado no banco de dados."));
                    }
                };

                if (processosErro.Count() > 0)
                {
                    retorno.Status = StatusRetornoEnum.ERRO;
                    retorno.Erros = processosErro.Select(x => { return string.Concat(x.Processo.Id, ": ", x.Mensagem); }).ToList();
                    retorno.Mensagem = "Não foi possível excluir todos os processos informados.";
                }
                else
                {
                    retorno.Status = StatusRetornoEnum.SUCESSO;
                    retorno.Mensagem = "Processo(s) excluído(s) com sucesso.";
                }

                retorno.Dados = processosExcluidos;
            }
            catch (Exception ex)
            {
                retorno.Erros = new List<string>() { ex.Message };
                retorno.Status = StatusRetornoEnum.ERRO;
                retorno.Mensagem = "Erro ao excluir o(s) processo(s).";
            }

            return retorno;
        }

        #region Métodos Privados

        private async Task ProcessarRetornoDatajud(ResponseDatajudDTO respostaDatajud, Processo processo)
        {
            await MapearAndamentosDoProcesso(respostaDatajud, processo);
            processo.UltimoAndamento = processo.Andamentos != null ? processo.Andamentos.OrderByDescending(x => x.DataHora).FirstOrDefault().DataHora : DateTime.MinValue;
            processo.UltimaAtualizacao = DateTime.Now;
            await _processoRepository.SalvarAlteracoesAsync();
        }

        private async Task MapearAndamentosDoProcesso(ResponseDatajudDTO respostaDatajud, Processo processo)
        {
            var datajudAndamentos = new ConcurrentBag<Movimento>(respostaDatajud.hits.hits.Select(x => x._source.movimentos).First());
            var andamentosIncluir = new ConcurrentBag<AndamentoProcesso>();
            ConcurrentBag<ComplementoTabelado> dataJudComplementos;

            await Parallel.ForEachAsync(datajudAndamentos, async (andamento, ct) =>
            {
                var andamentoExistente = processo.Andamentos != null && processo.Andamentos.Select(x => x.DataHora).Contains(andamento.dataHora);
                var complementosIncluir = new ConcurrentBag<ComplementoAndamento>();

                if (!andamentoExistente)
                {
                    var andamentoProcesso = new AndamentoProcesso()
                    {
                        Codigo = andamento.codigo,
                        Descricao = andamento.nome,
                        DataHora = andamento.dataHora,
                    };

                    if (andamento.complementosTabelados != null)
                    {
                        dataJudComplementos = new ConcurrentBag<ComplementoTabelado>(andamento.complementosTabelados);

                        if (dataJudComplementos != null)
                        {
                            await Parallel.ForEachAsync(dataJudComplementos, async (complemento, ct) =>
                            {
                                var complementoAndamento = new ComplementoAndamento()
                                {
                                    Codigo = complemento.codigo,
                                    Valor = complemento.valor,
                                    Nome = complemento.nome,
                                    Descricao = complemento.descricao,
                                };

                                complementosIncluir.Add(complementoAndamento);
                            });

                            andamentoProcesso.Complementos = complementosIncluir.ToList();
                        }

                        andamentosIncluir.Add(andamentoProcesso);
                    }
                }
            });

            if (processo.Andamentos == null)
                processo.Andamentos = new List<AndamentoProcesso>();

            processo.Andamentos.AddRange(andamentosIncluir);
        }

        #endregion
    }
}
