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

        public void RegisterJobSeeker(int id, string name, string surname, string email)
        {
            unitOfWork.JobSeekerRepository.Add(new JobSeeker()
            { 
                Id = id,
                Name = name,
                Surname = surname,
                Email = email
            });
        }

        public async Task<JobSeeker> GetJobSeeker(int id)
        {
            return await unitOfWork.JobSeekerRepository.GetByIdAsync(id);
        }

        public void UpdateJobSeeker(int id, string name, string surname, string email)
        {
            unitOfWork.JobSeekerRepository.Update(new JobSeeker()
            {
                Id = id,
                Name = name,
                Surname = surname,
                Email = email
            });
        }


    }
}
