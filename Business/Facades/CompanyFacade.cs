using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTOs;
using Business.Services;
using DAL.Entities;
using Infrastructure;

namespace Business.Facades
{
    public class CompanyFacade
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly CompanyService companyService;

        public CompanyFacade(UnitOfWork unitOfWork, IMapper mapper, CompanyService companyService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.companyService = companyService;
        }

        public async void AddAsync(int id, string name)
        {
            companyService.AddCompany(id, name);
            await unitOfWork.SaveChangesAsync();
        }

        public async void EditInfoAsync(int id, string newName)
        {
            companyService.UpdateCompany(id, newName);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CompanyDto>> ListByNameAsync(string name)
        {
            return mapper.Map< IEnumerable<Company>, IEnumerable<CompanyDto>>(
                await companyService.ListCompaniesByNameAsync(name));
        }

        public async Task<IEnumerable<CompanyDto>> ListByNameDescendingAsync(string name)
        {
            return mapper.Map<IEnumerable<Company>, IEnumerable<CompanyDto>>(
                await companyService.ListCompaniesByNameAsync(name, false));
        }

        public async Task<IEnumerable<CompanyDto>> ListByNameContainsContainsAsync(string name)
        {
            return mapper.Map<IEnumerable<Company>, IEnumerable<CompanyDto>>(
                await companyService.ListCompaniesByNameContainsAsync(name));
        }

        public async Task<IEnumerable<CompanyDto>> ListByNameContainsDescendingAsync(string name)
        {
            return mapper.Map<IEnumerable<Company>, IEnumerable<CompanyDto>>(
                await companyService.ListCompaniesByNameContainsAsync(name, false));
        }
    }
}
