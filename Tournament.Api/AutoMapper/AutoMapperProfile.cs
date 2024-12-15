using AutoMapper;
using Tournament.Core.DTOs;
using Tournament.Core.Entities;

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
            CreateMap<GameUpdateDto, Game>();
        }
    }
}
