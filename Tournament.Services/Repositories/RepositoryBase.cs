using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.Contracts;
using Tournament.Data.Data;

namespace Tournament.Services.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        protected TournamentApiContext Context { get; }
        protected DbSet<T> DbSet { get; }

        public RepositoryBase(TournamentApiContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public IQueryable<T> FindAll(bool trackChanges = false) =>
            trackChanges ? DbSet : DbSet.AsNoTracking();

        public IQueryable<T> FindByCondition(
            Expression<Func<T, bool>> expression,
            bool trackChanges = false
        ) => trackChanges ? DbSet.Where(expression) : DbSet.Where(expression).AsNoTracking();

        public void Add(T entity) => DbSet.Add(entity);

        public void Remove(T entity) => DbSet.Remove(entity);

        public void Update(T entity) => DbSet.Update(entity);
    }
}
