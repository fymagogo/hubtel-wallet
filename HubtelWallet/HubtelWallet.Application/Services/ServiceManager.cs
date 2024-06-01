using HubtelWallet.Application.Interfaces;
using HubtelWallet.Application.Interfaces.ExternalServices;
using HubtelWallet.Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
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

        public ServiceManager(IRepositoryManager repositoryManager, IFakeService fakeService, IHttpContextAccessor httpContextAccessor, IMemoryCache memoryCache)
        {
            _lazyCustomerService = new Lazy<ICustomerService>(() => new CustomerService(repositoryManager, fakeService, httpContextAccessor, memoryCache));
            _lazyWallerService = new Lazy<IWalletService>(() => new WalletService(repositoryManager, fakeService, httpContextAccessor));
        }

        public ICustomerService CustomerService => _lazyCustomerService.Value;

        public IWalletService WalletService => _lazyWallerService.Value;
    }
}
