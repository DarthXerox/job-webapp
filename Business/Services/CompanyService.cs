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

        public void AddCompany(Company company)
        {
            unitOfWork.CompanyRepository.Add(company);
        }

        public void UpdateCompany(Company company)
        {
            unitOfWork.CompanyRepository.Update(company);
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
