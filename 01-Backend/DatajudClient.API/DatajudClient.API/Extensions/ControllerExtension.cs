using DatajudClient.Domain.DTO.Shared;
using DatajudClient.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DatajudClient.API.Extensions
{
    public static class ControllerExtension
    {
        /// <summary>
        /// Trata a resposta de um serviço e gera o retorno da API.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controllerBase"></param>
        /// <param name="retornoServico"></param>
        /// <returns>Resposta da API com os dados retornados do serviço.</returns>
        public static IActionResult TratarRespostaServico<T>(this ControllerBase controllerBase, RetornoServico<T> retornoServico) where T : class
        {
            var respostaApi = new RetornoApi<T>();
            respostaApi.Dados = retornoServico.Dados;
            respostaApi.Mensagem = retornoServico.Mensagem;
            respostaApi.Erros = retornoServico.Erros;
            switch (retornoServico.Status)
            {
                case StatusRetornoEnum.SUCESSO:
                    return controllerBase.Ok(JsonConvert.SerializeObject(respostaApi));
                case StatusRetornoEnum.ERRO_VALIDACAO:
                    return controllerBase.BadRequest(JsonConvert.SerializeObject(respostaApi));
                case StatusRetornoEnum.ERRO:
                    return controllerBase.StatusCode(StatusCodes.Status500InternalServerError, JsonConvert.SerializeObject(respostaApi));
                case StatusRetornoEnum.NAO_ENCONTRADO:
                    return controllerBase.NotFound(JsonConvert.SerializeObject(respostaApi));
                default:
                    return controllerBase.NoContent();
            }
        }
    }
}
