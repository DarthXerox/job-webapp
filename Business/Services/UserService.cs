using Infrastructure;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Business.QueryObjects;
using DAL.Entities;
using DAL.Enums;

namespace Business.Services
{
    public class UserService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly UserQueryObject userQueryObject;

        private const int PBKDF2IterCount = 100000;
        private const int PBKDF2SubkeyLength = 160 / 8;
        private const int saltSize = 128 / 8;


        public UserService(UnitOfWork unitOfWork, UserQueryObject uquery)
        {
            this.unitOfWork = unitOfWork;
            userQueryObject = uquery;
        }


        public async Task<User?> AuthorizeUserAsync(string userName, string password)
        {
            var lol = unitOfWork.CompanyQuery.FilterByName("Apple").ExecuteAsync().Result;
            //var loggedUser = await GetUserByName(userName);
            Console.WriteLine(userName + " " + password);
            //var temp = unitOfWork.UserQuery.ExecuteAsync().Result.Where(u => u.Name == userName);
            var x = unitOfWork.UserQuery.FilterByName(userName).ExecuteAsync().Result;
            var temp = await unitOfWork.UserQuery.FilterByName(userName).ExecuteAsync();
            var loggedUser = temp.First();
            if (loggedUser == null)
            {
                return null;
            }

            var (hash, salt) = GetPassAndSalt(loggedUser.PasswordHash);
            return VerifyHashedPassword(hash, salt, password)
                ? loggedUser
                : null;
        }


        public int RegisterUser(string userName, string password, Roles role)
        {
            var (hash, salt) = CreateHash(password);
            var user = new User
            {
                Name = userName,
                PasswordHash = string.Join(',', hash, salt),
                Role = role
            };
            unitOfWork.UserRepository.Add(user);
            unitOfWork.SaveChanges();
            return user.Id.Value;
        }


        private (string, string) GetPassAndSalt(string passwordHash)
        {
            var result = passwordHash.Split(',');
            return result.Count() != 2
                ? (string.Empty, string.Empty)
                : (result[0], result[1]);
        }


        private async Task<User> GetUserByName(string name) => (await userQueryObject.GetByNameAsync(name)).First();


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


        public static Tuple<string, string> CreateHash(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                return Tuple.Create(Convert.ToBase64String(subkey), Convert.ToBase64String(salt));
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await unitOfWork.UserRepository.GetByIdAsync(id);
        }
    }
}

