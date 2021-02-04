using AutoMapper;
using Bussiness.Dto;
using Bussiness.QueryObjects;
using Infrastructure;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ZooDAL.Models;

namespace Bussiness.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserQueryObject _UserByNameQueryObject;

        private const int PBKDF2IterCount = 100000;
        private const int PBKDF2SubkeyLength = 160 / 8;
        private const int saltSize = 128 / 8;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserQueryObject uquery)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _UserByNameQueryObject = uquery;
        }


        public void Create(UserCreateDto user)
        {
            _unitOfWork.UserRepository.Create(_mapper.Map<User>(user));
        }
        public async Task<UserShowDto> GetUserAccordingToNameAsync(string name)
        {
            var result = await _UserByNameQueryObject.ExecuteAsync(name);
            return result;
        }
        public async Task<UserShowDto> AuthorizeUserAsync(UserLoginDto login)
        {
            var userDto = await GetUserAccordingToNameAsync(login.UserName);
            if (userDto == null)
            {
                return null;
            }

            //get user entity
            var user = _unitOfWork.UserRepository.Get(userDto.Id);

            var (hash, salt) = user != null ? GetPassAndSalt(user.PasswordHash) : (string.Empty, string.Empty);

            var succ = user != null && VerifyHashedPassword(hash, salt, login.Password);
            return succ ? userDto : null;
        }

        public void RegisterUser(UserCreateDto user)
        {
            var (hash, salt) = CreateHash(user.Password);
            user.PasswordHash = string.Join(',', hash, salt);

            Create(user);

        }

        private (string, string) GetPassAndSalt(string passwordHash)
        {
            var result = passwordHash.Split(',');
            if (result.Count() != 2)
            {
                return (string.Empty, string.Empty);
            }
            return (result[0], result[1]);
        }

        private bool VerifyHashedPassword(string hashedPassword, string salt, string password)
        {
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            var saltBytes = Convert.FromBase64String(salt);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, PBKDF2IterCount))
            {
                var generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                return hashedPasswordBytes.SequenceEqual(generatedSubkey);
            }
        }

        private Tuple<string, string> CreateHash(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                return Tuple.Create(Convert.ToBase64String(subkey), Convert.ToBase64String(salt));
            }
        }
    }
}

