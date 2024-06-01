using FluentResults;
using HubtelWallet.Application.Dtos;
using HubtelWallet.Application.Interfaces;
using HubtelWallet.Application.Interfaces.ExternalServices;
using HubtelWallet.Application.Models;
using HubtelWallet.Domain.Entities;
using HubtelWallet.Domain.IRepositories;
using Mapster;
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
    internal class CustomerService : BaseService, ICustomerService
    {
        private readonly IFakeService _fakeService;
        private readonly IMemoryCache _cache;
        public CustomerService(IRepositoryManager repositoryManager, IFakeService fakeService, IHttpContextAccessor httpContextAccessor, IMemoryCache memoryCache) : base(repositoryManager, httpContextAccessor)
        {
            _fakeService = fakeService;
            _cache = memoryCache;
        }

        public async Task<Result<CustomerDto>> CreateCustomerAsync(CreateCustomerRequest request)
        {
            //external service to provide bio details like name
            var customerName = _fakeService.GetCustomerName(request.PhoneNumber.ToInternationalNumber());

            Customer newCustomer = new Customer()
            {
                PhoneNumber = request.PhoneNumber.ToInternationalNumber(),
                Name = customerName // name provided by external service
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
            var isPresent = _cache.TryGetValue(phoneNumber.ToInternationalNumber(), out string cachedToken);

            if (isPresent)
            {
                var isCorrect = token == cachedToken;
                if(isCorrect)
                    _cache.Remove(phoneNumber.ToInternationalNumber());
                return isCorrect;
            }

            return false;

        }

        public async Task<Result<string>> GetCustomerToken(string phoneNumber)
        {
            //check if customer exists
            Customer customer = null;
            customer = await _repositoryManager.CustomerRepository.GetCustomerByPhoneNumber(phoneNumber.ToInternationalNumber());
            if(customer is null)
            {
                var customerName = _fakeService.GetCustomerName(phoneNumber.ToInternationalNumber());
                Customer newCustomer = new Customer()
                {
                    PhoneNumber = phoneNumber.ToInternationalNumber(),
                    Name = customerName // name provided by external service
                };
                var createdCustomer = await _repositoryManager.CustomerRepository.CreateAsync(newCustomer);
                customer = createdCustomer;
            }

            var token = Extension.RandomToken();
            _cache.Set(phoneNumber.ToInternationalNumber(), token, TimeSpan.FromMinutes(2));

            return Result.Ok(token);
        }

        public async Task<Result<bool>> LogoutCustomer(string phoneNumber)
        {
            //check if customer exists
            
            var customer = await _repositoryManager.CustomerRepository.GetCustomerByPhoneNumber(phoneNumber.ToInternationalNumber());
            if (customer is null)
                return Result.Fail<bool>("Customer Not Found");

            await _repositoryManager.CustomerRepository.UpdateAsync(customer);


            return Result.Ok(true);
        }

        public async Task<Result<CustomerDto>> GetCustomerByPhoneNumber(string phoneNumber)
        {
            var customer = await _repositoryManager.CustomerRepository.GetCustomerByPhoneNumber(phoneNumber.ToInternationalNumber());
            if (customer is null)
                return Result.Fail<CustomerDto>("Customer Not Found");
            var customerDto = customer.Adapt<CustomerDto>();
            return Result.Ok(customerDto)
                .WithSuccess("Successfully Retrieved Customer");
        }

        public async Task<Result<CustomerDto>> GetCurrentCustomer()
        {
            var customer = GetCurrentCustomerDetails();
            if (customer is null)
                return Result.Fail<CustomerDto>("Customer Not Found");
            var customerDto = customer.Adapt<CustomerDto>();
            return Result.Ok(customerDto)
                .WithSuccess("Successfully Retrieved Customer");
        }
    }
}
