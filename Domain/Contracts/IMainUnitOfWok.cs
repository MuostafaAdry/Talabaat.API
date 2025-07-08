using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IMainUnitOfWork
    {
        IMainRepo<T, TKey> GetRepository<T, TKey>() where T : BaseEntity<TKey>;
        IProductRepository ProductRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
