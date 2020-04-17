using LeoQuiz.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeoQuiz.Core.Abstractions.Services
{
    public interface IUserService
    {
        public Task<List<UserDto>> GetAll();

        public List<UserDto> GetAllInterviewees(int adminId);

        public Task<UserDto> GetById(int Id);

        public Task<UserDto> Insert(UserDto userDto);

        public Task<UserDto> Update(UserDto userDto);

        public Task Delete(int Id);

    }
}
