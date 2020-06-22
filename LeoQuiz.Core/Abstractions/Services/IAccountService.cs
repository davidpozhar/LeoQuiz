using LeoQuiz.Core.Dto;
using System.Threading.Tasks;

namespace LeoQuiz.Core.Abstractions.Services
{
    public interface IAccountService
    {
        public Task<string> SignUp(UserRegisterDto dto);

        public Task<string> SignIn(UserLoginDto dto);

        public Task Logout();

        public Task<string> RefreshToken(string name);
    }
}
