using HubtelWallet.Application;
using HubtelWallet.Application.Interfaces;
using HubtelWallet.Application.Models;
using HubtelWallet.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HubtelWallet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalletController : BaseController
    {
        public WalletController(IServiceManager serviceManager) : base(serviceManager)
        {
        }


        [HttpGet("customer/{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCustomerWallets(int customerId)
        {
            var res = await _serviceManager.WalletService.GetAllCustomerWallets(customerId);
            return Ok(res.ToResultDto());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWalletById(int id)
        {
            var res = await _serviceManager.WalletService.GetWalletById(id);
            return Ok(res.ToResultDto());
        }

        [HttpPost]
        [ActionName(nameof(CreateWallet))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateWallet(CreateWalletRequest createWalletRequest)
        {
            var res = await _serviceManager.WalletService.CreateWalletAsync(createWalletRequest);

            if (res.IsFailed)
                return Ok(res.ToResultDto());

            return CreatedAtAction(nameof(CreateWallet), value: res.ToResultDto());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteWallet(int id)
        {
            var res = await _serviceManager.WalletService.DeleteCustomerWallet(id);
            return Ok(res.ToResultDto());
        }


    }
}
