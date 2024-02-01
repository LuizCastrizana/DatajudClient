using AutoMapper;
using DatajudClient.Domain.DTO.Processos;
using DatajudClient.Domain.Models.Processos;

namespace DatajudClient.CrossCutting
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);

            CreateMap<Processo, CreateProcessoDTO>().ReverseMap();
            CreateMap<Processo, ReadProcessoDTO>().ReverseMap();
        }
    }
}
