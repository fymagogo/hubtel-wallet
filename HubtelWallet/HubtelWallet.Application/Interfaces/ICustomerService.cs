﻿using FluentResults;
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
        public Task<Result<CustomerDto>> GetCustomerById(int customerId);
        public Task<Result<CustomerDto>> GetCustomerByPhoneNumber(string phoneNumber);
        public Task<Result<CustomerDto>> GetCurrentCustomer();
        public Task<Result<String>> GetCustomerToken(string phoneNumber);
        public Task<bool> ValidateCustomerToken(string phoneNumber, string token);
        public Task<Result<bool>> LogoutCustomer(string phoneNumber);

    }
}
