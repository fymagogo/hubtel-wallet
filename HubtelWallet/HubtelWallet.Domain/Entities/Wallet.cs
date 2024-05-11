using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public Wallet() : base() 
        { }

        public WalletType WalletType { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public AccountScheme AccountScheme { get; set; }
        public string Owner { get; set; }
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        
    }

    public enum WalletType
    {
        momo,
        card
    }

    public enum AccountScheme
    {
        visa,
        mastercard,
        mtn,
        vodafone,
        airteltigo
    }
}
