using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTOs;
using Business.Services;
using DAL.Entities;
using Infrastructure;

namespace Business.Facades
{
    public class JobSeekerFacade
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly JobSeekerService jobSeekerService;

        public JobSeekerFacade(UnitOfWork unitOfWork, IMapper mapper, JobSeekerService jobSeekerService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.jobSeekerService = jobSeekerService;
        }

        public async void RegisterAsync(int id, string name, string surname, string email)
        {
            jobSeekerService.RegisterJobSeeker(id, name, surname, email);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<JobSeekerDto> GetInfoAsync(int id)
        {
            return mapper.Map<JobSeeker, JobSeekerDto>(await jobSeekerService.GetJobSeeker(id));
        } 

        public async void EditInfoAsync(int id, string name, string surname, string email)
        {
            jobSeekerService.UpdateJobSeeker(id, name, surname, email);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
