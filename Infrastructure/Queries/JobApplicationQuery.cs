using System.Linq;
using DAL.Entities;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class JobApplicationQuery : Query<JobApplication>
    {
        public JobApplicationQuery(DbContext dbContext) : base(dbContext)
        {
            Queryable = Queryable.Include(application => application.Applicant);
        }

        public JobApplicationQuery FilterByJobOfferId(int jobOfferId)
        {
            Queryable = Queryable.Where(application => application.JobOfferId == jobOfferId);
            return this;
        }

        public JobApplicationQuery FilterByApplicantId(int applicantId)
        {
            Queryable = Queryable.Where(application => application.ApplicantId == applicantId);
            return this;
        }

        public JobApplicationQuery FilterByStatus(Status status)
        {
            Queryable = Queryable.Where(application => application.Status == status);
            return this;
        }
    }
}
