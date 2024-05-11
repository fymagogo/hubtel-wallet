using FluentResults;
using HubtelWallet.Application.Dtos;
using HubtelWallet.Application.Interfaces;
using HubtelWallet.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Application.Services
{
    internal class CustomerService : BaseService, ICustomerService
    {
        public CustomerService(IRepositoryManager repositoryManager) : base( repositoryManager) 
        { }

        public Task<Result<IReadOnlyList<CustomerDto>>> GetAllCustomersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
