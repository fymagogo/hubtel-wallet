using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HubtelWallet.Application;
using HubtelWallet.Application.Interfaces;

namespace HubtelWallet.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IServiceManager _serviceManager;
        public BaseController(IServiceManager serviceManager) => 
            _serviceManager = serviceManager;
    }
}
