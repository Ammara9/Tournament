using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Tournament.Core.Contracts
{
    public interface IGameRepository
    {
        //void Update(Employee employee);
        //void Create(Employee employee);
        //void Delete(Employee employee);
        //Task<IEnumerable<Employee>> GetEmployeesAsync(int companyId, bool trackChanges = false);
        Task<Game?> GetGameAsync(int tournamentId, int gameId, bool trackChanges = false);
        Task<IEnumerable<Game>> GetGamesAsync(int tournamentId, bool trackchanges = false);
    }
}
