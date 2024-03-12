using CleanSample.Framework.Domain.Identity;
using CleanSample.Framework.Domain.Identity.Models;
using CleanSample.Framework.Domain.Results;
using Microsoft.AspNetCore.Mvc;

namespace CleanSample.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        [HttpPost(Name = "RegisterUser")]
        public async Task<Result> RegisterUser(RegisterModel registerModel)
        {
           return await _identityService.Register(registerModel);
        }
    }
}
