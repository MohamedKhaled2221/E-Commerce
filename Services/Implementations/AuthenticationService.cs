using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.IdentityModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction.Contracts;
using Shared;
using Shared.Dtos.IdentityModule;
using Shared.Dtos.OrderModule;
using ValidationException = Domain.Exceptions.ValidationException;

namespace Services.Implementations
{
    #region Part 4 Authentication Service
    public class AuthenticationService(UserManager<User> _userManager,
        IOptions<JwtOptions> options, IMapper mapper) : IAuthenticationService
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
            var jwtOptions = options.Value;
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
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));


            // Create Alg
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // return Token
            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer, // Backend URL
                audience: jwtOptions.Audience,
                claims: authclaims,
                expires: DateTime.UtcNow.AddDays(jwtOptions.DurationInDays),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        #endregion
        #region Part 10 Authentication Service {Get Address , Get User , Check Email }
        public async Task<bool> CheckEmailExists(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null; // Value != null  => true
        }

        public async Task<AddressDto> GetUserAddress(string email)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                 .FirstOrDefaultAsync(u => u.Email == email)
                   ?? throw new UserNotFoundException(email);
            return mapper.Map<AddressDto>(user.Address);
        }

        public async Task<UserResultDto> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new UserNotFoundException(email);
            return new UserResultDto(
                user.DisplayName,
                user.Email!,
               await CreateTokenAsync(user)
                );
        }
        public async Task<AddressDto> UpdateUserAddress(AddressDto address, string email)
        {
            var user = await _userManager.Users.Include(u => u.Address)
               .FirstOrDefaultAsync(u => u.Email == email)
                 ?? throw new UserNotFoundException(email);
            // User.address ==> null ==> Create Address 

            if (user.Address != null) // Update Address
            {
                user.Address.FirstName = address.FirstName;
                user.Address.LastName = address.LastName;
                user.Address.Street = address.Street;
                user.Address.City = address.City;
                user.Address.Country = address.Country;
                await _userManager.UpdateAsync(user);
            }
            else // Create Address
            {
                var useraddress = mapper.Map<UserAddress>(address);
                user.Address = useraddress;
            }
            await _userManager.UpdateAsync(user);
            return mapper.Map<AddressDto>(user.Address);
        }

        #endregion
    }
    #endregion


}
