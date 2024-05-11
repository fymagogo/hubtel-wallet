using FluentResults;
using HubtelWallet.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Application.Interfaces
{
    public interface IWalletService : IService
    {
        public Task<Result<IReadOnlyList<WalletDto>>> GetAllWalletsByCustomer(int CustomerId);
    }
}
