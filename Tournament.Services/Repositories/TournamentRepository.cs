using Microsoft.EntityFrameworkCore;
using Tournament.Core.Contracts;
using Tournament.Core.Entities;
using Tournament.Data.Data;

namespace Tournament.Services.Repositories
{
    public class TournamentRepository : RepositoryBase<TournamentDetails>, ITournamentRepository
    {
        public TournamentRepository(TournamentApiContext context)
            : base(context) { }

        public async Task<bool> AnyAsync(int id)
        {
            return await Context.TournamentDetails.AnyAsync(c => c.Id == id);
        }

        public async Task<TournamentDetails?> GetTournamentDetailsAsync(
            int id,
            bool trackChanges = false
        )
        {
            return await FindByCondition(c => c.Id.Equals(id), trackChanges).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TournamentDetails>> GetTournaments(
            bool includeGames = false,
            bool trackChanges = false
        )
        {
            return includeGames
                ? await FindAll(trackChanges).Include(t => t.Games).ToListAsync()
                : await FindAll(trackChanges).ToListAsync();
        }
    }
}
