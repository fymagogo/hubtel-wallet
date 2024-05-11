using HubtelWallet.Domain.Entities;
using HubtelWallet.Domain.IRepositories;
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

        public Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Wallet?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Wallet entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
