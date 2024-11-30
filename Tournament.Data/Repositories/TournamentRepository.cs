using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentApiContext _context;

        public TournamentRepository(TournamentApiContext context)
        {
            _context = context;
        }

        public void Add(TournamentDetails tournament)
        {
            _context.TournamentDetails.Add(tournament);
        }

        //Combine method
        public async Task<TournamentDetails?> GetTournamentDetailsAsync(int id)
        {
            return await _context.TournamentDetails.FindAsync(id);
        }

        public async Task<IEnumerable<TournamentDetails>> GetTournaments(bool includeGames = false)
        {
            return includeGames
                ? await _context.TournamentDetails.Include(t => t.Games).ToListAsync()
                : await _context.TournamentDetails.ToListAsync();
        }

        public void Remove(TournamentDetails tournamentDetails)
        {
            _context.TournamentDetails.Remove(tournamentDetails);
        }
    }
}
