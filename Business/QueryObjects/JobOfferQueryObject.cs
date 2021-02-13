using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using Infrastructure;

namespace Business.QueryObjects
{
    public class JobOfferQueryObject : QueryObject
    {
        public JobOfferQueryObject(UnitOfWork unit) : base(unit) { }

        public async Task<IEnumerable<JobOffer>> GetAllAsync(int pageSize, int pageNumber, string? skillTag)
        {
            return await UnitOfWork.JobOfferQuery
                .FilterBySkillTag(skillTag)
                .OrderBy(keySelector: jobOffer => jobOffer.Name)
                .Page(pageSize, pageNumber)
                .ExecuteAsync();
        }

        public async Task<IEnumerable<JobOffer>> GetByNameAsync(string name, bool ascendingOrder = true)
        {
            return await UnitOfWork.JobOfferQuery
                .FilterByName(name)
                .OrderBy(keySelector: jobOffer => jobOffer.Name, ascendingOrder)
                .ExecuteAsync();
        }

        public async Task<IEnumerable<JobOffer>> GetByNameContainsAsync(string name, bool ascendingOrder = true)
        {
            return await UnitOfWork.JobOfferQuery
                .FilterByNameContains(name)
                .OrderBy(keySelector: jobOffer => jobOffer.Name, ascendingOrder)
                .ExecuteAsync();
        }

        public async Task<IEnumerable<JobOffer>> GetByCompanyNameAsync(string companyName,
            bool ascendingOrder = true)
        {
            return await UnitOfWork.JobOfferQuery
                .FilterByCompanyName(companyName)
                .OrderBy(keySelector: jobOffer => jobOffer.Name, ascendingOrder)
                .ExecuteAsync();
        }

        public async Task<IEnumerable<JobOffer>> GetByCompanyIdAsync(int companyId,
            bool ascendingOrder = true)
        {
            return await UnitOfWork.JobOfferQuery
                .FilterByCompanyId(companyId)
                .OrderBy(keySelector: jobOffer => jobOffer.Name, ascendingOrder)
                .ExecuteAsync();
        }

        public async Task<IEnumerable<JobOffer>> GetByCityAsync(string city, bool ascendingOrder = true)
        {
            return await UnitOfWork.JobOfferQuery
                .FilterByCity(city)
                .OrderBy(keySelector: jobOffer => jobOffer.Name, ascendingOrder)
                .ExecuteAsync();
        }

        public async Task<int> GetTotalCountAsync(string? skillTag)
        {
            if (skillTag == null) return await UnitOfWork.JobOfferRepository.GetTotalCountAsync();

            return (await UnitOfWork.JobOfferQuery
                .FilterBySkillTag(skillTag)
                .ExecuteAsync()).Count();
        }

        public async Task<IEnumerable<JobOffer>> GetByIdWithQuestionsAsync(int id)
        {
            return await UnitOfWork.JobOfferQuery.GetByIdWithQuestions(id).ExecuteAsync(); ;
        }
    }
}
