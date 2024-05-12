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

        public Task<Wallet> CreateAsync(Wallet entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(x => x.Id == id);
            if (wallet is null)
                return false;

            _dbContext.Remove(wallet);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Wallet?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(x => x.Id == id);
            return wallet;
        }

        public Task UpdateAsync(Wallet entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
