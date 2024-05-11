using HubtelWallet.Application;
using HubtelWallet.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HubtelWallet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : BaseController
    {
        public WalletController(IServiceManager serviceManager) : base(serviceManager)
        {
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerWallets(int customerId)
        {
            var res = await _serviceManager.WalletService.GetAllWalletsByCustomer(customerId);
            return Ok(res.ToResultDto());
        }
    }
}
