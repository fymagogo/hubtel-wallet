using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Domain.IRepositories
{
    public interface IRepositoryManager
    {
        ICustomerRepository CustomerRepository  { get; }
        IWalletRepository WalletRepository  { get; }
    }
}
