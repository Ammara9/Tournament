using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Contracts;
using Tournament.Data.Data;

namespace Tournament.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TournamentApiContext _context;
        public ITournamentRepository TournamentRepository { get; }

        //public IGameRepository GameRepository { get; }
        public UnitOfWork(TournamentApiContext _context)
        {
            this._context = _context;
            TournamentRepository = new TournamentRepository(_context);
            //GameRepository = new GameRepository(_context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
