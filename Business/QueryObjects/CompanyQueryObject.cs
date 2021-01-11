using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;
using Infrastructure;

namespace Business.QueryObjects
{
    public class CompanyQueryObject : QueryObject
    {
        public CompanyQueryObject(UnitOfWork unit) : base(unit) { }

        public async Task<IEnumerable<Company>> GetAllAsyncPaged(int pageSize, int pageNumber)
        {
            return await UnitOfWork.CompanyQuery
                .OrderBy(keySelector: company => company.Name)
                .Page(pageSize, pageNumber)
                .ExecuteAsync();
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await UnitOfWork.CompanyQuery
                .OrderBy(keySelector: company => company.Name)
                .ExecuteAsync();
        }

        public async Task<IEnumerable<Company>> GetByNameAsync(string name, bool ascendingOrder = true)
        {
            return await UnitOfWork.CompanyQuery
                .FilterByName(name)
                .OrderBy(keySelector: company => company.Name, ascendingOrder)
                .ExecuteAsync();
        }

        public async Task<IEnumerable<Company>> GetByNameContainsAsync(string name, bool ascendingOrder = true)
        {
            return await UnitOfWork.CompanyQuery
                .FilterByNameContains(name)
                .OrderBy(keySelector: company => company.Name, ascendingOrder)
                .ExecuteAsync();
        }
    }
}
