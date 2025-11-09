using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.IdentityModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction.Contracts;
using Shared.Dtos.IdentityModule;
using ValidationException = Domain.Exceptions.ValidationException;

namespace Services.Implementations
{
    #region Part 4 Authentication Service
    internal class AuthenticationService(UserManager<User> _userManager) : IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            // Check if User Exists Under given Email
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) throw new UnAuthorizedException($"Email {loginDto.Email} is not Exist");

            // Check If Password is Correct
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result) throw new UnAuthorizedException();
            // Return User & Create Token
            return new UserResultDto(
                user.DisplayName,
                user.Email!,
               await CreateTokenAsync(user)
                );
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
            return new UserResultDto(
                user.DisplayName,
                user.Email!,
               await CreateTokenAsync(user)
                );
        }

        #region part 6 Jwt , Authentication Service Create Token
        // Method For Generate Token
        private async Task<string> CreateTokenAsync(User user)
        {
            // Craete claims for User 
            var authclaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.DisplayName),
                new Claim(ClaimTypes.Email, user.Email!)
            };
            // Add Roles To Claims If Exists
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                authclaims.Add(new Claim(ClaimTypes.Role, role));
            // Create Security Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
            ("a5bc82e5dfaa1c363a6e0f558fcd23e0fb311598d835dee8de9f20b0f820668d"));

            // Create Alg
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // return Token
            var token = new JwtSecurityToken(
                issuer: "BaseUrl", // Backend URL
                audience: "AngularProj",
                claims: authclaims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        } 
        #endregion
    }
    #endregion
   

}
