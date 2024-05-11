using HubtelWallet.Application;
using HubtelWallet.Application.Interfaces;
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
        public async Task<IActionResult> GetCustomer()
        {
            var res = await _serviceManager.CustomerService.GetAllCustomersAsync();
            return Ok(res.ToResultDto());
        }
    }
}
