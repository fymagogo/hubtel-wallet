using HubtelWallet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Application.Interfaces.ExternalServices
{
    public interface IFakeService
    {
        public string GetCustomerName(string phoneNumber);
        public bool ValidateMomoAccount(MomoWallet wallet);
        public bool ValidateCardAccount(VisaWallet wallet);
    }
}
