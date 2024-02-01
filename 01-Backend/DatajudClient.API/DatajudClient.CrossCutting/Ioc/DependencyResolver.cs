using AutoMapper;
using DatajudClient.Domain.Interfaces.Repositories;
using DatajudClient.Domain.Interfaces.Repositories.Processos;
using DatajudClient.Domain.Interfaces.Services.Processos;
using DatajudClient.Domain.Services;
using DatajudClient.Infrastructure.DatabaseContext;
using DatajudClient.Infrastructure.Repositories;
using DatajudClient.Infrastructure.Repositories.Processos;
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
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProcessoService, ProcessoService>();
        }
    }
}
