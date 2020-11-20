using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using System.Linq;

namespace DAL.Queries
{
    public class JobOfferQuery : Query<JobOffer>
    {
        public JobOfferQuery(JobDbContext dbContext) : base(dbContext) { }

        public JobOfferQuery FilterByName(string name)
        {
            query = query.Where(jobOffer => jobOffer.Name == name);
            return this;
        }

        public JobOfferQuery FilterByNameContains(string name)
        {
            query = query.Where(jobOffer => jobOffer.Name.Contains(name));
            return this;
        }

        public JobOfferQuery FilterByCompanyName(string companyName)
        {
            query = query.Where(jobOffer => jobOffer.Company.Name == companyName);
            return this;
        }

        public JobOfferQuery FilterBySkillTag(string tag)
        {
            query = query.Where(jobOffer => jobOffer.RelevantSkills
                                                    .Select(jobOfferSkill => jobOfferSkill.Skill.Tag)
                                                    .Contains(tag));
            return this;
        }

        public JobOfferQuery FilterByCity(string city)
        {
            query = query.Where(jobOffer => jobOffer.City == city);
            return this;
        }
    }
}
