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
    public class TournamentRepository : RepositoryBase<TournamentDetails>, ITournamentRepository
    {
        //private readonly TournamentApiContext _context;

        public TournamentRepository(TournamentApiContext context)
            : base(context)
        {
            //_context = context;
        }

        //public void Add(TournamentDetails tournament)
        //{
        //    Context.TournamentDetails.Add(tournament);
        //}

        //Combine method
        public async Task<TournamentDetails?> GetTournamentDetailsAsync(int id)
        {
            return await Context.TournamentDetails.FindAsync(id);
        }

        public async Task<IEnumerable<TournamentDetails>> GetTournaments(bool includeGames = false)
        {
            return includeGames
                ? await Context.TournamentDetails.Include(t => t.Games).ToListAsync()
                : await Context.TournamentDetails.ToListAsync();
        }

        //public void Remove(TournamentDetails tournamentDetails)
        //{
        //    Context.TournamentDetails.Remove(tournamentDetails);
        //}
    }
}
