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
    internal class WalletRepository : BaseRepository, IWalletRepository
    {
        public WalletRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Wallet> CreateAsync(Wallet entity)
        {
            var wallet = _dbContext.Wallets.Add(entity);
            await _dbContext.SaveChangesAsync();
            return wallet.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(x => x.Id == id);
            if (wallet is null)
                return false;

            _dbContext.Remove(wallet);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public Task<IReadOnlyList<Wallet?>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Wallet?> GetByIdAsync(int id)
        {
            var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(x => x.Id == id);
            return wallet;
        }

        public Task UpdateAsync(Wallet entity)
        {
            throw new NotImplementedException();
        }
    }
}
