using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Domain.Shared
{
    public record Result<T>(bool Status, string? Message, IEnumerable<Error> Errors, T Data);
}
