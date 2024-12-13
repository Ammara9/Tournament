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
                throw new ArgumentException($"Tournament with ID {id} not found.");
            }

            return mapper.Map<TournamentDetailsDto>(tournament);
        }

        // put
        public async Task<TournamentDetailsDto> UpdateTournamentAsync(
            int id,
            TournamentUpdateeDto dto
        )
        {
            if (id != dto.Id)
                throw new ArgumentException("ID Mismatch");

            var existingTournament = await uow.TournamentRepository.GetTournamentDetailsAsync(id);
            if (existingTournament == null)
            {
                throw new ArgumentException($"Tournament with ID {id} not found.");
            }

            mapper.Map(dto, existingTournament);
            await uow.CompleteAsync();

            // Return updated tournament
            return mapper.Map<TournamentDetailsDto>(existingTournament);
        }

        public async Task<TournamentDetailsDto> PostTournamentAsync(TournamentDetailsCreateDto dto)
        {
            var tournament = mapper.Map<TournamentDetails>(dto);
            uow.TournamentRepository.Add(tournament);
            await uow.CompleteAsync();

            var createdTournament = mapper.Map<TournamentDetailsDto>(tournament);
            return (createdTournament);
        }

        public async Task<bool> DeleteTournamentAsync(int id)
        {
            var tournamentDetails = await uow.TournamentRepository.GetTournamentDetailsAsync(id);
            if (tournamentDetails == null)
            {
                return false;
            }

            uow.TournamentRepository.Remove(tournamentDetails);
            await uow.CompleteAsync();

            return true;
        }
    }
}
