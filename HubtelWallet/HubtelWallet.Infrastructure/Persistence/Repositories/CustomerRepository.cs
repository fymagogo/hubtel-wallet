using HubtelWallet.Domain.Entities;
using HubtelWallet.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Infrastructure.Persistence.Repositories
{
    internal class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<Customer> CreateAsync(Customer entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Customer?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Customer entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
