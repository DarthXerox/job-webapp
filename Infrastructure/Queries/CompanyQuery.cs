using System.Linq;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class CompanyQuery : Query<Company>
    {
        public CompanyQuery(JobDbContext dbContext) : base(dbContext) { }

        public CompanyQuery FilterByName(string name)
        {
            queryable = queryable.Where(company => company.Name == name);
            return this;
        }

        public CompanyQuery FilterByNameContains(string name)
        {
            queryable = queryable.Where(company => company.Name != null && company.Name.Contains(name));
            return this;
        }
    }
}
