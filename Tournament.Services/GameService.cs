using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Services.Constracts;
using Tournament.Core.Contracts;
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

        public async Task<Game?> GetGameAsync(
            int tournamentId,
            int gameId,
            bool trackChanges = false
        )
        {
            return await uow.GameRepository.GetGameAsync(tournamentId, gameId, trackChanges);
        }
    }
}
