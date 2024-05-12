using FluentResults;
using HubtelWallet.Application.Dtos;
using HubtelWallet.Application.Interfaces;
using HubtelWallet.Application.Models;
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
    internal class WalletService : BaseService, IWalletService
    {
        public WalletService(IRepositoryManager repositoryManager) : base(repositoryManager)
        { }

        public Task<Result<WalletDto>> CreateWalletAsync(CreateWalletRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> DeleteCustomerWallet(int walletId)
        {
            var deleteWallet = await _repositoryManager.WalletRepository.DeleteAsync(walletId);
            if (deleteWallet is false)
                return Result.Fail($"Wallet with id {walletId} not found");

            return Result.Ok(deleteWallet)
                .WithSuccess($"Wallet with id {walletId} deleted successflly");
        }

        public Task<Result<IEnumerable<WalletDto>>> GetAllCustomerWallets(int CustomerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<WalletDto>> GetCustomerWalletById(int walletId)
        {
            var wallet = await _repositoryManager.WalletRepository.GetByIdAsync(walletId);
            if (wallet is null)
                return Result.Fail($"Wallet with id {walletId} not found");

            var walletDto = wallet.Adapt<WalletDto>();

            return Result.Ok(walletDto)
                .WithSuccess($"Wallet with id {walletId} retrieved successflly");
        }
    }
}
