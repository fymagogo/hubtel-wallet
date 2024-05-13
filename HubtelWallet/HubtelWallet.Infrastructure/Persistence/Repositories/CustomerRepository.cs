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

        public async Task<Customer> CreateAsync(Customer entity)
        {
            var customer = _dbContext.Customers.Add(entity);
            await _dbContext.SaveChangesAsync();
            return customer.Entity;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Customer?>> GetAllAsync()
        {
            var customers = await _dbContext.Customers.AsNoTracking().ToListAsync();
            return customers.AsReadOnly();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer?> GetCustomerByPhoneNumber(string phoneNumber)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        }

        public async Task<Customer?> GetExtendedByIdAsync(int id)
        {
            var customer =  await _dbContext.Customers
                .Include(x => x.Wallets)
                .FirstOrDefaultAsync(x => x.Id == id);
            return customer;
        }

        public Task UpdateAsync(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
