using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTOs;
using Business.Services;
using DAL.Entities;
using DAL.Enums;
using Infrastructure;

namespace Business.Facades
{
    public class JobApplicationFacade
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly JobApplicationService jobApplicationService;

        public JobApplicationFacade(UnitOfWork unitOfWork, IMapper mapper, JobApplicationService jobApplicationService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.jobApplicationService = jobApplicationService;
        }

        public async Task ApplyToJobOfferAsync(JobApplicationDto jobApplicationDto)
        {
            jobApplicationService.Create(mapper.Map<JobApplication>(jobApplicationDto));
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByJobOfferIdAsync(JobApplicationDto jobApplicationDto)
        {
            return mapper.Map<IEnumerable<JobApplicationDto>>(await jobApplicationService.GetByJobOfferIdAsync(
                jobApplicationDto.JobOfferId ?? throw new NullReferenceException("JobOfferId can't be null!")));
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByJobOfferIdAndStatusAsync(JobApplicationDto jobApplicationDto, Status status)
        {
            return mapper.Map<IEnumerable<JobApplicationDto>>(
                await jobApplicationService.GetByJobOfferIdAndStatusAsync(
                    jobApplicationDto.JobOfferId ?? throw new NullReferenceException("JobOfferId can't be null!"), status));
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByApplicantIdAsync(JobApplicationDto jobApplicationDto)
        {
            return mapper.Map<IEnumerable<JobApplicationDto>>(await jobApplicationService.GetByApplicantIdAsync(
                jobApplicationDto.ApplicantId ?? throw new NullReferenceException("ApplicantId can't be null!")));
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByApplicantIdAndStatusAsync(JobApplicationDto jobApplicationDto, Status status)
        {
            return mapper.Map<IEnumerable<JobApplicationDto>>(
                await jobApplicationService.GetByApplicantIdAndStatusAsync(jobApplicationDto.ApplicantId ?? throw new NullReferenceException("ApplicantId can't be null!"), status));
        }

        public async Task UpdateStatusAsync(JobApplicationDto jobApplicationDto, Status status)
        {
            jobApplicationService.UpdateStatus(jobApplicationDto.Id ?? throw new NullReferenceException("JobApplicationId can't be null!"), status);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(JobApplicationDto jobApplicationDto)
        {
            jobApplicationService.Delete(jobApplicationDto.Id ?? throw new NullReferenceException("JobApplicationId can't be null!"));
            await unitOfWork.SaveChangesAsync();
        }
    }
}
