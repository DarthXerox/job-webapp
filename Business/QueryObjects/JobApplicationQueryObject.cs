using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.DTOs;
using DAL.Entities;
using DAL.Enums;
using Infrastructure;

namespace Business.QueryObjects
{
    public class JobApplicationQueryObject : QueryObject
    {
        public JobApplicationQueryObject(UnitOfWork unit) : base(unit)
        {}

        public async Task<IEnumerable<JobApplication>> GetByJobOfferIdAsync(int jobOfferId, bool ascendingOrder = true)
        {
            return await UnitOfWork.JobApplicationQuery
                .FilterByJobOfferId(jobOfferId)
                .OrderBy(keySelector: application => application.Status, ascendingOrder)
                .ExecuteAsync();
        }

        public async Task<IEnumerable<JobApplication>> GetByJobOfferIdAndStatusAsync(int jobOfferId, Status status)
        {
            return await UnitOfWork.JobApplicationQuery
                .FilterByJobOfferId(jobOfferId)
                .FilterByStatus(status)
                .ExecuteAsync();
        }

        public async Task<IEnumerable<JobApplication>> GetByApplicantIdAsync(int applicantId, bool ascendingOrder = true)
        {
            return await UnitOfWork.JobApplicationQuery
                .FilterByApplicantId(applicantId)
                .OrderBy(keySelector: application => application.Status, ascendingOrder)
                .ExecuteAsync();
        }

        public async Task<IEnumerable<JobApplication>> GetByApplicantIdAndStatusAsync(int applicantId, Status status)
        {
            return await UnitOfWork.JobApplicationQuery
                .FilterByApplicantId(applicantId)
                .FilterByStatus(status)
                .ExecuteAsync();
        }

        public async Task<IEnumerable<JobApplication>> GetByCompanyIdAsync(int companyId)
        {
            return await UnitOfWork.JobApplicationQuery
                .FilterByCompanyId(companyId)
                .OrderBy(keySelector: application => application.Status)
                .ExecuteAsync();
        }

        public async Task<JobApplication> GetByIdAsync(int id)
        {
            return (await UnitOfWork.JobApplicationQuery
                .FilterById(id)
                .ExecuteAsync()).First();
        }
    }
}
