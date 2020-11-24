using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;
using DAL;

namespace BL.QueryObejcts
{
    public class CompanyQueryObject : QueryObject
    {
        public CompanyQueryObject(UnitOfWork unit) : base(unit) { }

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
