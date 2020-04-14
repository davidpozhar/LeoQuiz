using LeoQuiz.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeoQuiz.Core.Abstractions.Services
{
    public interface IUserService
    {
        public List<UserDto> GetAll();

        public Task<UserDto> GetById(int Id);

        public Task<UserDto> Insert(UserDto userDto);

        public UserDto Update(UserDto userDto);

        public Task Delete(int Id);
    }
}
