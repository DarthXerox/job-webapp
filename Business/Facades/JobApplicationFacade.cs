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
    public class JobApplicationFacade : IDisposable
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

        public async Task ApplyToJobOfferAsync(int jobOfferId, int applicantId, string text, ICollection<JobApplicationAnswer> answers)
        {
            jobApplicationService.Create(jobOfferId, applicantId, text, answers);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByJobOfferIdAsync(int jobOfferId)
        {
            IEnumerable<JobApplication> applications =
                await jobApplicationService.GetByJobOfferIdAsync(jobOfferId);
            return mapper.Map<IEnumerable<JobApplicationDto>>(applications);
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByJobOfferIdAndStatusAsync(int jobOfferId, Status status)
        {
            IEnumerable<JobApplication> applications =
                await jobApplicationService.GetByJobOfferIdAndStatusAsync(jobOfferId, status);
            return mapper.Map<IEnumerable<JobApplicationDto>>(applications);
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByApplicantIdAsync(int applicantId)
        {
            IEnumerable<JobApplication> applications =
                await jobApplicationService.GetByApplicantIdAsync(applicantId);
            return mapper.Map<IEnumerable<JobApplicationDto>>(applications);
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByApplicantIdAndStatusAsync(int applicantId, Status status)
        {
            IEnumerable<JobApplication> applications =
                await jobApplicationService.GetByApplicantIdAndStatusAsync(applicantId, status);
            return mapper.Map<IEnumerable<JobApplicationDto>>(applications);
        }

        public async Task UpdateStatusAsync(int applicationId, Status status)
        {
            jobApplicationService.UpdateStatus(applicationId, status);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int applicationId)
        {
            jobApplicationService.Delete(applicationId);
            await unitOfWork.SaveChangesAsync();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
