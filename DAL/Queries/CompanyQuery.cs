using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Entities;

namespace DAL.Queries
{
    public class CompanyQuery : Query<Company>
    {
        public CompanyQuery(JobDbContext dbContext) : base(dbContext) { }

        public void FilterByName(string name)
        {
            query = query.Where(company => company.Name == name);
        }

    }
}
