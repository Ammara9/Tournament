using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Entities;

namespace Tournament.Core.Repositories
{
    public interface IUnitOfWork
    {
        ITournamentRepository TournamentRepository { get; }

        //IGameRepository GameRepository { get; }
        Task CompleteAsync();
    }
}
