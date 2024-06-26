﻿using HubtelWallet.Domain.Entities;

namespace HubtelWallet.Domain.IRepositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> GetExtendedByIdAsync(int id);
        Task<Customer?> GetCustomerByPhoneNumber(string phoneNumber);
        Task<Customer?> GetExtendedByPhoneNumberAsync(string phoneNumber);
    }
}
