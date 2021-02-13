using System;
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
        private readonly UnitOfWork unitOfWork;
        private readonly JobOfferService jobOfferService;
        private readonly IMapper mapper;

        public JobOfferFacade(UnitOfWork unitOfWork, JobOfferService jobOfferService, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.jobOfferService = jobOfferService;
            this.mapper = mapper;
        }

        public async Task<(int totalCount, IEnumerable<JobOfferDto> offers)> GetAllAsync(int pageSize, int pageNumber, string? skillTag = null)
        {
            var jobOffers = mapper.Map<IEnumerable<JobOffer>, IEnumerable<JobOfferDto>>(await jobOfferService.GetAllAsync(pageSize, pageNumber, skillTag));
            int totalCount = await jobOfferService.GetTotalCountAsync(skillTag);
            return (totalCount, jobOffers);
        }

        public async Task<(int totalCount, IEnumerable<JobOfferDto> offers)> GetAllWithoutPagingAsync()
        {
            int totalCount = await jobOfferService.GetTotalCountAsync();
            var jobOffers = mapper.Map<IEnumerable<JobOffer>, IEnumerable<JobOfferDto>>(await jobOfferService
                .GetAllAsync(totalCount, 1));
            return (totalCount, jobOffers);
        }

        public async Task<IEnumerable<JobOfferDto>> GetByNameAsync(JobOfferDto jobOfferDto, bool ascendingOrder = true)
        {
            return mapper.Map<IEnumerable<JobOffer>, IEnumerable<JobOfferDto>>(await jobOfferService.GetByNameAsync(jobOfferDto.Name, ascendingOrder));
        }

        public async Task<IEnumerable<JobOfferDto>> GetByNameContainsAsync(JobOfferDto jobOfferDto, bool ascendingOrder = true)
        {
            return mapper.Map<IEnumerable<JobOffer>, IEnumerable<JobOfferDto>>(await jobOfferService.GetByNameContainsAsync(jobOfferDto.Name, ascendingOrder));
        }

        public async Task<IEnumerable<JobOfferDto>> GetByCompanyNameAsync(JobOfferDto jobOfferDto, bool ascendingOrder = true)
        {
            return mapper.Map<IEnumerable<JobOffer>, IEnumerable<JobOfferDto>>(await jobOfferService.GetByCompanyNameAsync(jobOfferDto.Company?.Name, ascendingOrder));
        }

        public async Task<IEnumerable<JobOfferDto>> GetByCompanyIdAsync(int companyId, bool ascendingOrder = true)
        {
            return mapper.Map<IEnumerable<JobOffer>, IEnumerable<JobOfferDto>>(await jobOfferService.GetByCompanyIdAsync(companyId, ascendingOrder));
        }

        public async Task<IEnumerable<JobOfferDto>> GetByCityAsync(string city, bool ascendingOrder = true)
        {
            return mapper.Map<IEnumerable<JobOffer>, IEnumerable<JobOfferDto>>(await jobOfferService.GetByCityAsync(city, ascendingOrder));
        }

        public async Task<JobOfferDto> GetByIdAsync(int id)
        {
            return mapper.Map<JobOfferDto>(await jobOfferService.GetByIdAsync(id));
        }

        public async Task<JobOfferDto> GetByIdWithQuestionsAsync(int id)
        {
            return mapper.Map<JobOfferDto>(await jobOfferService.GetByIdWithQuestionsAsync(id));
        }

        public async Task CreateAsync(JobOfferDto jobOfferDto)
        {
            await jobOfferService.CreateAsync(mapper.Map<JobOffer>(jobOfferDto));
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(JobOfferDto jobOffer)
        {
            await jobOfferService.UpdateAsync(mapper.Map<JobOfferDto, JobOffer>(jobOffer));
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(JobOfferDto jobOfferDto)
        {
            await jobOfferService.DeleteAsync(jobOfferDto.Id ?? throw new NullReferenceException("JobOfferDto.Id can't be null!"));
            await unitOfWork.SaveChangesAsync();
        }
    }
}
