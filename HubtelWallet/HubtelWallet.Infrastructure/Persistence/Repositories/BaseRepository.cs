using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Infrastructure.Persistence.Repositories
{
    internal class BaseRepository
    {
        protected readonly AppDbContext _dbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
