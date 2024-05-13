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
        public CustomerService(IRepositoryManager repositoryManager) : base(repositoryManager)
        { }

        public async Task<Result<CustomerDto>> CreateCustomerAsync(CreateCustomerRequest request)
        {
            //external service to provide bio details like name

            Customer newCustomer = new Customer()
            {
                PhoneNumber = request.PhoneNumber.ToInternationalNumber(),
                Name = "Test Name", // name provided by external service
                Token = Extension.RandomToken()
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

        public async Task<Result<CustomerDto>> GetCustomerById(int customerId)
        {
            var customer = await _repositoryManager.CustomerRepository.GetExtendedByIdAsync(customerId);
            if (customer is null)
                return Result.Fail($"Customer with id {customerId} not found");

            var customerDto = customer.Adapt<CustomerDto>();
            return Result.Ok(customerDto)
                .WithSuccess("Successfully Retrieved Customer");
        }

        public async Task<bool> ValidateCustomerToken(string phoneNumber, string token)
        {
            Customer customer = await _repositoryManager.CustomerRepository.GetCustomerByPhoneNumber(phoneNumber.ToInternationalNumber());
            if (customer is null)
                return false;
            if (customer.Token != token)
                return false;
            return true;

        }

        public async Task<Result<string>> GetCustomerToken(string phoneNumber)
        {
            //check if customer exists
            Customer customer = null;
            customer = await _repositoryManager.CustomerRepository.GetCustomerByPhoneNumber(phoneNumber.ToInternationalNumber());
            if(customer is null)
            {
                Customer newCustomer = new Customer()
                {
                    PhoneNumber = phoneNumber.ToInternationalNumber(),
                    Name = "Test Name", // name provided by external service
                    Token = Extension.RandomToken()
                };
                var createdCustomer = await _repositoryManager.CustomerRepository.CreateAsync(newCustomer);
                customer = createdCustomer;
            }

            return Result.Ok(customer.Token);
        }
    }
}
