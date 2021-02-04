using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;


namespace Business.QueryObjects
{
    public class UserQueryObject
    {
        private readonly UnitOfWork unit;

        public UserQueryObject(UnitOfWork unitOfWork)
        {
            unit = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetByNameAsync(string userName, bool ascending = true)
        {
            return await unit.UserQuery
                .FilterByName(userName)
                .OrderBy(u => u.Id, ascending)
                .ExecuteAsync();
        }
    }
}
