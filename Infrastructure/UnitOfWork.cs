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

        // for tests
        public UnitOfWork(DbContextOptions<JobDbContext> contextOptions)
        {
            context = new JobDbContext(contextOptions);
            CompanyRepository = new Repository<Company>(context);
            JobApplicationAnswerRepository = new Repository<JobApplicationAnswer>(context);
            JobApplicationRepository = new Repository<JobApplication>(context);
            JobOfferQuestionRepository = new Repository<JobOfferQuestion>(context);
            JobOfferRepository = new Repository<JobOffer>(context);
            JobOfferSkillRepository = new Repository<JobOfferSkill>(context);
            JobSeekerRepository = new Repository<JobSeeker>(context);
            JobSeekerSkillRepository = new Repository<JobSeekerSkill>(context);
            SkillRepository = new Repository<Skill>(context);
            JobOfferQuery = new JobOfferQuery(context);
            CompanyQuery = new CompanyQuery(context);
            JobApplicationQuery = new JobApplicationQuery(context);
        }

        public UnitOfWork(JobDbContext context,
            Repository<Company> companyRepository,
            Repository<JobApplicationAnswer> jobApplicationAnswerRepository,
            Repository<JobApplication> jobApplicationRepository,
            Repository<JobOfferQuestion> jobOfferQuestionRepository,
            Repository<JobOffer> jobOfferRepository,
            Repository<JobOfferSkill> jobOfferSkillRepository,
            Repository<JobSeeker> jobSeekerRepository,
            Repository<JobSeekerSkill> jobSeekerSkillRepository,
            Repository<Skill> skillRepository,
            JobOfferQuery jobOfferQuery, CompanyQuery companyQuery, JobApplicationQuery joApplicationQuery)
        {
            this.context = context;
            CompanyRepository = companyRepository;
            JobApplicationAnswerRepository = jobApplicationAnswerRepository;
            JobApplicationRepository = jobApplicationRepository;
            JobOfferQuestionRepository = jobOfferQuestionRepository;
            JobOfferRepository = jobOfferRepository;
            JobOfferSkillRepository = jobOfferSkillRepository;
            JobSeekerRepository = jobSeekerRepository;
            JobSeekerSkillRepository = jobSeekerSkillRepository;
            SkillRepository = skillRepository;
            JobOfferQuery = jobOfferQuery;
            CompanyQuery = companyQuery;
            JobApplicationQuery = joApplicationQuery;
        }

        public Repository<Company> CompanyRepository { get; }
        public Repository<JobApplication> JobApplicationRepository { get; }
        public Repository<JobApplicationAnswer> JobApplicationAnswerRepository { get; }
        public Repository<JobOffer> JobOfferRepository { get; }
        public Repository<JobOfferQuestion> JobOfferQuestionRepository { get; }
        public Repository<JobOfferSkill> JobOfferSkillRepository { get; }
        public Repository<JobSeeker> JobSeekerRepository { get; }
        public Repository<JobSeekerSkill> JobSeekerSkillRepository { get; }
        public Repository<Skill> SkillRepository { get; }

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
