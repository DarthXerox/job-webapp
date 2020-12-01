using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.QueryObjects;
using DAL.Entities;
using Infrastructure;

namespace Business.Services
{
    public class CompanyService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly CompanyQueryObject companyQueryObject;

        public CompanyService(UnitOfWork unitOfWork, CompanyQueryObject companyQueryObject)
        {
            this.unitOfWork = unitOfWork;
            this.companyQueryObject = companyQueryObject;
        }

        public void AddCompany(int id, string name)
        {
            unitOfWork.CompanyRepository.Add(new Company()
            {
                Id = id,
                Name = name
            });
        }

        public void UpdateCompany(int id, string name)
        {
            unitOfWork.CompanyRepository.Update(new Company()
            {
                Id = id,
                Name = name
            });
        }

        public async Task<IEnumerable<Company>> ListCompaniesByNameAsync(string name, bool ascending = true)
        {
            return await companyQueryObject.GetByNameAsync(name, ascending);
        }

        public async Task<IEnumerable<Company>> ListCompaniesByNameContainsAsync(string name, bool ascending = true)
        {
            return await companyQueryObject.GetByNameContainsAsync(name, ascending);
        }
    }

}
