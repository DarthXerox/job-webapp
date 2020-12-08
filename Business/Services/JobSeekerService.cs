using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Infrastructure;

namespace Business.Services
{
    public class JobSeekerService 
    {
        private readonly UnitOfWork unitOfWork;

        public JobSeekerService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void RegisterJobSeeker(JobSeeker jobSeeker)
        {
            unitOfWork.JobSeekerRepository.Add(jobSeeker);
        }

        public async Task<JobSeeker> GetJobSeeker(int id)
        {
            return await unitOfWork.JobSeekerRepository.GetByIdAsync(id);
        }

        public void UpdateJobSeeker(JobSeeker jobSeeker)
        {
            unitOfWork.JobSeekerRepository.Update(jobSeeker);
        }


    }
}
