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

        public async Task<List<UserDto>> GetAll()
        {
            return await _userRepository.GetAll().Select(el => _mapper.Map(el, new UserDto())).ToListAsync();
        }

        public async Task<UserDto> GetById(int Id)
        {
            var entity = await _userRepository.GetById(Id);
            return _mapper.Map(entity, new UserDto());
        }

        public async Task<UserDto> Insert(UserDto userDto)
        {
            var entity = new User();
            _mapper.Map(userDto, entity);
            await _userRepository.Insert(entity);
            await _userRepository.SaveAsync();
            return _mapper.Map(entity, userDto);
        }

        public async Task<UserDto> Update(UserDto userDto)
        {
            var entity = new User();
            _mapper.Map(userDto, entity);
            _userRepository.Update(entity);
            await _userRepository.SaveAsync();
            return _mapper.Map(entity, userDto);
        }

        public async Task Delete(int Id)
        {
            await _userRepository.Delete(Id);
            await _userRepository.SaveAsync();
        }

        //Костиль, голова не варить, виправити!!!!!
        public List<UserDto> GetAllInterviewees(int adminId)
        {
            var adminInfo =  _userRepository.GetAll()
                .Include(admin => admin.Quizzes)
                .ThenInclude(quiz => quiz.PassedQuizzes)
                .ThenInclude(passquiz => passquiz.User)
                .Where(admin =>admin.Id == adminId).FirstOrDefault();

            var userList = new List<UserDto>();

            foreach(var quiz in adminInfo.Quizzes)
            {
                foreach(var passquiz in quiz.PassedQuizzes)
                {
                    userList.Add(_mapper.Map(passquiz.User, new UserDto()));
                }
            }

            return userList;
        }
    }
}
