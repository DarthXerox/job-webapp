using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTOs;
using Business.QueryObjects;
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

        public JobApplicationFacade()
        {
            unitOfWork = new UnitOfWork();
            mapper = new Mapper(new MapperConfiguration(GeneralMappingConfig.ConfigureMapping));
            jobApplicationService = new JobApplicationService(unitOfWork);
        }

        public async Task ApplyToJobOfferAsync(int jobOfferId, int applicantId, string text, ICollection<JobApplicationAnswer> answers)
        {
            jobApplicationService.CreateJobApplication(jobOfferId, applicantId, text, answers);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobApplicationDto>> GetJobApplicationsForJobOfferId(int jobOfferId)
        {
            IEnumerable<JobApplication> applications =
                await jobApplicationService.GetJobApplicationsByJobOfferIdAsync(jobOfferId);
            return mapper.Map<IEnumerable<JobApplicationDto>>(applications);
        }

        public async Task UpdateJobApplicationStatus(int applicationId, Status status)
        {
            jobApplicationService.UpdateJobApplicationStatus(applicationId, status);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteJobApplication(int applicationId)
        {
            jobApplicationService.DeleteJobApplication(applicationId);
            await unitOfWork.SaveChangesAsync();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
