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
    public interface IWalletService : IService
    {
        public Task<Result<WalletDto>> CreateWalletAsync(CreateWalletRequest request);
        public Task<Result<IEnumerable<WalletDto>>> GetAllCustomerWallets(int customerId);
        public Task<Result<WalletDto>> GetCustomerWalletById(int walletId);
        public Task<Result<bool>> DeleteCustomerWallet(int walletId);
    }
}
