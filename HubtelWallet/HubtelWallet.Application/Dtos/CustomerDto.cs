using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Application.Dtos
{
    public record CustomerDto(int Id, string Name, string PhoneNumber, IEnumerable<WalletDto> Wallets);
}
