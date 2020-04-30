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
using System.Threading.Tasks;

namespace LeoQuiz.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager; 
        readonly IConfiguration _configuration;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
        }

        public async Task<string> SignUp(UserRegisterDto dto)
        {
            await IsEmalInUse(dto.Email);

            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                Name = dto.Name,
                Surname = dto.Surname,
                UserRoleId = dto.UserRoleId,
                Age = dto.Age
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                var userEntity = await _userManager.FindByEmailAsync(dto.Email);

                await _userManager.AddToRoleAsync(user, "Admin");

                return GetToken(userEntity);
            }
            return null;
        }

        public async Task<string> SignIn(UserLoginDto dto)
        {
            await IsEmailNull(dto.Email);

            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, true, false);

            if (!result.Succeeded)
            {
                var userEntity = await _userManager.FindByEmailAsync(dto.Email);

                return GetToken(userEntity);
            }

            return null;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();

        }

        public async Task<string> RefreshToken(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            return GetToken(user);
        }

        private async Task IsEmalInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                throw new FormatException("Email is in use.");

            }
        }

        private async Task IsEmailNull(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new FormatException("There is no email");

            }
        }

        private string GetToken(IdentityUser user)
        {
            var utcNow = DateTime.UtcNow;

            using (RSA privateRsa = RSA.Create())
            {
                privateRsa.FromXmlFile(Path.Combine(Directory.GetCurrentDirectory(),
                                "Keys",
                                 this._configuration.GetValue<String>("Tokens:PrivateKey")
                                 ));
                var privateKey = new RsaSecurityKey(privateRsa);
                SigningCredentials signingCredentials = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256);


                var claims = new Claim[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
                };

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
}
