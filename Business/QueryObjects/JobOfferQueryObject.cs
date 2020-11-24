using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BL;
using DAL.Entities;
using Infrastructure;

namespace Business.QueryObjects
{
    public class JobOfferQueryObject : QueryObject
    {
        public JobOfferQueryObject(UnitOfWork unit) : base(unit) { }

        public async Task<IEnumerable<JobOffer>> GetByNameAsync(string name, bool ascendingOrder = true)
        {
            return await UnitOfWork.JobOfferQuery
                .FilterByName(name)
                .OrderBy(jobOffer => jobOffer.Name, ascendingOrder)
                .ExecuteAsync();
        }

        public async Task<IEnumerable<JobOffer>> GetByNameContainsAsync(string name, bool ascendingOrder = true)
        {
            return await UnitOfWork.JobOfferQuery
                .FilterByNameContains(name)
                .OrderBy(jobOffer => jobOffer.Name, ascendingOrder)
                .ExecuteAsync()
            ;
        }

        public async Task<IEnumerable<JobOffer>> GetByCompanyNameAsync(string comapnyName,
            bool ascendingOrder = true)
        {
            return await UnitOfWork.JobOfferQuery
                .FilterByCompanyName(comapnyName)
                .OrderBy(jobOffer => jobOffer.Name, ascendingOrder)
                .ExecuteAsync()
            ;
        }

        public async Task<IEnumerable<JobOffer>> GetBySkillTagAsync(string skillTag, bool ascendingOrder = true)
        {
            return await UnitOfWork.JobOfferQuery
                .FilterBySkillTag(skillTag)
                .OrderBy(jobOffer => jobOffer.Name, ascendingOrder)
                .ExecuteAsync()
            ;
        }

        public async Task<IEnumerable<JobOffer>> GetByCityAsync(string city, bool ascendingOrder = true)
        {
            return await UnitOfWork.JobOfferQuery
                .FilterByCity(city)
                .OrderBy(jobOffer => jobOffer.Name, ascendingOrder)
                .ExecuteAsync()
            ;
        }
    }
}
