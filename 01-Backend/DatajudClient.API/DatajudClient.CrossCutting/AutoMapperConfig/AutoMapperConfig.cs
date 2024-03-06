using AutoMapper;
using DatajudClient.Domain.DTO.Enderecos;
using DatajudClient.Domain.DTO.Processos;
using DatajudClient.Domain.DTO.Tribunais;
using DatajudClient.Domain.Models.Endereco;
using DatajudClient.Domain.Models.Processos;
using DatajudClient.Domain.Models.Tribunais;

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
            CreateMap<AndamentoProcesso, ReadAndamentoDTO>().ReverseMap();
            CreateMap<ComplementoAndamento, ReadComplementoAndamentoDTO>().ReverseMap();
            CreateMap<Processo, ReadProcessoDTO>().ReverseMap();
            CreateMap<Tribunal, ReadTribunalDTO>().ReverseMap();
            CreateMap<CategoriaTribunal, ReadCategoriaTribunalDTO>().ReverseMap();
            CreateMap<Estado, ReadEstadoDTO>().ReverseMap();
            
        }
    }
}
