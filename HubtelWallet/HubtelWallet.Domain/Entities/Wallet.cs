using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        
    }

    public enum WalletType
    {
        momo,
        card,
        unknown
    }

    public enum AccountScheme
    {
        visa,
        mastercard,
        mtn,
        vodafone,
        airteltigo
    }

    public class MomoWallet : Wallet
    {
    }

    public class VisaWallet : Wallet
    {

        public VisaWallet(int customerId, string owner, string name, WalletType walletType, AccountScheme accountScheme,string accountNumber, DateTime issueDate, DateTime expiryDate)
        {
            AccountNumber = SetAccountNumber(accountNumber);
            MaskedVisaNumber = ComputeHash(accountNumber);
            IssueDate = issueDate;
            ExpiryDate = expiryDate;
            CustomerId = customerId;
            Owner = owner;
            Name = name;
            WalletType = walletType;
            AccountScheme = accountScheme;
        }

        public DateTime ExpiryDate { get; set; }
        public DateTime IssueDate { get; set; }
        public string MaskedVisaNumber {  get; private set; }

        private string SetAccountNumber(string value)
        {
            return value.Substring(0, 6) + new string('x', value.Length - 6);
        }

        private string ComputeHash(string input)
        {
            using (SHA512 sha512Hash = SHA512.Create())
            {
                // Compute hash from input string
                byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to a string representation (hexadecimal)
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Convert each byte to 2-digit hexadecimal representation
                }
                return builder.ToString();
            }
        }
    }
}
