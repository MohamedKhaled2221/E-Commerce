using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.IdentityModule;
using Shared.Dtos.OrderModule;

namespace Services.Abstraction.Contracts
{
    public interface IAuthenticationService
    {
        // LOGIN & REGISTER
        public Task<UserResultDto> LoginAsync(LoginDto loginDto);
        public Task<UserResultDto> RegisterAsync(RegisterDto registerDto);

        // Get Current User
        public Task<UserResultDto> GetUserByEmail(string email);

        // Check if Email Exists or not 
        public Task<bool> CheckEmailExists(string email);
        // Get User Address
        public Task<AddressDto> GetUserAddress(string email);
        // Update User Address
        public Task<AddressDto> UpdateUserAddress( AddressDto address, string email);
    }
}
