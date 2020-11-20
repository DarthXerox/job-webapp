using System;
using DAL.Entities;
using DAL.Queries;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public sealed class UnitOfWork : IDisposable
    {
        private readonly JobDbContext context;

        public UnitOfWork()
        {
            context = new JobDbContext();
            JobOfferQuery = new JobOfferQuery(context);
        }


        public UnitOfWork(DbContextOptions<JobDbContext> contextOptions)
        {
            context = new JobDbContext(contextOptions);
        }


        private Repository<Company> companyRepository;
        private Repository<JobApplication> jobApplicationRepository;
        private Repository<JobApplicationAnswer> jobApplicationAnswerRepository;
        private Repository<JobOffer> jobOfferRepository;
        private Repository<JobOfferQuestion> jobOfferQuestionRepository;
        private Repository<JobOfferSkill> jobOfferSkillRepository;
        private Repository<JobSeeker> jobSeekerRepository;
        private Repository<JobSeekerSkill> jobSeekerSkillRepository;
        private Repository<Skill> skillRepository;

        public Repository<Company> CompanyRepository => companyRepository ??= new Repository<Company>(context);
        public Repository<JobApplication> JobApplicationRepository => jobApplicationRepository ??= new Repository<JobApplication>(context);
        public Repository<JobApplicationAnswer> JobApplicationAnswerRepository => jobApplicationAnswerRepository ??= new Repository<JobApplicationAnswer>(context);
        public Repository<JobOffer> JobOfferRepository => jobOfferRepository ??= new Repository<JobOffer>(context);
        public Repository<JobOfferQuestion> JobOfferQuestionRepository => jobOfferQuestionRepository ??= new Repository<JobOfferQuestion>(context);
        public Repository<JobOfferSkill> JobOfferSkillRepository => jobOfferSkillRepository ??= new Repository<JobOfferSkill>(context);
        public Repository<JobSeeker> JobSeekerRepository => jobSeekerRepository ??= new Repository<JobSeeker>(context);
        public Repository<JobSeekerSkill> JobSeekerSkillRepository => jobSeekerSkillRepository ??= new Repository<JobSeekerSkill>(context);
        public Repository<Skill> SkillRepository => skillRepository ??= new Repository<Skill>(context);

        public JobOfferQuery JobOfferQuery { get; }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public async void SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
