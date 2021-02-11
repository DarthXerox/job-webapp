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

        public async Task<IEnumerable<Company>> GetAllAsyncPaged(int pageSize, int pageNumber)
        {
            return await companyQueryObject.GetAllAsyncPaged(pageSize, pageNumber);
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await companyQueryObject.GetAllAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await unitOfWork.CompanyRepository.GetTotalCountAsync();
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

        public async Task<Company> GetByIdAsync(int id)
        {
            return await unitOfWork.CompanyRepository.GetByIdAsync(id);
        }
    }

}
