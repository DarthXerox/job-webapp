using Infrastructure;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTOs;
using Business.Services;

namespace Business.Facades
{
    public class UserFacade
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserService userService;

        public UserFacade(UnitOfWork unitOfWork, UserService userService, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userService = userService;
        }

        public async Task<UserShowDto> LoginUserAsync(UserLoginDto userLoginDto)
        {
            var loggedUser = await userService.AuthorizeUserAsync(userLoginDto.Name, userLoginDto.Password);
            if (loggedUser != null)
            {
                return mapper.Map<UserShowDto>(loggedUser);
            }
            throw new UnauthorizedAccessException();
        }

        public async Task<int> RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            int id = await userService.RegisterUserAsync(userRegisterDto.Name, userRegisterDto.Password, userRegisterDto.Role);
            await unitOfWork.SaveChangesAsync();
            return id;
        }

        public async Task<UserShowDto> GetByIdAsync(int id)
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(id);
            return mapper.Map<UserShowDto>(user);
        }
    }
}
