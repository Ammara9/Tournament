using AutoMapper;
using Services.Constracts;
using Tournament.Core.Contracts;
using Tournament.Core.DTOs;
using Tournament.Core.Entities;

namespace Tournament.Services
{
    public class TournamentService : ITournamentService
    {
        private IUnitOfWork uow;
        private readonly IMapper mapper;

        public TournamentService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TournamentDetailsDto>> GetTournamentDetailsAsync(
            bool includeGames,
            bool trackChanges = false
        )
        {
            return mapper.Map<IEnumerable<TournamentDetailsDto>>(
                await uow.TournamentRepository.GetTournaments(includeGames, trackChanges)
            );
        }

        public async Task<TournamentDetailsDto> GetTournamentAsync(
            int id,
            bool trackChanges = false
        )
        {
            TournamentDetails? tournament =
                await uow.TournamentRepository.GetTournamentDetailsAsync(id);

            if (tournament == null)
            {
                //ToDo: Fix later
            }

            return mapper.Map<TournamentDetailsDto>(tournament);
        }
    }
}
