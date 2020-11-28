using System;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public sealed class UnitOfWork : IDisposable
    {
        private readonly JobDbContext context;


        private Repository<Company>? companyRepository;
        private Repository<JobApplicationAnswer>? jobApplicationAnswerRepository;
        private Repository<JobApplication>? jobApplicationRepository;
        private Repository<JobOfferQuestion>? jobOfferQuestionRepository;
        private Repository<JobOffer>? jobOfferRepository;
        private Repository<JobOfferSkill>? jobOfferSkillRepository;
        private Repository<JobSeeker>? jobSeekerRepository;
        private Repository<JobSeekerSkill>? jobSeekerSkillRepository;
        private Repository<Skill>? skillRepository;

        public UnitOfWork()
        {
            context = new JobDbContext();
            JobOfferQuery = new JobOfferQuery(context);
            CompanyQuery = new CompanyQuery(context);
            JobApplicationQuery = new JobApplicationQuery(context);
        }

        public UnitOfWork(DbContextOptions<JobDbContext> contextOptions)
        {
            context = new JobDbContext(contextOptions);
            JobOfferQuery = new JobOfferQuery(context);
            CompanyQuery = new CompanyQuery(context);
            JobApplicationQuery = new JobApplicationQuery(context);
        }

        public Repository<Company> CompanyRepository => companyRepository ??= new Repository<Company>(context);

        public Repository<JobApplication> JobApplicationRepository =>
            jobApplicationRepository ??= new Repository<JobApplication>(context);

        public Repository<JobApplicationAnswer> JobApplicationAnswerRepository =>
            jobApplicationAnswerRepository ??= new Repository<JobApplicationAnswer>(context);

        public Repository<JobOffer> JobOfferRepository => jobOfferRepository ??= new Repository<JobOffer>(context);

        public Repository<JobOfferQuestion> JobOfferQuestionRepository =>
            jobOfferQuestionRepository ??= new Repository<JobOfferQuestion>(context);

        public Repository<JobOfferSkill> JobOfferSkillRepository =>
            jobOfferSkillRepository ??= new Repository<JobOfferSkill>(context);

        public Repository<JobSeeker> JobSeekerRepository => jobSeekerRepository ??= new Repository<JobSeeker>(context);

        public Repository<JobSeekerSkill> JobSeekerSkillRepository =>
            jobSeekerSkillRepository ??= new Repository<JobSeekerSkill>(context);

        public Repository<Skill> SkillRepository => skillRepository ??= new Repository<Skill>(context);

        public JobOfferQuery JobOfferQuery { get; }
        public CompanyQuery CompanyQuery { get; }
        public JobApplicationQuery JobApplicationQuery { get; }

        public void Dispose()
        {
            context.Dispose();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
