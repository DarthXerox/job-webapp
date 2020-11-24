using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BL;
using DAL.Entities;
using Infrastructure;

namespace Business.QueryObjects
{
    public class CompanyQueryObject : QueryObject
    {
        public CompanyQueryObject(UnitOfWork unit, IMapper mapper) : base(unit) { }

        public async Task<IEnumerable<Company>> GetByNameAsync(string name, bool ascendingOrder = true)
        {
            return await UnitOfWork.CompanyQuery
                .FilterByName(name)
                .OrderBy(company => company.Name, ascendingOrder)
                .ExecuteAsync();
        }

        public async Task<IEnumerable<Company>> GetByNameContainsAsync(string name, bool ascendingOrder = true)
        {
            return await UnitOfWork.CompanyQuery
                .FilterByNameContains(name)
                .OrderBy(company => company.Name, ascendingOrder)
                .ExecuteAsync();
        }
    }
}
