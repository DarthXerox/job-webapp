using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;


namespace Business.QueryObjects
{
    public class UserQueryObject : QueryObject
    {
        public UserQueryObject(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IEnumerable<User>> GetByNameAsync(string userName, bool ascending = true)
        {
            return await UnitOfWork.UserQuery
                .FilterByName(userName)
                .OrderBy(u => u.Id, ascending)
                .ExecuteAsync();
        }
    }
}
