using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public Customer() : base()
        { }

        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<Wallet> Wallets { get; } = new List<Wallet>();

    }
}
