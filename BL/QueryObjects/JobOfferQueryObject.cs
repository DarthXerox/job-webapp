using System.Collections.Generic;
using DAL;
using AutoMapper;
using System.Threading.Tasks;
using BL.Entities.Dto;

namespace BL
{
    public class JobOfferQueryObject : QueryObject
    {
        public JobOfferQueryObject(UnitOfWork unit, IMapper mapper) : base(unit, mapper) { }

        public async Task<IEnumerable<JobOfferDto>> GetByNameAsync(string name, bool ascendingOrder = true)
        {
            return mapper.Map<IEnumerable<JobOfferDto>>
                (await UnitOfWork.JobOfferQuery
                            .FilterByName(name)
                            .OrderBy(jobOffer => jobOffer.Name, ascendingOrder)
                            .ExecuteAsync()
                 );
        }

        public async Task<IEnumerable<JobOfferDto>> GetByNameContainsAsync(string name, bool ascendingOrder = true)
        {
            return mapper.Map<IEnumerable<JobOfferDto>>
                (await UnitOfWork.JobOfferQuery
                            .FilterByNameContains(name)
                            .OrderBy(jobOffer => jobOffer.Name, ascendingOrder)
                            .ExecuteAsync()
                 );
        }

        public async Task<IEnumerable<JobOfferDto>> GetByCompanyNameAsync(string comapnyName, bool ascendingOrder = true)
        {
            return mapper.Map<IEnumerable<JobOfferDto>>
                (await UnitOfWork.JobOfferQuery
                            .FilterByCompanyName(comapnyName)
                            .OrderBy(jobOffer => jobOffer.Name, ascendingOrder)
                            .ExecuteAsync()
                 );
        }

        public async Task<IEnumerable<JobOfferDto>> GetBySkillTagAsync(string skillTag, bool ascendingOrder = true)
        {
            return mapper.Map<IEnumerable<JobOfferDto>>
                (await UnitOfWork.JobOfferQuery
                            .FilterBySkillTag(skillTag)
                            .OrderBy(jobOffer => jobOffer.Name, ascendingOrder)
                            .ExecuteAsync()
                 );
        }

        public async Task<IEnumerable<JobOfferDto>> GetByCityAsync(string city, bool ascendingOrder = true)
        {
            return mapper.Map<IEnumerable<JobOfferDto>>
                (await UnitOfWork.JobOfferQuery
                            .FilterByCity(city)
                            .OrderBy(jobOffer => jobOffer.Name, ascendingOrder)
                            .ExecuteAsync()
                 );
        }
    }
}
