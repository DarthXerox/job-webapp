using System.Linq;
using DAL;
using DAL.Entities;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class JobApplicationQuery : Query<JobApplication>
    {
        public JobApplicationQuery(JobDbContext dbContext) : base(dbContext)
        {
            queryable = queryable.Include(application => application.Applicant);
        }

        public JobApplicationQuery FilterByJobOfferId(int jobOfferId)
        {
            queryable = queryable.Where(application => application.JobOfferId == jobOfferId);
            return this;
        }

        public JobApplicationQuery FilterByApplicantId(int applicantId)
        {
            queryable = queryable.Where(application => application.ApplicantId == applicantId);
            return this;
        }

        public JobApplicationQuery FilterByStatus(Status status)
        {
            queryable = queryable.Where(application => application.Status == status);
            return this;
        }

        public JobApplicationQuery FilterByCompanyId(int companyId)
        {
            queryable = queryable.Where(application => application.JobOffer.CompanyId == companyId);
            return this;
        }
    }
}
