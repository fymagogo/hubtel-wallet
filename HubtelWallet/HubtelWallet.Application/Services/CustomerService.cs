using FluentResults;
using HubtelWallet.Application.Dtos;
using HubtelWallet.Application.Interfaces;
using HubtelWallet.Application.Models;
using HubtelWallet.Domain.Entities;
using HubtelWallet.Domain.IRepositories;
using Mapster;
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

        public async Task<Result<CustomerDto>> CreateCustomerAsync(CreateCustomerRequest request)
        {
            //external service to provide details

            Customer newCustomer = new Customer()
            {
                PhoneNumber = request.PhoneNumber.ToInternationalNumber(),
                Name = "Test Name"
            };
            var createdCustomer = await _repositoryManager.CustomerRepository.CreateAsync(newCustomer);

            var customerDto =  createdCustomer.Adapt<CustomerDto>();
            return Result.Ok(customerDto)
                .WithSuccess("Successfully Created New Customer");
        }

        public async Task<Result<IEnumerable<CustomerDto>>> GetAllCustomersAsync()
        {
            var customers = await _repositoryManager.CustomerRepository.GetAllAsync();
            var customersDto = customers.Adapt<IEnumerable<CustomerDto>>();
            return Result.Ok(customersDto)
                .WithSuccess("Successfully Retrieved Customers");

        }
    }
}
