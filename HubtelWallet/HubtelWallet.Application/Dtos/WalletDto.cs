using HubtelWallet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Application.Dtos
{
    public record WalletDto(int Id, string Name, WalletType WalletType, string AccountNumber, AccountScheme AccountScheme);
}
