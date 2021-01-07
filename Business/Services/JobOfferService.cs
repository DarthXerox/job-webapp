using System.Collections.Generic;
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
            return await jobOfferQueryObject.GetByNameContainsAsync(companyName, ascendingOrder);
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

        public async Task CreateAsync(JobOffer jobOffer)
        {
            unitOfWork.JobOfferRepository.Add(jobOffer);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(JobOffer jobOffer)
        {
            unitOfWork.JobOfferRepository.Update(jobOffer);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int jobOfferId)
        {
            unitOfWork.JobOfferRepository.Delete(jobOfferId);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
