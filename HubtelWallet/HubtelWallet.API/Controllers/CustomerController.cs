using HubtelWallet.Application;
using HubtelWallet.Application.Interfaces;
using HubtelWallet.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace HubtelWallet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        public CustomerController(IServiceManager serviceManager) : base(serviceManager)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCustomers()
        {
            var res = await _serviceManager.CustomerService.GetAllCustomersAsync();
            return Ok(res.ToResultDto());
        }

        [HttpPost]
        [ActionName(nameof(CreateCustomer))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequest createCustomerRequest)
        {
            var res = await _serviceManager.CustomerService.CreateCustomerAsync(createCustomerRequest);
            return CreatedAtAction(nameof(CreateCustomer), value: res.ToResultDto());
        }
    }
}
