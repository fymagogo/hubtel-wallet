using FluentResults;
using HubtelWallet.Domain.Entities;
using HubtelWallet.Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Application.Services
{
    internal class BaseService
    {
        protected IRepositoryManager _repositoryManager;
        protected readonly IHttpContextAccessor _httpContextAccessor;


        public BaseService(IRepositoryManager repositoryManager, IHttpContextAccessor httpContextAccessor) =>
            (_repositoryManager, _httpContextAccessor) = (repositoryManager,httpContextAccessor);

        protected string GetCurrentUser()
        {
            var username = _httpContextAccessor.HttpContext.Session.GetString("Username");
            return username;
        }

        //protected async Task<Result<Customer>> CurrentCustomerDetails()
        //{
        //    var username = GetCurrentUser();
        //    if (username is null)
        //        //return Result.Fail($"Current customer not found");
        //        throw new BadHttpRequestException("Current customer not found");

        //    var customer = await _repositoryManager.CustomerRepository.GetExtendedByPhoneNumberAsync(username);
        //    if (customer is null)
        //        throw new BadHttpRequestException("Customer not found");
        //        //return Result.Fail($"Customer not found");
        //    return Result.Ok(customer);
        //}

        protected async Task<Customer> GetCurrentCustomerDetails()
        {
            var username = GetCurrentUser();
            if (username is null)
                //return Result.Fail($"Current customer not found");
                throw new BadHttpRequestException("Current customer not found");

            var customer = await _repositoryManager.CustomerRepository.GetExtendedByPhoneNumberAsync(username);
            if (customer is null)
                throw new BadHttpRequestException("Customer not found");
            //return Result.Fail($"Customer not found");
            return customer;
        }
    }
}
