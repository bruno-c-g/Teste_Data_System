using AutoMapper;
using TesteDataSystem.Application.DTOs;
using TesteDataSystem.Domain.Entities;

namespace TesteDataSystem.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<DataBase, DataBaseDTO>().ReverseMap();
        }
    }
}
