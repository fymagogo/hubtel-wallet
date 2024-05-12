using FluentResults;
using HubtelWallet.Application.Dtos;
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
    internal class CustomerService : BaseService, ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(IRepositoryManager repositoryManager) : base(repositoryManager)
        { }

        public Task<Result<IReadOnlyList<CustomerDto>>> GetAllCustomersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
