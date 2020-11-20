using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DAL.Entities;
using AutoMapper;
using System.Threading.Tasks;
using DAL.Entities.Dto;
using DAL.Queries;

namespace BL
{
    public class JobOfferQueryObject : IDisposable
    {
        private readonly IMapper mapper;

        public UnitOfWork UnitOfWork { get; private set; }

        public JobOfferQueryObject(UnitOfWork unit, IMapper mapper)
        {
            UnitOfWork = unit;
            this.mapper = mapper;
        }

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


        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
