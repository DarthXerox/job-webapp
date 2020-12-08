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

        public async void AddAsync(CompanyDto companyDto)
        {
            companyService.AddCompany(mapper.Map<CompanyDto, Company>(companyDto));
            await unitOfWork.SaveChangesAsync();
        }

        public async void EditInfoAsync(CompanyDto companyDto)
        {
            companyService.UpdateCompany(mapper.Map<CompanyDto, Company>(companyDto));
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CompanyDto>> ListByNameAsync(CompanyDto companyDto)
        {
            return mapper.Map<IEnumerable<Company>, IEnumerable<CompanyDto>>(
                await companyService.ListCompaniesByNameAsync(companyDto.Name));
        }

        public async Task<IEnumerable<CompanyDto>> ListByNameDescendingAsync(CompanyDto companyDto)
        {
            return mapper.Map<IEnumerable<Company>, IEnumerable<CompanyDto>>(
                await companyService.ListCompaniesByNameAsync(companyDto.Name, false));
        }

        public async Task<IEnumerable<CompanyDto>> ListByNameContainsAsync(CompanyDto companyDto)
        {
            return mapper.Map<IEnumerable<Company>, IEnumerable<CompanyDto>>(
                await companyService.ListCompaniesByNameContainsAsync(companyDto.Name));
        }

        public async Task<IEnumerable<CompanyDto>> ListByNameContainsDescendingAsync(CompanyDto companyDto)
        {
            return mapper.Map<IEnumerable<Company>, IEnumerable<CompanyDto>>(
                await companyService.ListCompaniesByNameContainsAsync(companyDto.Name, false));
        }
    }
}
