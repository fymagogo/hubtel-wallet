using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Domain.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

        Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
