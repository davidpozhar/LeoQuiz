using AutoMapper;
using LeoQuiz.Core.Abstractions.Repositories;
using LeoQuiz.Core.Abstractions.Services;
using LeoQuiz.Core.Dto;
using LeoQuiz.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeoQuiz.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
        }

        public List<UserDto> GetAll()
        {
            return _userRepository.GetAll().Select(el => _mapper.Map(el, new UserDto())).ToList();
        }

        public async Task<UserDto> GetById(int Id)
        {
            var entity = await _userRepository.GetById(Id);
            var dto = new UserDto();
            _mapper.Map(entity, dto);
            return dto;
        }

        public async Task<UserDto> Insert(UserDto userDto)
        {
            var entity = new User();
            _mapper.Map(userDto, entity);
            await _userRepository.Insert(entity);
            await _userRepository.SaveAsync();
            _mapper.Map(entity, userDto);
            return userDto;
        }

        public UserDto Update(UserDto userDto)
        {
            var entity = new User();
            _mapper.Map(userDto, entity);
            _userRepository.Update(entity);
            _userRepository.Save();
            _mapper.Map(entity, userDto);
            return userDto;
        }

        public async Task Delete(int Id)
        {
            await _userRepository.Delete(Id);
            await _userRepository.SaveAsync();
        }

        public List<UserDto> GetAllInterviewees(int adminId)
        {
            var users = _userRepository.GetAll()
                .Include(admin => admin.Quizzes)
                .ThenInclude(quizzes => quizzes)
                .ThenInclude(x=>x)//????
                .Where(admin=>admin.Id == adminId)
                .ToList();

            return users;
        }
    }
}
