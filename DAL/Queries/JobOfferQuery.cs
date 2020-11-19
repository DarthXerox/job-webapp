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

        public void FilterByName(string name)
        {
            query = query.Where(jobOffer => jobOffer.Name == name);
        }

        public void FilterByNameContains(string name)
        {
            query = query.Where(jobOffer => jobOffer.Name.Contains(name));
        }

        public void FilterByCompanyName(string companyName)
        {
            query = query.Where(jobOffer => jobOffer.Company.Name == companyName);
        }

        public void FilterBySkillTag(string tag)
        {
            query = query.Where(jobOffer => jobOffer.RelevantSkills
                                                    .Select(jobOfferSkill => jobOfferSkill.Skill.Tag)
                                                    .Contains(tag));
        }

        public void FilterByCity(string city)
        {
            query = query.Where(jobOffer => jobOffer.City == city);
        }
    }
}
