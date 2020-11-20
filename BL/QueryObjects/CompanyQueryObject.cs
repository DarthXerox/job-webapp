using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BL.Entities.Dto;
using DAL;

namespace BL.QueryObejcts
{
    public class CompanyQueryObject : QueryObject
    {
        public CompanyQueryObject(UnitOfWork unit, IMapper mapper) : base(unit, mapper) { }

        public async Task<IEnumerable<JobOfferDto>> GetByNameAsync(string name, bool ascendingOrder = true)
        {
            return mapper.Map<IEnumerable<JobOfferDto>>
                (await UnitOfWork.CompanyQuery
                            .FilterByName(name)
                            .OrderBy(company => company.Name, ascendingOrder)
                            .ExecuteAsync()
                 );
        }

        public async Task<IEnumerable<JobOfferDto>> GetByNameContainsAsync(string name, bool ascendingOrder = true)
        {
            return mapper.Map<IEnumerable<JobOfferDto>>
                (await UnitOfWork.CompanyQuery
                            .FilterByNameContains(name)
                            .OrderBy(company => company.Name, ascendingOrder)
                            .ExecuteAsync()
                 );
        }

    }
}
