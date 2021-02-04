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
            queryable = queryable.Where(company => company.Name == name);
            return this;
        }
    }
}
