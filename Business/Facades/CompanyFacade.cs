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

        public async Task<(int totalCount, IEnumerable<CompanyDto> companies)> GetAllAsyncPaged(int pageSize, int pageNumber)
        {
            var companies = mapper.Map<IEnumerable<CompanyDto>>(await companyService.GetAllAsyncPaged(pageSize, pageNumber));
            int totalCount = await companyService.GetTotalCountAsync();
            return (totalCount, companies);
        }

        public async Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            var companies = mapper.Map<IEnumerable<CompanyDto>>(await companyService.GetAllAsync());
            return companies;
        }

        public async Task AddAsync(CompanyDto companyDto)
        {
            companyService.AddCompany(mapper.Map<CompanyDto, Company>(companyDto));
            await unitOfWork.SaveChangesAsync();
        }

        public async Task EditInfoAsync(CompanyDto companyDto)
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
