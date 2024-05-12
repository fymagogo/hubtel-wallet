using HubtelWallet.Application;
using HubtelWallet.Application.Interfaces;
using HubtelWallet.Application.Models;
using HubtelWallet.Domain.Entities;
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


        [HttpGet("customer/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCustomerWallets(int id)
        {
            var res = await _serviceManager.WalletService.GetAllCustomerWallets(id);
            return Ok(res.ToResultDto());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWalletById(int id)
        {
            var res = await _serviceManager.WalletService.GetCustomerWalletById(id);
            return Ok(res.ToResultDto());
        }

        [HttpPost]
        [ActionName(nameof(CreateWallet))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateWallet(CreateWalletRequest createWalletRequest)
        {
            //var res = await _serviceManager.WalletService.GetAllWalletsByCustomer(walletId);
            var res = await _serviceManager.WalletService.GetCustomerWalletById(1);
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
