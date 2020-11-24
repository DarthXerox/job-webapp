using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Enums;
using Infrastructure;

namespace Business.QueryObjects
{
    public class JobApplicationQueryObject : QueryObject
    {
        public JobApplicationQueryObject(UnitOfWork unit) : base(unit) { }

        public async Task<IEnumerable<JobApplication>> GetByJobOfferIdAsync(int jobOfferId, bool ascendingOrder = true)
        {
            return await UnitOfWork.JobApplicationQuery
                .FilterByJobOfferId(jobOfferId)
                .OrderBy(keySelector: application => application.Status, ascendingOrder)
                .ExecuteAsync();
        }

        public async Task<IEnumerable<JobApplication>> GetByStatusAsync(Status status, bool ascendingOrder = true)
        {
            return await UnitOfWork.JobApplicationQuery
                .FilterByStatus(status)
                .OrderBy(keySelector: application => application.Status, ascendingOrder)
                .ExecuteAsync();
        }
    }
}
