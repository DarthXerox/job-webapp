using System.Collections.Generic;
using System.Threading.Tasks;
using Business.QueryObjects;
using DAL.Entities;
using DAL.Enums;
using Infrastructure;

namespace Business.Services
{
    public class JobApplicationService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly JobApplicationQueryObject jobApplicationQueryObject;

        public JobApplicationService(UnitOfWork unitOfWork, JobApplicationQueryObject jobApplicationQueryObject)
        {
            this.unitOfWork = unitOfWork;
            this.jobApplicationQueryObject = jobApplicationQueryObject;
        }

        public async Task<IEnumerable<JobApplication>> GetByJobOfferIdAsync(int jobOfferId)
        {
            return await jobApplicationQueryObject.GetByJobOfferIdAsync(jobOfferId);
        }

        public async Task<IEnumerable<JobApplication>> GetByJobOfferIdAndStatusAsync(int jobOfferId, Status status)
        {
            return await jobApplicationQueryObject.GetByJobOfferIdAndStatusAsync(jobOfferId, status);
        }

        public async Task<IEnumerable<JobApplication>> GetByApplicantIdAsync(int applicantId)
        {
            return await jobApplicationQueryObject.GetByApplicantIdAsync(applicantId);
        }

        public async Task<IEnumerable<JobApplication>> GetByApplicantIdAndStatusAsync(int applicantId, Status status)
        {
            return await jobApplicationQueryObject.GetByApplicantIdAndStatusAsync(applicantId, status);
        }

        public void Create(JobApplication jobApplication)
        {
            unitOfWork.JobApplicationRepository.Add(jobApplication);
        }

        public void UpdateStatus(int applicationId, Status status)
        {
            var application = unitOfWork.JobApplicationRepository.GetById(applicationId);
            application.Status = status;
            unitOfWork.JobApplicationRepository.Update(application);
        }

        public void Delete(int applicationId)
        {
            unitOfWork.JobApplicationRepository.Delete(applicationId);
        }
    }
}
