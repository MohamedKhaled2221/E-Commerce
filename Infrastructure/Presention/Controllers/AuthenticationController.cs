using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.IdentityModule;

namespace Presention.Controllers
{
    #region Part 5 Authentication Controller 
    public class AuthenticationController(IServiceManager serviceManager) : ApiControllerBase
    {


        [HttpPost("Login")] //POST api/authentication/login
        public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
        => Ok(await serviceManager.AuthenticationService.LoginAsync(loginDto));


        [HttpPost("Register")] //POST api/authentication/Register

        public async Task<ActionResult<UserResultDto>> Register(RegisterDto registerDto)
      => Ok(await serviceManager.AuthenticationService.RegisterAsync(registerDto));
    } 
    #endregion
}
