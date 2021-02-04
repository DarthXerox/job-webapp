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
            JobSeekerRepository = new Repository<JobSeeker>(context);
            UserRepository = new Repository<User>(context);
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
            Repository<JobSeeker> jobSeekerRepository,
            Repository<User> userRepository,
            UserQuery userQuery,
            JobOfferQuery jobOfferQuery,
            CompanyQuery companyQuery,
            JobApplicationQuery jobApplicationQuery)
        {
            this.context = context;
            CompanyRepository = companyRepository;
            JobApplicationAnswerRepository = jobApplicationAnswerRepository;
            JobApplicationRepository = jobApplicationRepository;
            JobOfferQuestionRepository = jobOfferQuestionRepository;
            JobOfferRepository = jobOfferRepository;
            JobSeekerRepository = jobSeekerRepository;
            UserRepository = userRepository;
            UserQuery = userQuery;
            JobOfferQuery = jobOfferQuery;
            CompanyQuery = companyQuery;
            JobApplicationQuery = jobApplicationQuery;
        }

        public Repository<Company> CompanyRepository { get; }
        public Repository<JobApplication> JobApplicationRepository { get; }
        public Repository<JobApplicationAnswer> JobApplicationAnswerRepository { get; }
        public Repository<JobOffer> JobOfferRepository { get; }
        public Repository<JobOfferQuestion> JobOfferQuestionRepository { get; }
        public Repository<JobSeeker> JobSeekerRepository { get; }
        public Repository<User> UserRepository { get; }

        public UserQuery UserQuery { get; }
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
