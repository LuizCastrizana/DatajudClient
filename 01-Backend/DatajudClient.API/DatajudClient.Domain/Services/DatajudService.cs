using DatajudClient.Domain.DTO.Datajud;
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
        public async Task<ResponseDatajudDTO> ObterDadosProcessoAsync(Processo processo)
        {
            var resposta = new ResponseDatajudDTO();

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
                var retornoDatajud = await _httpClient.ExecutarRequisicaoAsync(processo.Tribunal.EndpointConsultaNumero, HttpMethod.Post, request);
                if (retornoDatajud.StatusCode != HttpStatusCode.OK)
                    throw new Exception("Erro ao obter dados do processo no Datajud");
                resposta = JsonConvert.DeserializeObject<ResponseDatajudDTO>(retornoDatajud.Content);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter dados do processo no Datajud", ex);
            }

            return resposta ?? new ResponseDatajudDTO();
        }
    }
}
