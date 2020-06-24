using AutoMapper;
using LeoQuiz.Core.Abstractions.Repositories;
using LeoQuiz.Core.Abstractions.Services;
using LeoQuiz.Core.Dto;
using LeoQuiz.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeoQuiz.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;

        public UserService(IMapper mapper, IUserRepository userRepository, UserManager<User> userManager)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
            this._userManager = userManager;
        }

        public async Task<List<UserDto>> GetAll()
        {
            return await _userRepository.GetAll().Select(el =>_mapper.Map<User, UserDto>(el)).ToListAsync().ConfigureAwait(false);
        }

        public async Task<UserDto> GetById(string Id)
        {
            var entity = await _userRepository.GetAll().Where(user => user.Email == Id).FirstOrDefaultAsync().ConfigureAwait(false);
            return _mapper.Map<User, UserDto>(entity);
        }

        public async Task<UserDto> Insert(UserDto userDto)
        {
            var entity = new User();
            _mapper.Map(userDto, entity);
            await _userRepository.Insert(entity).ConfigureAwait(false);
            await _userRepository.SaveAsync().ConfigureAwait(false);
            return _mapper.Map<User, UserDto>(entity);
        }

        public async Task<UserDto> Update(UserDto userDto)
        {
            var entity = new User();
            _mapper.Map(userDto, entity);
            _userRepository.Update(entity);
            await _userRepository.SaveAsync().ConfigureAwait(false);
            return _mapper.Map<User, UserDto>(entity);
        }

        public async Task Delete(string Id)
        {
            await _userRepository.Delete(Id).ConfigureAwait(false);
            await _userRepository.SaveAsync().ConfigureAwait(false);
        }

        public List<UserDto> GetAllInterviewees(string adminId)
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
                    userList.Add(_mapper.Map<User, UserDto>(passquiz.User));
                }
            }

            return userList;
        }
    }
}
