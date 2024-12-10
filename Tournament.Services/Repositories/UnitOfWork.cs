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
        private readonly TournamentApiContext context;
        private readonly Lazy<ITournamentRepository> tournamentRepository;

        //private readonly Lazy<IEmployeeRepository> employeeRepository;

        public ITournamentRepository TournamentRepository => tournamentRepository.Value;

        //public IEmployeeRepository EmployeeRepository => employeeRepository.Value;

        //Add More Repos

        public UnitOfWork(TournamentApiContext context)
        {
            this.context = context;
            tournamentRepository = new Lazy<ITournamentRepository>(
                () => new TournamentRepository(context)
            );
            //employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(context));
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
