using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.IdentityModule;
using Shared.Dtos.OrderModule;

namespace Presention.Controllers
{
    #region Part 11 Authentication Controller 

    #region Part 5 Authentication Controller 
    public class AuthenticationController(IServiceManager serviceManager) : ApiControllerBase
    {


        [HttpPost("Login")] //POST api/authentication/login
        public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
        => Ok(await serviceManager.AuthenticationService.LoginAsync(loginDto));


        [HttpPost("Register")] //POST api/authentication/Register

        public async Task<ActionResult<UserResultDto>> Register(RegisterDto registerDto)
      => Ok(await serviceManager.AuthenticationService.RegisterAsync(registerDto));

        [HttpGet("EmailExists")] //GET :  BaseUrl/api/authentication/EmailExists
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
         => Ok(await serviceManager.AuthenticationService.CheckEmailExists(email));

        [Authorize]
        [HttpGet] //GET :  BaseUrl/api/authentication
        public async Task<ActionResult<UserResultDto>> GetCurrentUser()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var userResult = await serviceManager.AuthenticationService.GetUserByEmail(email!);
            return Ok(userResult);
        }
        [Authorize]
        [HttpGet("Address")] //GET :  BaseUrl/api/authentication/Address
        public async Task<ActionResult<AddressDto>> GetAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var address = await serviceManager.AuthenticationService.GetUserAddress(email!);
            return Ok(address);
        }
        [Authorize]
        [HttpPut("Address")] //PUT :  BaseUrl/api/authentication/Address
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto addressDto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var updatedAddress = await serviceManager.AuthenticationService.UpdateUserAddress(addressDto, email!);
            return Ok(updatedAddress);
        }
    }
    #endregion 
    #endregion
}
