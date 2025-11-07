using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.IdentityModule;

namespace Services.Abstraction.Contracts
{
    public interface IAuthenticationService
    {
        // LOGIN & REGISTER
        public Task<UserResultDto> LoginAsync(LoginDto loginDto);
        public Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
    }
}
