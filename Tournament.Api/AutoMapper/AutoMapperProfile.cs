using AutoMapper;
using Tournament.Api.DTOs;
using Tournament.Core.Entities;

namespace Tournament.Api.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TournamentDetails, TournamentDetailsDto>();
        }
    }
}
