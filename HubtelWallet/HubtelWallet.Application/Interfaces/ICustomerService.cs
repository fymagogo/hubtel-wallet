using FluentResults;
using HubtelWallet.Application.Dtos;
using HubtelWallet.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Application.Interfaces
{
    public interface ICustomerService : IService
    {
        public Task<Result<IEnumerable<CustomerDto>>> GetAllCustomersAsync();
        public Task<Result<CustomerDto>> CreateCustomerAsync(CreateCustomerRequest request);
    }
}
