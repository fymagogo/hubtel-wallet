using HubtelWallet.Domain.Entities;
using HubtelWallet.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Customer> CreateAsync(Customer entity, CancellationToken cancellationToken = default)
        {
            var customer = _dbContext.Customers.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return customer.Entity;
        }

        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Customer?>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var customers = await _dbContext.Customers.AsNoTracking().ToListAsync(cancellationToken);
            return customers.AsReadOnly();
        }

        public Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Customer entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
