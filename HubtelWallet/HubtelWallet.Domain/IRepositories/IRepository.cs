using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Domain.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);

        Task<T> CreateAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteAsync(int id);
        Task<IReadOnlyList<T?>> GetAllAsync();
    }
}
