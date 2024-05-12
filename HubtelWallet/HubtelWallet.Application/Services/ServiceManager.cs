using HubtelWallet.Application.Interfaces;
using HubtelWallet.Domain.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICustomerService> _lazyCustomerService;
        private readonly Lazy<IWalletService> _lazyWallerService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _lazyCustomerService = new Lazy<ICustomerService>(() => new CustomerService(repositoryManager));
            _lazyWallerService = new Lazy<IWalletService>(() => new WalletService(repositoryManager));
        }

        public ICustomerService CustomerService => _lazyCustomerService.Value;

        public IWalletService WalletService => _lazyWallerService.Value;
    }
}
