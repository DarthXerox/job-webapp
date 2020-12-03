using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTOs;
using Business.QueryObjects;
using Business.Services;
using DAL.Entities;
using Infrastructure;

namespace Business.Facades
{
    public class JobOfferFacade
    {
        private readonly JobOfferService jobOfferService;
        private readonly IMapper mapper;

        public JobOfferFacade(JobOfferService jobOfferService, IMapper mapper)
        {
            this.jobOfferService = jobOfferService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<JobOfferDto>> GetByNameAsync(string name, bool ascendingOrder = true)
        {
            return mapper.Map<IEnumerable<JobOffer>, IEnumerable<JobOfferDto>>(await jobOfferService.GetByNameAsync(name, ascendingOrder));
        }

        public async Task<IEnumerable<JobOfferDto>> GetByNameContainsAsync(string name, bool ascendingOrder = true)
        {
            return mapper.Map<IEnumerable<JobOffer>, IEnumerable<JobOfferDto>>(await jobOfferService.GetByNameContainsAsync(name, ascendingOrder));
        }

        public async Task<IEnumerable<JobOfferDto>> GetByCompanyNameAsync(string companyName, bool ascendingOrder = true)
        {
            return mapper.Map<IEnumerable<JobOffer>, IEnumerable<JobOfferDto>>(await jobOfferService.GetByNameContainsAsync(companyName, ascendingOrder));
        }

        public async Task<IEnumerable<JobOfferDto>> GetBySkillTagAsync(string skillTag, bool ascendingOrder = true)
        {
            return mapper.Map<IEnumerable<JobOffer>, IEnumerable<JobOfferDto>>(await jobOfferService.GetBySkillTagAsync(skillTag, ascendingOrder));
        }

        public async Task<IEnumerable<JobOfferDto>> GetByCityAsync(string city, bool ascendingOrder = true)
        {
            return mapper.Map<IEnumerable<JobOffer>, IEnumerable<JobOfferDto>>(await jobOfferService.GetByCityAsync(city, ascendingOrder));
        }

        public async Task CreateAsync(string name, string city, Company company, string description, ICollection<string> relevantSkills, ICollection<string> questions)
        {
            await jobOfferService.CreateAsync(name, city, company, description, relevantSkills, questions);
        }

        public async Task Update(JobOfferDto jobOffer)
        {
            await jobOfferService.UpdateAsync(mapper.Map<JobOfferDto, JobOffer>(jobOffer));
        }

        public async Task Delete(int jobOfferId)
        {
            await jobOfferService.DeleteAsync(jobOfferId);
        }
    }
}
