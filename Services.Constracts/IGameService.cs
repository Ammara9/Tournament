using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.DTOs;
using Tournament.Core.Entities;

namespace Services.Constracts
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetGamesAsync(int tournamentId, bool trackChanges = false);
        Task<Game?> GetGameAsync(int tournamentId, int gameId, bool trackChanges = true);
        Task<Game> UpdateGameAsync(int gameId, int tournamentId, Game game);
        Task<Game> PostGameAsync(GameUpdateDto dto);
        Task<bool> DeleteGameAsync(int tournamentId, int gameId);
    }
}
