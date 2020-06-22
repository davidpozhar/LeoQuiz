using LeoQuiz.Core.Abstractions.Services;
using LeoQuiz.Core.Dto;
using LeoQuiz.Core.Entities;
using LeoQuiz.Services.Extentions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LeoQuiz.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager; 
        private readonly RoleManager<IdentityRole> _roleManager;
        readonly IConfiguration _configuration;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._configuration = configuration;
        }

        public async Task<string> SignUp(UserRegisterDto dto)
        {
            var roleCheck = await _roleManager.RoleExistsAsync("Admin").ConfigureAwait(false);
            if (!roleCheck)
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin")).ConfigureAwait(false);
            }

            await IsEmalInUse(dto.Email).ConfigureAwait(false);

            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                Name = dto.Name,
                Surname = dto.Surname,
                Age = dto.Age,
                UserRoleId = 1
            };

            var result = await _userManager.CreateAsync(user, dto.Password).ConfigureAwait(false);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);
                var userEntity = await _userManager.FindByEmailAsync(dto.Email).ConfigureAwait(false);

                await _userManager.AddToRoleAsync(user, "Admin").ConfigureAwait(false);

                return GetToken(userEntity);
            }
            return null;
        }

        public async Task<string> SignIn(UserLoginDto dto)
        {
            await IsEmailNull(dto.Email).ConfigureAwait(false);

            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, true, false).ConfigureAwait(false);

            if (result.Succeeded)
            {
                var userEntity = await _userManager.FindByEmailAsync(dto.Email).ConfigureAwait(false);

                return GetToken(userEntity);
            }

            return null;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);

        }

        public async Task<string> RefreshToken(string name)
        {
            var user = await _userManager.FindByNameAsync(name).ConfigureAwait(false);
            return GetToken(user);
        }

        private async Task IsEmalInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);

            if (user != null)
            {
                throw new FormatException("Email is in use.");

            }
        }

        private async Task IsEmailNull(string email)
        {
            var user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);

            if (user == null)
            {
                throw new FormatException("There is no email");

            }
        }

        private string GetToken(IdentityUser user)
        {
            var utcNow = DateTime.UtcNow;

            var claims = new Claim[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("marvel marvel marvel marvel marvel marvel marvel marvel marvel marvel marvel marvel marvel marvel"));

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(this._configuration.GetValue<int>("Tokens:Lifetime")),
                audience: this._configuration.GetValue<String>("Tokens:Audience"),
                issuer: this._configuration.GetValue<String>("Tokens:Issuer")
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
