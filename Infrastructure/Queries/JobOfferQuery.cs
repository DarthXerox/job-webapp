using System.Linq;
using DAL;
using DAL.Entities;

namespace Infrastructure.Queries
{
    public class JobOfferQuery : Query<JobOffer>
    {
        public JobOfferQuery(JobDbContext dbContext) : base(dbContext) { }

        public JobOfferQuery FilterByName(string name)
        {
            Queryable = Queryable.Where(jobOffer => jobOffer.Name == name);
            return this;
        }

        public JobOfferQuery FilterByNameContains(string name)
        {
            Queryable = Queryable.Where(jobOffer => jobOffer.Name.Contains(name));
            return this;
        }

        public JobOfferQuery FilterByCompanyName(string companyName)
        {
            Queryable = Queryable.Where(jobOffer => jobOffer.Company.Name == companyName);
            return this;
        }

        public JobOfferQuery FilterBySkillTag(string tag)
        {
            Queryable = Queryable.Where(jobOffer => jobOffer.RelevantSkills
                .Select(jobOfferSkill => jobOfferSkill.Skill.Tag)
                .Contains(tag));
            return this;
        }

        public JobOfferQuery FilterByCity(string city)
        {
            Queryable = Queryable.Where(jobOffer => jobOffer.City == city);
            return this;
        }
    }
}
