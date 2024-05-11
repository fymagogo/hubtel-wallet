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
    internal class WalletService : BaseService, IWalletService
    {
        public WalletService(IRepositoryManager repositoryManager) : base(repositoryManager)
        { }

        public Task<Result<IReadOnlyList<WalletDto>>> GetAllWalletsByCustomer(int CustomerId)
        {
            throw new NotImplementedException();
        }
    }
}
