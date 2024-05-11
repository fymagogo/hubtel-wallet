using HubtelWallet.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Infrastructure.Persistence.Repositories
{
    internal class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<ICustomerRepository> _lazyCustomerRepository;
        private readonly Lazy<IWalletRepository> _lazyWalletRepository;
        public RepositoryManager(AppDbContext dbContext) 
        {
            _lazyCustomerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(dbContext));
            _lazyWalletRepository = new Lazy<IWalletRepository>(() => new WalletRepository(dbContext));
        }
        public ICustomerRepository CustomerRepository => _lazyCustomerRepository.Value;

        public IWalletRepository WalletRepository => _lazyWalletRepository.Value;
    }
}
