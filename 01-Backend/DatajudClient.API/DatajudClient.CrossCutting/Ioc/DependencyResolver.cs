using AutoMapper;
using DatajudClient.Domain.Interfaces.Repositories;
using DatajudClient.Domain.Interfaces.Services;
using DatajudClient.Domain.Services;
using DatajudClient.HttpClient;
using DatajudClient.Infrastructure.DatabaseContext;
using DatajudClient.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DatajudClient.CrossCutting.Ioc
{
    public static class DependencyResolver
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfigurationRoot configurationBuilder)
        {
            ConfigureAutoMapper(services);
            ConfigureDataBase(services, configurationBuilder);
            ConfigureRepositories(services);
            ConfigureServices(services);

            return services;
        }

        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            var autoMappercfg = new MapperConfiguration(config =>
            {
                config.AllowNullDestinationValues = false;
                config.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
                config.AddProfile(new AutoMapperConfig());
            });
            services.AddSingleton(autoMappercfg.CreateMapper());
        }

        private static void ConfigureDataBase(IServiceCollection services, IConfigurationRoot configurationBuilder)
        {
            services.AddDbContext<AppDbContext>(opts =>
            {
                opts
                //.UseLazyLoadingProxies()
                .UseMySQL(configurationBuilder.GetConnectionString("DatajudClientConnection"));
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IProcessoRepository, ProcessoRepository>();
            services.AddTransient<ITribunalRepository, TribunalRepository>();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IHttpClient, HttpClient.HttpClient>();
            services.AddTransient<IProcessoService, ProcessoService>();
            services.AddTransient<IDatajudService, DatajudService>();
            services.AddTransient<ITribunalService, TribunalService>();
            services.AddTransient<IEnderecoService, EnderecoService>();
        }
    }
}
