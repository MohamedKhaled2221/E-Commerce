using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.IdentityModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
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
                "This is Token"
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
                "This is Token"
                );
        }
    } 
    #endregion
}
