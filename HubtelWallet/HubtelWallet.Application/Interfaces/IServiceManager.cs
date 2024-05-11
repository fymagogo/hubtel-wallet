using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Application.Interfaces
{
    public interface IServiceManager
    {
        ICustomerService CustomerService { get; }
        IWalletService WalletService { get; }
    }
}
