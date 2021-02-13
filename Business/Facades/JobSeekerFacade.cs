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
        private readonly UserService userService;

        public JobSeekerFacade(UnitOfWork unitOfWork, IMapper mapper, JobSeekerService jobSeekerService, UserService userService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.jobSeekerService = jobSeekerService;
            this.userService = userService;
        }

        public async Task<JobSeekerDto> GetByIdAsync(int id)
        {
            return mapper.Map<JobSeekerDto>(await jobSeekerService.GetJobSeeker(id));
        }

        public async Task RegisterAsync(JobSeekerDto jobSeekerDto)
        {
            var jobSeeker = mapper.Map<JobSeeker>(jobSeekerDto);
            jobSeekerService.RegisterJobSeeker(jobSeeker);
            await unitOfWork.SaveChangesAsync();
            var user = await userService.GetByIdAsync(jobSeeker.UserId.Value);
            user.JobSeekerId = jobSeeker.Id;
            await unitOfWork.SaveChangesAsync();

            jobSeekerDto.Id = jobSeeker.Id; // this helps with unit tests
        }

        public async Task<JobSeekerDto> GetInfoAsync(JobSeekerDto jobSeekerDto)
        {
            return mapper.Map<JobSeeker, JobSeekerDto>(await jobSeekerService.GetJobSeeker(
                jobSeekerDto.Id ?? throw new NullReferenceException("JobSeekerDto.Id can't be null!")));
        }

        public async Task EditInfoAsync(JobSeekerDto jobSeekerDto)
        {
            jobSeekerService.UpdateJobSeeker(mapper.Map<JobSeeker>(jobSeekerDto));
            await unitOfWork.SaveChangesAsync();
        }
    }
}
