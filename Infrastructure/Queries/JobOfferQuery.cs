using System.Linq;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class JobOfferQuery : Query<JobOffer>
    {
        public JobOfferQuery(JobDbContext dbContext) : base(dbContext)
        {
            queryable = queryable.Include(jobOffer => jobOffer.Company);
        }

        public JobOfferQuery FilterByName(string name)
        {
            queryable = queryable.Where(jobOffer => jobOffer.Name == name);
            return this;
        }

        public JobOfferQuery FilterByNameContains(string name)
        {
            queryable = queryable.Where(jobOffer => jobOffer.Name != null && jobOffer.Name.Contains(name));
            return this;
        }

        public JobOfferQuery FilterByCompanyName(string companyName)
        {
            queryable = queryable.Where(jobOffer => jobOffer.Company != null && jobOffer.Company.Name == companyName);
            return this;
        }

        public JobOfferQuery FilterBySkillTag(string tag)
        {
            queryable = queryable.Where(jobOffer => jobOffer.RelevantSkills != null && jobOffer.RelevantSkills.Contains(tag));
            return this;
        }

        public JobOfferQuery FilterByCity(string city)
        {
            queryable = queryable.Where(jobOffer => jobOffer.City == city);
            return this;
        }
    }
}
