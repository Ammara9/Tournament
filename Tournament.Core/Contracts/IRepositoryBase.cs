﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges = false);
        IQueryable<T> FindByCondition(
            Expression<Func<T, bool>> expression,
            bool trackChanges = false
        );
        void Add(T entity);

        //void Update(T entity);
        void Remove(T entity);
    }
}