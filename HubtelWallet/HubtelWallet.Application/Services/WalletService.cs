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
    internal class WalletService : BaseService, IWalletService
    {
        public WalletService(IRepositoryManager repositoryManager) : base(repositoryManager)
        { }

        public async Task<Result<WalletDto>> CreateWalletAsync(CreateWalletRequest request)
        {

            var customer = await _repositoryManager.CustomerRepository.GetExtendedByIdAsync(request.CustomerId);
            if (customer is null)
                return Result.Fail($"Customer with id {request.CustomerId} not found");

            if(customer.Wallets.Count == 5)
                return Result.Fail($"Customer cannot add more than 5 wallets");

            Wallet newWallet = new Wallet()
            {
                CustomerId = customer.Id,
                Name = request.Name,
                AccountNumber = request.AccountNumber,
                Owner = customer.PhoneNumber,
                AccountScheme = request.AccountScheme,
                WalletType = request.WalletType
            };

            var createdWallet = await _repositoryManager.WalletRepository.CreateAsync(newWallet);

            var walletDto = createdWallet.Adapt<WalletDto>();
            return Result.Ok(walletDto)
                .WithSuccess("Successfully Created New Wallet");
        }

        public async Task<Result<bool>> DeleteCustomerWallet(int walletId)
        {
            var deleteWallet = await _repositoryManager.WalletRepository.DeleteAsync(walletId);
            if (deleteWallet is false)
                return Result.Fail($"Wallet with id {walletId} not found");

            return Result.Ok(deleteWallet)
                .WithSuccess($"Wallet with id {walletId} deleted successflly");
        }

        public async Task<Result<IEnumerable<WalletDto>>> GetAllCustomerWallets(int customerId)
        {
            var customer = await _repositoryManager.CustomerRepository.GetExtendedByIdAsync(customerId);
            if (customer is null)
                return Result.Fail($"Customer with id {customerId} not found");
            var wallets = customer.Wallets;

            var walletsDto = wallets.Adapt<IEnumerable<WalletDto>>();
            return Result.Ok(walletsDto)
                .WithSuccess($"Successfully Retrieved Wallets for Customer {customer.Name}");
        }

        public async Task<Result<WalletDto>> GetWalletById(int walletId)
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
