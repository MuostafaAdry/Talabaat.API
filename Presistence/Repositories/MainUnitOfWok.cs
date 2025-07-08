using Domain.Contracts;
using Domain.Models;
using Domain.Models.ProductModule;
using Presistence.Data;
using Presistence.Repositories;

namespace Presistence.Repositories;

public class MainUnitOfWork : IMainUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    private IProductRepository _productRepository;

    public MainUnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IProductRepository ProductRepository =>
        _productRepository ??= new ProductRepository(_dbContext);

    public IMainRepo<T, TKey> GetRepository<T, TKey>() where T : BaseEntity<TKey>
    {
        if (typeof(T) == typeof(Product))
        {
            return (IMainRepo<T, TKey>)ProductRepository;
        }

        return new MainRepo<T, TKey>(_dbContext);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}
