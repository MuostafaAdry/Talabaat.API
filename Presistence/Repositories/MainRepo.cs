using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System.Linq.Expressions;
using System.Linq;

public class MainRepo<T, TKey> : IMainRepo<T, TKey> where T : BaseEntity<TKey>
{
    private readonly ApplicationDbContext dbContext;
    public readonly DbSet<T> dbSet;

    public MainRepo(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
        dbSet = dbContext.Set<T>();
    }

    public async Task<string> Create(T entity)
    {
        await dbSet.AddAsync(entity);
        return "success";
    }

    public async Task Create(List<T> entities) => await dbSet.AddRangeAsync(entities);

    public string Edit(T entity)
    {
        dbSet.Update(entity);
        return "success";
    }

    public void Delete(T entity) => dbSet.Remove(entity);

    public void Delete(List<T> entities) => dbSet.RemoveRange(entities);

    public void Commit() => dbContext.SaveChanges();

    public IQueryable<T> Get(
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, object>>[]? includes = null,
        bool tracked = true)
    {
        IQueryable<T> query = dbSet;

        if (filter is not null)
            query = query.Where(filter);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        return tracked ? query : query.AsNoTracking();
    }

    public async Task<T?> GetOne(
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, object>>[]? includes = null,
        bool tracked = true)
        => await Get(filter, includes, tracked).FirstOrDefaultAsync();

    public async Task<bool> Exist(Expression<Func<T, bool>> predicate)
        => await dbSet.AnyAsync(predicate);
}
