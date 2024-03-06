using DatajudClient.Domain.DTO.Datajud;
using DatajudClient.Domain.DTO.Shared;
using DatajudClient.Domain.Enum;
using DatajudClient.Domain.Interfaces.Services;
using DatajudClient.Domain.Models.Processos;
using DatajudClient.HttpClient;
using Newtonsoft.Json;
using System.Net;

namespace DatajudClient.Domain.Services
{
    public class DatajudService : IDatajudService
    {
        private readonly IHttpClient _httpClient;

        public DatajudService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<RetornoServico<ResponseDatajudDTO>> ObterDadosProcessoAsync(Processo processo)
        {
            var resposta = new RetornoServico<ResponseDatajudDTO>();

            try
            {
                var request = new RequestDatajudDTO
                {
                    query = new Query
                    {
                        match = new Match
                        {
                            numeroProcesso = processo.NumeroProcesso
                        }
                    }
                };

                var uri = ObterUri(processo.Tribunal.EndpointConsultaNumero);
                
                var retornoDatajud = await _httpClient.ExecutarRequisicaoAsync(uri, HttpMethod.Get, request);

                if (retornoDatajud.StatusCode != HttpStatusCode.OK)
                {
                    resposta.Status = StatusRetornoEnum.ERRO;
                    resposta.Erros = new List<string>() { retornoDatajud.Content };
                }
                else
                {
                    resposta.Status = StatusRetornoEnum.SUCESSO;
                    resposta.Dados = JsonConvert.DeserializeObject<ResponseDatajudDTO>(retornoDatajud.Content);
                    resposta.Mensagem = "Dados do processo obtidos com sucesso.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter dados do processo no Datajud: " + ex.Message);
            }

            return resposta;
        }

        #region Métodos Privados

        private string ObterUri(string endpoint)
        {
            if (endpoint.EndsWith('/'))
                endpoint = endpoint.Substring(0, endpoint.Length - 1);
            
            return endpoint;
        }

        #endregion
    }
}
