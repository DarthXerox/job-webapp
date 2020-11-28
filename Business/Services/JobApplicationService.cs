using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.QueryObjects;
using DAL.Entities;
using DAL.Enums;
using Infrastructure;

namespace Business.Services
{
    class JobApplicationService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly JobApplicationQueryObject jobApplicationQueryObject;

        public JobApplicationService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            jobApplicationQueryObject = new JobApplicationQueryObject(unitOfWork);
        }

        public async Task<IEnumerable<JobApplication>> GetJobApplicationsByJobOfferIdAsync(int jobOfferId)
        {
            return await jobApplicationQueryObject.GetByJobOfferIdAsync(jobOfferId);
        }

        public void CreateJobApplication(int jobOfferId, int applicantId, string text, ICollection<JobApplicationAnswer> answers)
        {
            var application = new JobApplication{ JobOfferId = jobOfferId, ApplicantId = applicantId,
                                                  Status = Status.Unresolved, Text = text, Answers = answers};
            unitOfWork.JobApplicationRepository.Add(application);
        }

        public void UpdateJobApplicationStatus(int applicationId, Status status)
        {
            var application = unitOfWork.JobApplicationRepository.GetById(applicationId);
            application.Status = status;
            unitOfWork.JobApplicationRepository.Update(application);
        }

        public void DeleteJobApplication(int applicationId)
        {
            unitOfWork.JobApplicationRepository.Delete(applicationId);
        }
    }
}
