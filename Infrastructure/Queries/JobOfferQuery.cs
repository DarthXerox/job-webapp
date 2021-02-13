using System;
using System.Collections.Generic;
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

        public JobOfferQuery GetByIdWithQuestions(int id)
        {
            queryable = queryable
                .Where(jobOffer => jobOffer.Id == id)
                .Include(jobOffer => jobOffer.Questions)
                .Include(jobOffer => jobOffer.Company);
            return this;
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

        public JobOfferQuery FilterByCompanyId(int companyId)
        {
            queryable = queryable.Where(jobOffer => jobOffer.Company != null && jobOffer.Company.Id == companyId);
            return this;
        }

        public JobOfferQuery FilterBySkillTags(IList<string> skillTags)
        {
            if (skillTags == null || !skillTags.Any()) return this;

            string query = String.Format("SELECT * FROM dbo.JobOffers WHERE '{0}' IN (SELECT value FROM STRING_SPLIT(RelevantSkills, ';'))", skillTags[0]);
            if (skillTags.Count > 1)
            {
                foreach (var tag in skillTags.Skip(1))
                {
                    query += String.Format(" OR '{0}' IN (SELECT value FROM STRING_SPLIT(RelevantSkills, ';'))", tag);
                }
            }

            queryable = dbContext.Set<JobOffer>()
                .FromSqlRaw(query)
                .Include(jobOffer => jobOffer.Company);
            return this;
        }

        public JobOfferQuery FilterByCity(string city)
        {
            queryable = queryable.Where(jobOffer => jobOffer.City == city);
            return this;
        }
    }
}
