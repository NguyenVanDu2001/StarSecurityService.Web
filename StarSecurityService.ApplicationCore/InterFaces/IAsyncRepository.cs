﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StarSecurityService.ApplicationCore.InterFaces
{
    public interface IAsyncRepository<T> : IDisposable where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetAllAsync();
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> GetAllListAsync();
    }
}
