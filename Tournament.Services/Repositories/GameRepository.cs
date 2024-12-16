using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.Contracts;
using Tournament.Core.Entities;
using Tournament.Data.Data;

namespace Tournament.Services.Repositories
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(TournamentApiContext context)
            : base(context) { }

        public async Task<Game?> GetGameAsync(
            int tournamentId,
            int gameId,
            bool trackChanges = false
        )
        {
            return await FindByCondition(
                    e => e.Id.Equals(gameId) && e.TournamentDetailsId.Equals(tournamentId),
                    trackChanges
                )
                .FirstOrDefaultAsync();
        }

        //pagination
        public async Task<IEnumerable<Game>> GetGamesPageAsync(
            int tournamentId,
            int pageNumber,
            int pageSize
        )
        {
            return await Context
                .Games.Where(g => g.TournamentDetailsId == tournamentId)
                .Skip((pageNumber - 1) * pageSize) // Hoppa över tidigare sidor
                .Take(pageSize) // Ta endast de spel som motsvarar pageSize
                .ToListAsync();
        }

        public async Task<int> GetGamesCountAsync(int tournamentId)
        {
            return await Context
                .Games.Where(g => g.TournamentDetailsId == tournamentId)
                .CountAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesAsync(
            int tournamentId,
            bool trackchanges = false
        )
        {
            return await FindByCondition(
                    e => e.TournamentDetailsId.Equals(tournamentId),
                    trackchanges
                )
                .ToListAsync();
        }
    }
}
