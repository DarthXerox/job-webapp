using System.Linq;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class CompanyQuery : Query<Company>
    {
        public CompanyQuery(DbContext dbContext) : base(dbContext) { }

        public CompanyQuery FilterByName(string name)
        {
            Queryable = Queryable.Where(company => company.Name == name);
            return this;
        }

        public CompanyQuery FilterByNameContains(string name)
        {
            Queryable = Queryable.Where(company => company.Name.Contains(name));
            return this;
        }
    }
}
