using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Tournament.Core.Contracts
{
    public interface ITournamentRepository
    {
        void Add(TournamentDetails tournament);
        Task<TournamentDetails?> GetTournamentDetailsAsync(int id, bool trackChanges = false);
        Task<IEnumerable<TournamentDetails>> GetTournaments(
            bool includeGame = false,
            bool trackChanges = false
        );
        void Remove(TournamentDetails tournamentDetails);

        //Task<bool> AnyAsync(int id);
        void Update(TournamentDetails tournament);
    }
}
