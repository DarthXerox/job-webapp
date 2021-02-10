using System.Linq;
using DAL;
using DAL.Entities;

namespace Infrastructure.Queries
{
    public class UserQuery : Query<User>
    {
        public UserQuery(JobDbContext dbContext) : base(dbContext) { }

        public UserQuery FilterByName(string name)
        {
            queryable = queryable.Where(user => user.Name == name);
            return this;
        }

        public UserQuery FilterByNameContains(string name)
        {
            queryable = queryable.Where(user => user.Name != null && user.Name.Contains(name));
            return this;
        }
    }
}

