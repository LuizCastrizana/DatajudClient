using DatajudClient.Domain.DTO.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DatajudClient.API.Extensions
{
    public static class Configuration
    {
        public static IServiceCollection ConfigureModelStateResponse(this IServiceCollection services)
        {
            services.AddMvcCore().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = (errorContext) =>
                {
                    var errors = new List<string>();

                    foreach (var key in errorContext.ModelState.Keys)
                    {
                        if (!string.IsNullOrEmpty(key))
                            errors.Add(key + ": " + string.Join(',', errorContext.ModelState[key].Errors.Select(x => x.ErrorMessage)));
                    }

                    var result = new RetornoApi<string>
                    {
                        Dados = null,
                        Mensagem = null,
                        Erros = errors
                    };

                    return new BadRequestObjectResult(result);
                };
            });

            return services;
        }
    }
}
