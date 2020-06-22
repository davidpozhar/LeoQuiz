using LeoQuiz.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeoQuiz.Core.Abstractions.Services
{
    public interface IUserService
    {
        public Task<List<UserDto>> GetAll();

        public List<UserDto> GetAllInterviewees(string adminId);

        public Task<UserDto> GetById(string Id);

        public Task<UserDto> Insert(UserDto userDto);

        public Task<UserDto> Update(UserDto userDto);

        public Task Delete(string Id);

    }
}
