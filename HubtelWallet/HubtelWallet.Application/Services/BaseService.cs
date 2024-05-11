using HubtelWallet.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Application.Services
{
    internal class BaseService
    {
        protected IRepositoryManager _repositoryManager;

        public BaseService(IRepositoryManager repositoryManager) =>
            _repositoryManager = repositoryManager;
    }
}
