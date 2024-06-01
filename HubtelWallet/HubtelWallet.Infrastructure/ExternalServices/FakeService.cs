using HubtelWallet.Application.Interfaces.ExternalServices;
using HubtelWallet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Infrastructure.ExternalServices
{
    internal class FakeService : IFakeService
    {
        public string GetCustomerName(string phoneNumber)
        {
            return "Fidel Agogo" + phoneNumber;
        }

        public bool ValidateCardAccount(VisaWallet wallet)
        {
            return true;
        }

        public bool ValidateMomoAccount(MomoWallet wallet)
        {
            return true;
        }
    }
}
