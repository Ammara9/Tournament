using AutoMapper;
using Services.Constracts;
using Tournament.Core.Contracts;
using Tournament.Core.DTOs;
using Tournament.Core.Entities;

namespace Tournament.Services
{
    public class GameService : IGameService
    {
        private IUnitOfWork uow;
        private readonly IMapper mapper;

        public GameService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Game>> GetGamesAsync(
            int tournamentId,
            bool trackChanges = false
        )
        {
            var games = await uow.GameRepository.GetGamesAsync(tournamentId, trackChanges);
            return mapper.Map<IEnumerable<Game>>(games);
        }

        public async Task<object> GetPaginatedGamesAsync(
            int tournamentId,
            int pageNumber,
            int pageSize
        )
        {
            // Hämta totalt antal spel i turneringen
            var totalGames = await uow.GameRepository.GetGamesCountAsync(tournamentId);

            // Beräkna de spel som ska hämtas baserat på pageNumber och pageSize
            var games = await uow.GameRepository.GetGamesPageAsync(
                tournamentId,
                pageNumber,
                pageSize
            );

            // För att skicka tillbaka pagination info kan du skapa en response som innehåller både data och metadata
            var paginationMetadata = new
            {
                TotalItems = totalGames,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalGames / (double)pageSize),
            };

            // Skapa DTO:er för spelen
            var gameDtos = mapper.Map<IEnumerable<GameDto>>(games);

            // Returnera både metadata och DTO:erna
            return new { Pagination = paginationMetadata, Games = gameDtos };
        }

        public async Task<int> GetTotalGamesCountAsync(int tournamentId)
        {
            return await uow.GameRepository.GetGamesCountAsync(tournamentId);
        }

        public async Task<Game?> GetGameAsync(
            int tournamentId,
            int gameId,
            bool trackChanges = true
        )
        {
            return await uow.GameRepository.GetGameAsync(tournamentId, gameId, trackChanges);
        }

        // put
        public async Task<Game> UpdateGameAsync(int tournamentId, int gameId, Game game)
        {
            var existingGame = await uow.GameRepository.GetGameAsync(gameId, tournamentId);
            if (existingGame == null)
            {
                throw new ArgumentException($"Game with ID {gameId} not found.");
            }

            mapper.Map(game, existingGame);
            await uow.CompleteAsync();

            return mapper.Map<Game>(existingGame);
        }

        public async Task<Game> PostGameAsync(GameUpdateDto dto)
        {
            var totalGames = await uow.GameRepository.GetGamesCountAsync(dto.TournamentDetailsId);
            if (totalGames >= 10)
            {
                throw new InvalidOperationException("A tournament can have a maximum of 10 games.");
            }
            var games = mapper.Map<Game>(dto);
            uow.GameRepository.Add(games);
            await uow.CompleteAsync();

            return (games);
        }

        public async Task<bool> DeleteGameAsync(int tournamentId, int gameId)
        {
            var game = await uow.GameRepository.GetGameAsync(gameId, tournamentId);
            if (game == null)
            {
                return false;
            }

            uow.GameRepository.Remove(game);
            await uow.CompleteAsync();

            return true;
        }
    }
}
