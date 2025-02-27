﻿using AutoMapper;
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
                var processos = new List<Processo>(_mapper.Map<IEnumerable<Processo>>(dtos));
                var listProcessoErro = new List<(Processo processo, string mensagem)>();

                foreach(var processo in processos) 
                {
                    var processoExistente = _processoRepository.Obter(x => x.NumeroProcesso == processo.NumeroProcesso).FirstOrDefault() != null;

                    if (processoExistente)
                    {
                        listProcessoErro.Add((processo, "Processo já cadastrado."));
                        continue;
                    }

                    if (await _processoRepository.AdicionarAsync(processo) == 0)
                        listProcessoErro.Add((processo, "Nenhum registro alterado no banco de dados."));
                }

                var numerosProcessosSucesso = processos.Except(listProcessoErro.Select(x => x.processo)).Select(x => x.NumeroProcesso).ToList();
                var respostaAtualizarProcessos = await AtualizarProcessosAsync(new UpdateProcessoDTO() { Numeros = numerosProcessosSucesso });

                if (listProcessoErro.Count == 0 && respostaAtualizarProcessos.Status == StatusRetornoEnum.SUCESSO)
                {
                    retorno.Dados = _mapper.Map<IEnumerable<ReadProcessoDTO>>(processos).ToList();
                    retorno.Status = StatusRetornoEnum.SUCESSO;
                    retorno.Mensagem = "Processo(s) incluído(s) e atualizado(s) com sucesso.";                    
                }
                else
                {
                    retorno.Erros = new List<string>();

                    if (listProcessoErro.Count > 0)
                        retorno.Erros.AddRange(listProcessoErro.Select(x => x.processo.NumeroProcesso + ": " + x.mensagem).ToList());

                    if (respostaAtualizarProcessos.Erros != null && respostaAtualizarProcessos.Erros.Count() > 0)
                        retorno.Erros.AddRange(respostaAtualizarProcessos.Erros);

                    retorno.Status = StatusRetornoEnum.ERRO;
                    retorno.Mensagem = "Não foi possível incluir ou obter andamentos de todos os processos informados.";
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

                foreach (var numero in dto.Numeros)
                {
                    var processo = _processoRepository.Obter(x => x.NumeroProcesso == ApenasNumeros(numero)).FirstOrDefault();

                    if (processo == null)
                    {
                        listProcessoErro.Add((numero, "Processo não encontrado."));
                        continue;
                    }

                    var retornoDatajud = await _datajudService.ObterDadosProcessoAsync(processo);

                    if (retornoDatajud == null || retornoDatajud.Dados == null || retornoDatajud.Status != StatusRetornoEnum.SUCESSO)
                    {
                        var msg = retornoDatajud != null && retornoDatajud.Erros != null ? string.Join(", ", retornoDatajud.Erros) : "Erro ao obter dados do Datajud.";
                        listProcessoErro.Add((processo.NumeroProcesso, msg));
                        continue;
                    }

                    if (await ProcessarRetornoDatajud(retornoDatajud.Dados, processo) == false)
                        listProcessoErro.Add((processo.NumeroProcesso, "Erro ao processar retorno do Datajud."));
                }

                if (listProcessoErro.Count() > 0)
                {
                    retorno.Erros = listProcessoErro.Select(x => { return string.Concat(x.processo, ": ", x.msg); }).ToList();
                    retorno.Status = StatusRetornoEnum.ERRO;
                    retorno.Mensagem = "Não foi possível obter os andamentos de todos os processos informados.";
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
                var buscaNumero = ApenasNumeros(busca);

                var processos = await _processoRepository.ObterAsync(x => 
                (buscaNumero.Length > 0 && x.NumeroProcesso.Contains(buscaNumero))
                ||
                x.NomeCaso.ToUpper().Contains(busca) 
                ||
                x.Tribunal.Nome.ToUpper().Contains(busca) 
                ||
                x.Tribunal.Sigla.ToUpper().Contains(busca)
                ||
                x.Andamentos.Select(x => x.Descricao.ToUpper()).Any(x => x.Contains(busca)));

                retorno.Dados = _mapper.Map<IEnumerable<ReadProcessoDTO>>(processos).ToList();

                PreencherUltimoAndamentoEComplementos(retorno.Dados);

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

        public async Task<RetornoServico<ReadProcessoDTO>> ObterProcessosPorIdAsync(int id)
        {
            var retorno = new RetornoServico<ReadProcessoDTO>();
            try
            {
                var processos = (await _processoRepository.ObterAsync(x => x.Id == id)).FirstOrDefault();

                retorno.Dados = _mapper.Map<ReadProcessoDTO>(processos);

                PreencherUltimoAndamentoEComplementos(new List<ReadProcessoDTO>() { retorno.Dados });

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

        private async Task<bool> ProcessarRetornoDatajud(ResponseDatajudDTO respostaDatajud, Processo processo)
        {
            var retorno = true;

            try
            {
                if (respostaDatajud.hits.hits.Count() > 0)
                {
                    await MapearAndamentosDoProcesso(respostaDatajud, processo);
                    processo.UltimoAndamento = processo.Andamentos != null ? processo.Andamentos.OrderByDescending(x => x.DataHora).FirstOrDefault().DataHora : DateTime.MinValue;
                }

                processo.UltimaAtualizacao = DateTime.Now;

                if (await _processoRepository.SalvarAlteracoesAsync() == 0)
                    retorno = false;
            }
            catch (Exception)
            {
                retorno = false;
            }

            return retorno;
        }

        private async Task MapearAndamentosDoProcesso(ResponseDatajudDTO respostaDatajud, Processo processo)
        {
            var processoAndamentos = new ConcurrentBag<AndamentoProcesso>(processo.Andamentos != null ? processo.Andamentos : new List<AndamentoProcesso>());
            var andamentosIncluir = new ConcurrentBag<AndamentoProcesso>();
            
            var listMovimento = new List<Movimento>();
            respostaDatajud.hits.hits.ForEach(x => listMovimento.AddRange(x._source.movimentos));
            var datajudAndamentos = new ConcurrentBag<Movimento>(listMovimento);

            await Parallel.ForEachAsync(datajudAndamentos, async (andamento, ct) =>
            {
                var andamentoExistente = processoAndamentos.Count() > 0 && processoAndamentos.Select(x => x.DataHora.ToString()).Contains(andamento.dataHora.ToString());
                var complementosIncluir = new ConcurrentBag<ComplementoAndamento>();

                if (!andamentoExistente)
                {
                    var andamentoProcesso = new AndamentoProcesso()
                    {
                        Codigo = Convert.ToInt32(andamento.codigo),
                        Descricao = andamento.nome,
                        DataHora = andamento.dataHora,
                    };

                    if (andamento.complementosTabelados != null)
                    {
                        var dataJudComplementos = new ConcurrentBag<ComplementoTabelado>(andamento.complementosTabelados);

                        if (dataJudComplementos != null)
                        {
                            await Parallel.ForEachAsync(dataJudComplementos, async (complemento, ct) =>
                            {
                                var complementoAndamento = new ComplementoAndamento()
                                {
                                    Codigo = Convert.ToInt32(andamento.codigo),
                                    Valor = Convert.ToInt32(complemento.valor),
                                    Nome = complemento.nome,
                                    Descricao = complemento.descricao,
                                };

                                complementosIncluir.Add(complementoAndamento);
                            });

                            andamentoProcesso.Complementos = complementosIncluir.ToList();
                        }
                    }

                    andamentosIncluir.Add(andamentoProcesso);
                }
            });

            if (processo.Andamentos == null)
                processo.Andamentos = new List<AndamentoProcesso>();

            processo.Andamentos.AddRange(andamentosIncluir);
        }

        private void PreencherUltimoAndamentoEComplementos(List<ReadProcessoDTO> processos)
        {
            // TODO: Refatorar logica a baixo para incluir o campo UltimoAndamentoDescricao na model Processo e no banco de dados
            // e assim preencher o valor no momento de cadastrar o processo.
            processos.ForEach(x =>
            {
                var ultimoAndamento = x.Andamentos.OrderByDescending(x => x.DataHora).FirstOrDefault();
                x.UltimoAndamentoDescricao = ultimoAndamento != null ? ultimoAndamento.Descricao : string.Empty;

                if (x.Andamentos != null && x.Andamentos.Count() > 0)
                {
                    //var complemento = ultimoAndamento.Complementos != null && ultimoAndamento.Complementos.Count() > 0 ? ultimoAndamento.Complementos.FirstOrDefault() : null;
                    var complemento = string.Empty;

                    if (ultimoAndamento != null)
                    {
                        if (ultimoAndamento.Complementos != null && ultimoAndamento.Complementos.Count() > 0)
                            complemento = string.Join("| ", ultimoAndamento.Complementos.Select(x => string.Concat(x.Descricao, ": ", x.Nome)));

                        x.UltimoAndamentoDescricao = ultimoAndamento.Descricao + (string.IsNullOrWhiteSpace(complemento) ? string.Empty : string.Concat(" - ", complemento));
                    }

                    PreencherComplementosDescricao(x.Andamentos);
                }
            });
        }

        private void PreencherComplementosDescricao(List<ReadAndamentoDTO> andamentos)
        {
            andamentos.ForEach(x =>
            {
                if (x.Complementos != null && x.Complementos.Count() > 0)
                {
                    x.ComplementosDescricao = string.Join("| ", x.Complementos.Select(x => string.Concat(x.Descricao, ": ", x.Nome)));
                }
            });
        }

        private string ApenasNumeros(string texto)
        {
            return new string(texto.Where(char.IsDigit).ToArray());
        }

        #endregion
    }
}
