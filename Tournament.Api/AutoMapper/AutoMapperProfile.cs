using AutoMapper;
using Tournament.Api.DTOs;
using Tournament.Core.Entities;
using Tournament.Shared.DTOs;

namespace Tournament.Api.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TournamentDetails, TournamentDetailsDto>();
            CreateMap<TournamentDetailsCreateDto, TournamentDetails>();
            CreateMap<TournamentUpdateeDto, TournamentDetails>();

            CreateMap<Game, GameDto>().ReverseMap();
            CreateMap<Game, GameUpdateDto>().ReverseMap();
;
        }
    }
}
