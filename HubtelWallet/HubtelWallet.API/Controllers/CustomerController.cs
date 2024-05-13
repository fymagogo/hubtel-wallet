using HubtelWallet.Application;
using HubtelWallet.Application.Interfaces;
using HubtelWallet.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCustomers()
        {
            var res = await _serviceManager.CustomerService.GetAllCustomersAsync();
            return Ok(res.ToResultDto());
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var res = await _serviceManager.CustomerService.GetCustomerById(id);
            return Ok(res.ToResultDto());
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ActionName(nameof(Login))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(CreateCustomerRequest createCustomerRequest)
        {
            var res = await _serviceManager.CustomerService.GetCustomerToken(createCustomerRequest.PhoneNumber);

            if (res.IsFailed)
                return Ok(res.ToResultDto());

            return Ok(res.ToResultDto());
        }

        [HttpPost("logout")]
        [Authorize]
        [ActionName(nameof(Logout))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            var username = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var res = await _serviceManager.CustomerService.LogoutCustomer(username);

            if (res.IsFailed)
                return Ok(res.ToResultDto());

            return Ok(res.ToResultDto());
        }
    }
}
