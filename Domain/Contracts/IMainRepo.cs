using Domain.Models;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IMainRepo<T, TKey> where T : BaseEntity<TKey>
//public class MainRepo<T, TKey> : IMainRepo<T, TKey> where T : BaseEntity<TKey>
    {
        public Task<string> Create(T entity);

        public Task Create(List<T> entities);

        public string Edit(T entity);

        public void Delete(T entity);

        public Task<bool> Exist(Expression<Func<T, bool>> predicate);

        public void Delete(List<T> entities);

        public void Commit();

        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includes = null, bool tracked = true );

        public Task<T?> GetOne(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includes = null, bool tracked = true);


        
    }
}
