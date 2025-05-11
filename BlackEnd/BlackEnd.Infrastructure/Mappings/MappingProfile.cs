using AutoMapper;
using BlackEnd.Application.DTOs;
using BlackEnd.Domain.Entities;

namespace BlackEnd.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();
        }
    }
}
