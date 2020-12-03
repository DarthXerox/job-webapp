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

        public async Task ApplyToJobOfferAsync(int jobOfferId, int applicantId, string text, ICollection<JobApplicationAnswerDto> answers)
        {
            jobApplicationService.Create(jobOfferId, applicantId, text, mapper.Map<ICollection<JobApplicationAnswer>>(answers));
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByJobOfferIdAsync(int jobOfferId)
        {
            return mapper.Map<IEnumerable<JobApplicationDto>>(await jobApplicationService.GetByJobOfferIdAsync(jobOfferId));
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByJobOfferIdAndStatusAsync(int jobOfferId, Status status)
        {
            return mapper.Map<IEnumerable<JobApplicationDto>>(
                await jobApplicationService.GetByJobOfferIdAndStatusAsync(jobOfferId, status));
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByApplicantIdAsync(int applicantId)
        {
            return mapper.Map<IEnumerable<JobApplicationDto>>(await jobApplicationService.GetByApplicantIdAsync(applicantId));
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByApplicantIdAndStatusAsync(int applicantId, Status status)
        {
            return mapper.Map<IEnumerable<JobApplicationDto>>(
                await jobApplicationService.GetByApplicantIdAndStatusAsync(applicantId, status));
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
    }
}
