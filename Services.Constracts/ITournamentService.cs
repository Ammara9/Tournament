using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.DTOs;

namespace Services.Constracts
{
    public interface ITournamentService
    {
        Task<IEnumerable<TournamentDetailsDto>> GetTournamentDetailsAsync(
            bool includeGames,
            bool trackChanges = false
        );
        Task<TournamentDetailsDto> GetTournamentAsync(int id, bool trackChanges = false);

        Task<TournamentDetailsDto> UpdateTournamentAsync(int id, TournamentUpdateeDto dto);

        Task<TournamentDetailsDto> PostTournamentAsync(TournamentDetailsCreateDto dto);
        Task<bool> DeleteTournamentAsync(int id);
    }
}
