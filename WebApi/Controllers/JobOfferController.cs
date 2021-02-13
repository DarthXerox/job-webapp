using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.DTOs;
using Business.Facades;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobOfferListController : ControllerBase
    {
        private readonly JobOfferFacade jobOfferFacade;

        public JobOfferListController(JobOfferFacade jobOfferFacade)
        {
            this.jobOfferFacade = jobOfferFacade;
        }

        [HttpGet]
        [Route("Paging")]
        public async Task<(int totalCount, IEnumerable<JobOfferDto> offers)> GetJobOffersAsync(int pgsz, int pgnum)
            => await jobOfferFacade.GetAllAsync(pgsz, pgnum);

        [HttpGet]
        [Route("All")]
        public async Task<(int totalCount, IEnumerable<JobOfferDto> offers)> GetAllJobOffersAsync()
            => await jobOfferFacade.GetAllWithoutPagingAsync();

        [HttpGet]
        [Route("ByCompany")]
        public async Task<IEnumerable<JobOfferDto>> GetAppleOffers(string name)
            => await jobOfferFacade.GetByCompanyNameAsync(new JobOfferDto() { Company = new CompanyDto() {Name = name}});
    }
}
