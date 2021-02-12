using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.QueryObjects;
using DAL.Entities;
using Infrastructure;

namespace Business.Services
{
    public class JobOfferService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly JobOfferQueryObject jobOfferQueryObject;

        public JobOfferService(UnitOfWork unitOfWork, JobOfferQueryObject jobOfferQueryObject)
        {
            this.unitOfWork = unitOfWork;
            this.jobOfferQueryObject = jobOfferQueryObject;
        }

        public async Task<IEnumerable<JobOffer>> GetAllAsync(int pageSize, int pageNumber)
        {
            return await jobOfferQueryObject.GetAllAsync(pageSize, pageNumber);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await unitOfWork.JobOfferRepository.GetTotalCountAsync();
        }

        public async Task<IEnumerable<JobOffer>> GetByNameAsync(string name, bool ascendingOrder = true)
        {
            return await jobOfferQueryObject.GetByNameAsync(name, ascendingOrder);
        }

        public async Task<IEnumerable<JobOffer>> GetByNameContainsAsync(string name, bool ascendingOrder = true)
        {
            return await jobOfferQueryObject.GetByNameContainsAsync(name, ascendingOrder);
        }

        public async Task<IEnumerable<JobOffer>> GetByCompanyNameAsync(string companyName, bool ascendingOrder = true)
        {
            return await jobOfferQueryObject.GetByCompanyNameAsync(companyName, ascendingOrder);
        }

        public async Task<IEnumerable<JobOffer>> GetByCompanyIdAsync(int companyId, bool ascendingOrder = true)
        {
            return await jobOfferQueryObject.GetByCompanyIdAsync(companyId, ascendingOrder);
        }

        public async Task<IEnumerable<JobOffer>> GetBySkillTagAsync(string skillTag, bool ascendingOrder = true)
        {
            return await jobOfferQueryObject.GetBySkillTagAsync(skillTag, ascendingOrder);
        }

        public async Task<IEnumerable<JobOffer>> GetByCityAsync(string city, bool ascendingOrder = true)
        {
            return await jobOfferQueryObject.GetByCityAsync(city, ascendingOrder);
        }

        public async Task<JobOffer> GetByIdAsync(int id)
        {
            return await unitOfWork.JobOfferRepository.GetByIdAsync(id);
        }

        public async Task<JobOffer> GetByIdWithQuestionsAsync(int id)
        {
            return (await jobOfferQueryObject.GetByIdWithQuestionsAsync(id)).First();
        }

        public async Task CreateAsync(JobOffer jobOffer)
        {
            unitOfWork.JobOfferRepository.Add(jobOffer);
        }

        public async Task UpdateAsync(JobOffer jobOffer)
        {
            unitOfWork.JobOfferRepository.Update(jobOffer);
        }

        public async Task DeleteAsync(int jobOfferId)
        {
            unitOfWork.JobOfferRepository.Delete(jobOfferId);
        }
    }
}
