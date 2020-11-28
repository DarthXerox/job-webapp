using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.QueryObjects;
using DAL.Entities;
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

        public void CreateJobApplication(int jobOfferId, int applicantId)
        {
            var application = new JobApplication{ JobOfferId = jobOfferId, ApplicantId = applicantId };
            unitOfWork.JobApplicationRepository.Add(application);
        }
    }
}
